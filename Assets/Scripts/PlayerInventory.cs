using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int coinsCount;
    public static PlayerInventory Instance { get; set; }
    public BuffReciever buffReciever;
    [SerializeField] private Text coinsText;
    private List<Item> items;
    public List<Item> Items => items;

    private void Awake()
    {
        Instance = this;
        coinsText.text = coinsCount.ToString();
    }

    private void Start()
    {
        items = new List<Item>();
    }

    public void AddCoin(int newCoinsCount)
    {
        coinsCount += newCoinsCount;
        coinsText.text = coinsCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.coinContainer.TryGetValue(collision.gameObject, out Coin coin))
        {
            AddCoin(1);
            coin.StartDestroy();
        }
        if (GameManager.Instance.itemContainer.TryGetValue(collision.gameObject, out ItemComponent itemComponent))
        {
            items.Add(itemComponent.Item);
            itemComponent.Destroy();
        }
    }
}
