using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemComponent;

public class PlayerRewarder : MonoBehaviour
{
    [SerializeField] private ItemType rewardPotionType1;
    [SerializeField] private ItemType rewardPotionType2;
    [SerializeField] private ItemType rewardPotionType3;


    public static PlayerRewarder Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void RewardPlayer()
    {
        PlayerInventory.Instance.Items.AddRange(new List<Item>
        {
        GameManager.Instance.ItemDB.GetItem((int)rewardPotionType1),
        GameManager.Instance.ItemDB.GetItem((int)rewardPotionType2),
        GameManager.Instance.ItemDB.GetItem((int)rewardPotionType3),
        });
    }
}
