using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.MaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth == 0)
        {
            GameOver();
        }
    }

    void Damage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void GameOver()
    {
        Debug.Log("GG");
    }
}
