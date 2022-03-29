using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEmitter : MonoBehaviour
{
    [SerializeField] private Buff buff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.Instance.buffRecieverContainer.TryGetValue(collision.gameObject, out BuffReciever reciever))
        {
            reciever.AddBuff(buff);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.Instance.buffRecieverContainer.TryGetValue(collision.gameObject, out BuffReciever reciever))
        {
            reciever.RemoveBuff(buff);
        }
    }
}
[System.Serializable]
public class Buff
{
    public BuffType type;
    public float additiveBonus;
    public float multipleBonus;
    public int lifetimeInSeconds;
}

public enum BuffType : byte
{
    Damage, Force, Armor
}
