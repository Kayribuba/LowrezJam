using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] GameManagerScript GM;
    [SerializeField] GameObject DieAnimation;
    [SerializeField] Slider HealthBar;
    [SerializeField] int maxHealth = 6;

    int health;

    void Start()
    {
        if (GM == null && GetComponent<GameManagerScript>() != null)
            GM = GetComponent<GameManagerScript>();

        health = maxHealth;
        HealthBar.maxValue = maxHealth;
        HealthBar.value = maxHealth;
    }

    public void RecieveDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            health = 0;
            HealthBar.value = health;
            Die();
        }

        HealthBar.value = health;
    }

    public void Heal()
    {
        health = maxHealth;
        HealthBar.value = health;
    }

    void Die()
    {
        if (DieAnimation != null)
        {
            Instantiate(DieAnimation, transform.position, Quaternion.identity);
        }

        GM.EndGame(3f);
    }
}
