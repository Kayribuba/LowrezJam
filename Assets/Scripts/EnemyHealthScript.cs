using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] AnimationClip[] animations;
    [SerializeField] Animation anim;

    [SerializeField] float maxHealth = 6f;
    [SerializeField] bool destroyAfterDeath = true;

    float health;
    int animPhase;
    bool dead;

    void Start()
    {
        health = maxHealth;

        for(int i = 0; i < animations.Length; i++)
        {
            anim.AddClip(animations[i], "Phase" + i);
        }
    }

    public void TakeDamage()
    {
        if (!dead)
        {
            anim.Play("Phase"+animPhase);
            if(animPhase < animations.Length)
            animPhase++;

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
