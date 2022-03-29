using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Item item;

    private void Awake()
    {
        ClearCell();
    }
    public void Init(Item item)
    {
        this.item = item;
        icon.sprite = item.Icon;
    }

    public void OnClickCell()
    {
        if (item == null)
            return;
        PlayerInventory.Instance.Items.Remove(item);
        var buff = new Buff
        {
            type = item.Type,
            additiveBonus = item.Value,
            lifetimeInSeconds = item.LifetimeInSeconds
        };
        PlayerInventory.Instance.buffReciever.AddBuff(buff);
        ClearCell();
    }

    private void ClearCell()
    {
        icon.sprite = null;
        item = null;
    }
}
