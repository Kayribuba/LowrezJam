using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] int treshold1 = 4;
    [SerializeField] int treshold2 = 2;
    [SerializeField] Animator animator;

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

            CompareTreshold((int)health);

            Debug.Log($"Enemy {gameObject.name} lot health. Remaining health : {maxHealth}/{health}.");

            if (health <= 0)
            {
                Die();
            }
        }
    }

    void CompareTreshold(int health)
    {
        if(health <= treshold1 && health > treshold2)
        {
            animator.SetInteger("Phase", 2);
        }
        if (health <= treshold2 && health > 0)
        {
            animator.SetInteger("Phase", 3);
        }
        if (health <= 0)
        {
            animator.SetInteger("Phase", 4);
        }

    }

    void Die()
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
