using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffReciever : MonoBehaviour
{
    private List<Buff> buffs;
    public List<Buff> Buffs => buffs;
    public Action onBuffChanges;

    private void Start()
    {
        buffs = new List<Buff>();
        GameManager.Instance.BuffRecieverContainer.Add(gameObject, this);
    }

    public void AddBuff(Buff buff)
    {
        if (!buffs.Contains(buff))
        {
            buffs.Add(buff);
            StartCoroutine(RemoveEndedBuff(buff));
        }
        if (onBuffChanges != null)
        {
            onBuffChanges();
        }
    }

    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))
        {
            buffs.Remove(buff);
        }
        if (onBuffChanges != null)
        {
            onBuffChanges();
        }
    }

    private IEnumerator RemoveEndedBuff(Buff buff)
    {
        yield return new WaitForSeconds(buff.lifetimeInSeconds);
        RemoveBuff(buff);
    }
}
