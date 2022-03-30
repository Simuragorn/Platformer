using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PanelsEnum
{
    Inventory,
    Settings,
    DeathScreen
}

public enum ScenesEnum
{
    Menu,
    Level1
}

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
    public Dictionary<GameObject, Health> HealthContainer;
    public Dictionary<GameObject, Coin> CoinContainer;
    public Dictionary<GameObject, BuffReciever> BuffRecieverContainer;
    public ItemDB ItemDB;
    public Dictionary<GameObject, ItemComponent> ItemContainer;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject deathPanel;

    public void InventoryOpenOrClose()
    {
        OpenOrClosePanel(PanelsEnum.Inventory);
    }
    public void SettingsOpenOrClose()
    {
        OpenOrClosePanel(PanelsEnum.Settings);
    }

    public void DeathScreenOpen()
    {
        OpenOrClosePanel(PanelsEnum.DeathScreen);
    }

    public void ExitToMenu()
    {
        int previousScore = 0;
        if (PlayerPrefs.HasKey(StorageConstants.PLAYER_SCORE_KEY))
        {
            previousScore = PlayerPrefs.GetInt(StorageConstants.PLAYER_SCORE_KEY);
        }
        int currentScore = PlayerInventory.Instance.coinsCount;
        if (previousScore < currentScore)
        {
            PlayerPrefs.SetInt(StorageConstants.PLAYER_SCORE_KEY, currentScore);
        }
        SceneManager.LoadScene((int)ScenesEnum.Menu);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(scene.name);
    }

    public void Finish()
    {
        //Show some animation
        ExitToMenu();
    }

    private void Awake()
    {
        Instance = this;
        HealthContainer = new Dictionary<GameObject, Health>();
        CoinContainer = new Dictionary<GameObject, Coin>();
        BuffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
        ItemContainer = new Dictionary<GameObject, ItemComponent>();
    }

    private void OpenOrClosePanel(PanelsEnum panelType)
    {
        if (panelType == PanelsEnum.DeathScreen)
        {
            deathPanel.SetActive(true);
        }

        settingsPanel.SetActive(panelType == PanelsEnum.Settings &&
            !settingsPanel.activeSelf);
        inventoryPanel.SetActive(panelType == PanelsEnum.Inventory &&
            !inventoryPanel.activeSelf);

        bool isPaused = settingsPanel.activeSelf || inventoryPanel.activeSelf;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
