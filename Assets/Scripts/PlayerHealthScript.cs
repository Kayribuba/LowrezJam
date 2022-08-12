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
    [SerializeField] Transform CP;

    int health;
    GameObject dieAnimInstantiated;

    void Start()
    {
        if (GM == null && FindObjectOfType<GameManagerScript>() != null)
            GM = FindObjectOfType<GameManagerScript>();

        CP = GameObject.FindGameObjectWithTag("CheckPointer")?.transform;

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
            dieAnimInstantiated = Instantiate(DieAnimation, transform.position, Quaternion.identity);
        }

        if (CP == null)
            GM.EndGame();
        else
            GM.EndGame(3f);
    }

    public void RespawnP()
    {
        Destroy(dieAnimInstantiated);
        transform.position = CP.position;
    }
}
