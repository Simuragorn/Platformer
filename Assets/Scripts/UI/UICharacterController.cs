using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterController : MonoBehaviour
{
    [SerializeField] private PressedButton left;
    [SerializeField] private PressedButton right;
    [SerializeField] private Button fire;
    [SerializeField] private Button jump;

    public PressedButton Left => left;
    public PressedButton Right => right;
    public Button Fire => fire;
    public Button Jump => jump;

    private void Start()
    {
        Player.Instance.Init(this);
    }
}
