using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour, IBuffable
{
    [SerializeField] private int originalDamage;
    private int damage;
    [SerializeField] private bool isDestroyingAfterCollision;
    private GameObject parent;
    private IObjectDestroyer destroyer;
    public GameObject Parent { get { return parent; } set { parent = value; } }

    public void Init(IObjectDestroyer objectDestroyer)
    {
        destroyer = objectDestroyer;
        damage = originalDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == parent)
        {
            return;
        }
        if (GameManager.Instance.HealthContainer.TryGetValue(collision.gameObject, out Health health))
        {
            health.TakeHit(damage);
        }
        if (isDestroyingAfterCollision)
        {
            if (destroyer != null)
            {
                destroyer.Destroy();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetBuff(int additiveValue)
    {
        damage = originalDamage + additiveValue;
    }
}

public interface IObjectDestroyer
{
    void Destroy();
}
