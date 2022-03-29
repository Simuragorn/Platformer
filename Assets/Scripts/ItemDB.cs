using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Databases/Items")]
public class ItemDB : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Item> items;
    [SerializeField] private Item currentItem;
    private int currentIndex;
    public void CreateItem()
    {
        if (items == null)
            items = new List<Item>();
        var item = new Item();
        items.Add(item);
        currentItem = item;
        currentIndex = items.Count - 1;
    }

    public void RemoveItem()
    {
        if (items == null)
            return;
        if (currentItem == null)
            return;

        items.Remove(currentItem);
        if (items.Count > 0)
            currentItem = items[0];
        else
            CreateItem();
        currentIndex = 0;
    }

    public void NextItem()
    {
        if (currentIndex + 1 < items.Count)
        {
            currentIndex++;
            currentItem = items[currentIndex];
        }
    }

    public void PrevItem()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            currentItem = items[currentIndex];
        }
    }

    public Item GetItem(int id)
    {
        return items.Find(i => i.Id == id);
    }
}

[System.Serializable]
public class Item
{
    [SerializeField] private int id;
    public int Id => id;
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    [SerializeField] private string description;
    public string Description => description;
    [SerializeField] private BuffType type;
    public BuffType Type => type;
    [SerializeField] private float value;
    public float Value => value;
    [SerializeField] private int lifetimeInSeconds;
    public int LifetimeInSeconds => lifetimeInSeconds;
}
