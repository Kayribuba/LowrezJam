using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StationaryEnemyHealthScript : MonoBehaviour
{
    [SerializeField] int treshold1 = 4;
    [SerializeField] int treshold2 = 2;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource AS;

    [SerializeField] int maxHealth = 6;
    [SerializeField] bool destroyAfterDeath = false;

    int health;
    bool dead;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage()
    {
        if (!dead)
        {
            if (health > 0)
            {
                AS.Play();
                Debug.Log(AS.clip.name);
            }
            health--;

            CompareTreshold(health);

            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void RefreshHouse()
    {
        dead = false;
        health = maxHealth;
        animator.SetInteger("Phase",1);
        if (GetComponent<StaEnAttack>() != null)
            GetComponent<StaEnAttack>().enabled = true;
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

        if (GetComponent<StaEnAttack>() != null)
            GetComponent<StaEnAttack>().enabled = false;
        
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
