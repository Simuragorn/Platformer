using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] InputField name;

    void Start()
    {
        if (PlayerPrefs.HasKey(StorageConstants.PLAYER_NAME_KEY))
        {
            name.text = PlayerPrefs.GetString(StorageConstants.PLAYER_NAME_KEY);
        }
    }
    public void OnEditName()
    {
        PlayerPrefs.SetString(StorageConstants.PLAYER_NAME_KEY, name.text);
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
