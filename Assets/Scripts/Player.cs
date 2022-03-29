using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float buffedForce;

    public float speed = 2;
    public float force = 2;
    public Rigidbody2D rigidbody;
    public int minimalHeight;
    public bool isCheatMode;
    public Animator animator;
    private Vector3 direction;
    public SpriteRenderer spriteRenderer;
    public GroundDetection groundDetection;
    public static Player Instance;
    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private int arrowsCount = 3;
    public Health health;
    [SerializeField] private BuffReciever buffReciever;
    private bool isJumpReady = true;

    private List<Arrow> arrowsPool;

    private Arrow currentArrow;
    private UICharacterController uiCharacterController;

    [SerializeField] int reloadInSeconds;
    float secondsAfterShot;
    bool isReloading;

    private bool isJumping;

    public void Init(UICharacterController uicontroller)
    {
        uiCharacterController = uicontroller;
        uiCharacterController.Jump.onClick.AddListener(Jump);
        uiCharacterController.Fire.onClick.AddListener(StartShooting);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        buffedForce = force;
        arrowsPool = new List<Arrow>();
        for (int i = 0; i < arrowsCount; ++i)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            arrowsPool.Add(arrowTemp);
            arrowTemp.gameObject.SetActive(false);
        }
        buffReciever.onBuffChanges += ReloadBuffs;
    }

    // FixedUpdate is called fixed times per sec (50)
    void FixedUpdate()
    {
        Move();
        animator.SetFloat("Speed", Mathf.Abs(direction.x));
        CheckFall();
    }

    public void Move()
    {
        animator.SetBool("IsGrounded", groundDetection.isGrounding);
        if (!isJumping && !groundDetection.isGrounding)
        {
            animator.SetTrigger("StartFall");
        }
        isJumping = isJumping && !groundDetection.isGrounding;
        direction = Vector3.zero;

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
            direction = Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction = Vector3.right;
#endif

        if (uiCharacterController.Left.IsPressed)
            direction = Vector3.left;
        if (uiCharacterController.Right.IsPressed)
            direction = Vector3.right;
        direction *= speed;
        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
#endif
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }

    }

    public void Jump()
    {
        if (groundDetection.isGrounding && isJumpReady)
        {
            StartCoroutine(JumpDelay());
            rigidbody.AddForce(Vector2.up * buffedForce, ForceMode2D.Impulse);
            animator.SetBool("StartJump", true);
            isJumping = true;
        }
    }

    public void SpawnArrow()
    {
        currentArrow = GetArrowFromPool();
        float direction = spriteRenderer.flipX ? -1 : 1;
        currentArrow.SetImpulse(Vector2.zero, direction, this);
    }

    public void StartShooting()
    {
        if (!isReloading)
        {
            animator.SetTrigger("Shooting");
        }
    }

    public void Shoot()
    {
        var arrowForce = (spriteRenderer.flipX ? -buffedForce : buffedForce) * 5;
        currentArrow.SetImpulse(Vector2.right, arrowForce, this);

        isReloading = true;
        StartCoroutine(Reloading());
    }

    public void ReturnArrowToPool(Arrow arrow)
    {
        if (!arrowsPool.Contains(arrow))
        {
            arrowsPool.Add(arrow);
        }
        arrow.gameObject.SetActive(false);
        arrow.transform.parent = arrowSpawnPoint;
    }

    private IEnumerator JumpDelay()
    {
        isJumpReady = false;
        yield return new WaitForSeconds(0.5f);
        isJumpReady = true;
    }

    private void ReloadBuffs()
    {
        buffedForce = force;
        buffReciever.Buffs.ForEach(buff =>
        {
            switch (buff.type)
            {
                case BuffType.Damage:
                    arrow.BuffDamage((int)buff.additiveBonus);
                    break;
                case BuffType.Force:
                    buffedForce = force + buff.additiveBonus;
                    break;
                case BuffType.Armor:
                    health.SetBuff((int)buff.additiveBonus);
                    break;
                default:
                    break;
            }
        });
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            StartShooting();
        }
#endif
    }

    private Arrow GetArrowFromPool()
    {
        if (arrowsPool.Count > 0)
        {
            var arrowTemp = arrowsPool[0];
            arrowsPool.Remove(arrowTemp);
            arrowTemp.gameObject.SetActive(true);
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawnPoint.position;
            return arrowTemp;
        }
        return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
    }

    private IEnumerator Reloading()
    {
        while (isReloading && secondsAfterShot < reloadInSeconds)
        {
            secondsAfterShot += Time.deltaTime;
            yield return null;
        }

        isReloading = false;
        secondsAfterShot = 0;
        yield break;
    }


    private void CheckFall()
    {
        if (transform.position.y < minimalHeight)
        {
            if (isCheatMode)
            {
                transform.position = new Vector3(0, 0, 0);
                rigidbody.velocity = new Vector2(0, 0);
            }
            else
            {
                Object.Destroy(gameObject);
            }
        }
    }
}
