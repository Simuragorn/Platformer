using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public int bonusHealth = 10;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.healthContainer.TryGetValue(collision.gameObject, out Health health))
        {
            health.SetHealth(bonusHealth);
            animator.SetTrigger("StartDestroy");
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
