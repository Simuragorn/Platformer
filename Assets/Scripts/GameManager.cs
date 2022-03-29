using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }
    #endregion
    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
    public ItemDB itemDB;
    public Dictionary<GameObject, ItemComponent> itemContainer;
    [SerializeField] private GameObject inventoryPanel;

    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
        itemContainer = new Dictionary<GameObject, ItemComponent>();
    }

    public void Pause()
    {
        if (Time.timeScale > 0)
        {
            inventoryPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            inventoryPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
