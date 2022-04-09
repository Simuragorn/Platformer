using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private ItemType type;
    [SerializeField] SpriteRenderer spriteRenderer;
    private Item item;
    public Item Item => item;

    public void Destroy()
    {
       MonoBehaviour.Destroy(gameObject);
    }

    void Start()
    {
        item = GameManager.Instance.ItemDB.GetItem((int)type);
        spriteRenderer.sprite = item.Icon;
        GameManager.Instance.ItemContainer.Add(gameObject, this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum ItemType
    {
        ForcePotion, DamagePotion, ArmorPotion
    }
}
