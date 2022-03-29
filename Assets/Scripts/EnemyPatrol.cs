using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundDetection))]
public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rightBorder;
    public Rigidbody2D rigidbody;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private CollisionDamage collisionDamage;

    private GroundDetection groundDetection;

    public bool isRightDirection;
    public float speed;

    private void Start()
    {
        groundDetection = GetComponent<GroundDetection>();
    }

    public void Update()
    {
        if (groundDetection.isGrounding)
        {
            if (transform.position.x > rightBorder.transform.position.x ||
               collisionDamage.Direction < 0)
                isRightDirection = false;
            if (transform.position.x < leftBorder.transform.position.x ||
                collisionDamage.Direction > 0)
                isRightDirection = true;

            rigidbody.velocity = isRightDirection ? Vector2.right : Vector2.left;
            rigidbody.velocity *= speed;
        }
        if (rigidbody.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        if (rigidbody.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
