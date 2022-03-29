using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] InputField name;
    private const string playerNameKey = "Player_Name";

    void Start()
    {
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            name.text = PlayerPrefs.GetString(playerNameKey);
        }
    }
    public void OnEditName()
    {
        PlayerPrefs.SetString(playerNameKey, name.text);
    }
    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
