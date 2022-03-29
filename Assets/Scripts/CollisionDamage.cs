using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int damage = 10;
    [SerializeField]
    private Animator animator;
    private Health targetHealth;
    private float direction;
    public float Direction => direction;

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.Instance.healthContainer.TryGetValue(collision.gameObject, out Health health))
        {
            direction = (collision.transform.position - transform.position).x;
            targetHealth = health;
            animator.SetFloat("Direction", Mathf.Abs(direction));
        }
    }

    public void SetDamage()
    {
        if (targetHealth != null)
            targetHealth.TakeHit(damage);
        targetHealth = null;
        direction = 0;
        animator.SetFloat("Direction", 0f);
    }
}
