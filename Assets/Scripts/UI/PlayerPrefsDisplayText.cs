using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ValueType
{
    Int,
    Float,
    String
}

[RequireComponent(typeof(Text))]
public class PlayerPrefsDisplayText : MonoBehaviour
{
    [SerializeField] private StorageConstants.PlayerPrefsEnum playerPref;
    [SerializeField] private ValueType valueType;

    [SerializeField] private string prefixIfValueExists;
    [SerializeField] private string noValueContent;
    private Text textComponent;
    void Start()
    {
        string key = StorageConstants.GetKeyByPlayerPrefs(playerPref);
        textComponent = GetComponent<Text>();
        string value = null;
        if (PlayerPrefs.HasKey(key))
        {
            switch (valueType)
            {
                case ValueType.Int:
                    value = PlayerPrefs.GetInt(key).ToString();
                    break;
                case ValueType.Float:
                    value = PlayerPrefs.GetFloat(key).ToString();
                    break;
                case ValueType.String:
                    value = PlayerPrefs.GetString(key).ToString();
                    break;
                default:
                    break;
            }
        }
        textComponent.text = noValueContent;
        if (!string.IsNullOrWhiteSpace(value))
        {
            textComponent.text = prefixIfValueExists + value;
        }
    }
}
