using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IBuffable
{
    [SerializeField] private int health;
    [SerializeField] private Animator animator;
    public int CurrentHealth => health;

    public void TakeHit(int damage)
    {
        health -= damage;
        if (animator != null)
        {
            animator.SetTrigger("GettingDamage");
        }
        if (health <= damage)
        {
            Object.Destroy(gameObject);
        }
    }

    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth;
        health = Mathf.Max(health, 100);

    }
    void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBuff(int value)
    {
        if (value <= 0)
        {
            health = Mathf.Min(health, 100);
            return;
        }
        health += value;
    }
}

public interface IBuffable
{
    void SetBuff(int additiveValue);
}
