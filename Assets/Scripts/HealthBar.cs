using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    private float displayHealth;
    private Player player;
    [SerializeField] private float deltaHealth;
    void Start()
    {
        player = Player.Instance;
        displayHealth = (float)player.health.CurrentHealth / 100;
        StartCoroutine(CulculateHealth());
    }

    private IEnumerator CulculateHealth()
    {
        while (true)
        {
            float actualHealth = (float)player.health.CurrentHealth / 100;
            if (actualHealth > displayHealth)
            {
                displayHealth += deltaHealth;
            }
            if (actualHealth < displayHealth)
            {
                displayHealth -= deltaHealth;
            }
            if (Mathf.Abs(actualHealth - displayHealth) < deltaHealth)
            {
                displayHealth = actualHealth;
            }
            health.fillAmount = displayHealth;
            yield return null;
        }
    }
}
