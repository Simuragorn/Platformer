using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Rigidbody2D rigidbody;
    [SerializeField] private float force;
    [SerializeField] private int lifetime;
    [SerializeField] private TriggerDamage triggerDamage;
    private Player player;
    public float Force { get { return force; } set { force = value; } }

    public void Destroy()
    {
        player.ReturnArrowToPool(this);
    }

    public void SetImpulse(Vector2 direction, float force, Player playerObject)
    {
        player = playerObject;
        triggerDamage.Parent = player.gameObject;
        triggerDamage.Init(this);
        if (force < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        if (isActiveAndEnabled)
            StartCoroutine(StartLife());
    }

    public void BuffDamage(int additionDamage)
    {
        triggerDamage.SetBuff(additionDamage);
    }

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy();
        yield break;
    }
}
