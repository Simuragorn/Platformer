using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start()
    {
        GameManager.Instance.CoinContainer.Add(gameObject, this);
    }

    public void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
        GameManager.Instance.CoinContainer.Remove(gameObject);
    }

    public void EndDestroy()
    {
        Destroy(gameObject);
    }
}
