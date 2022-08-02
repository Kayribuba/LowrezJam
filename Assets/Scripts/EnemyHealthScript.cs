using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] float maxHealth = 6f;
    [SerializeField] bool destroyAfterDeath = true;

    float health;
    bool dead;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage()
    {
        if (!dead)
        {
            health--;
            Debug.Log($"Enemy {gameObject.name} lot health. Remaining health : {maxHealth}/{health}.");

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        dead = true;
        Debug.Log($"Enemy {gameObject.name} has died.");

        if (destroyAfterDeath)
        {
            DestroyEnemy();
        }
    }
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
