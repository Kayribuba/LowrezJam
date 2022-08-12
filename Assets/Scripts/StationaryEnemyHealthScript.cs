using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StationaryEnemyHealthScript : MonoBehaviour
{
    public event EventHandler<int> PhaseChangedEvent;

    [SerializeField] int[] tresholds = new int[] { 4, 2 };

    [SerializeField] Animator animator;
    [SerializeField] AudioSource AS;

    [SerializeField] int maxHealth = 6;
    [SerializeField] bool destroyAfterDeath = false;

    [SerializeField] int health;
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
                AS.Play();

            health--;

            CompareTreshold(health);

            if (health <= 0)
                Die();
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
        for(int i = 0; i < tresholds.Length; i++)
        {
            int nextT = 0;

            if (i < tresholds.Length - 1)
                nextT = tresholds[i + 1];

            if (health <= tresholds[i] && health > nextT)
                ChangePhase(i + 2);
            if (health <= 0)
                ChangePhase(tresholds.Length + 2);
        }
    }
    void ChangePhase(int changeTo)
    {
        animator.SetInteger("Phase", changeTo);
        PhaseChangedEvent?.Invoke(this, changeTo);
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
