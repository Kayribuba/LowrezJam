using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class bulletScript : MonoBehaviour
{
    public Vector2 MoveDir;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    [SerializeField] float bulletLifespan = 10f;

    [SerializeField] Animator animator;
    [SerializeField] Animation animClip;

    float deathTargetTime = float.MaxValue;

    void Start()
    {
        rb.velocity = MoveDir * speed;

        deathTargetTime = Time.time + bulletLifespan;
    }
    void Update()
    {
        if (deathTargetTime <= Time.time)
            DestroyBullet();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            DestroyBullet();
        }
        else if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealthScript>()?.TakeDamage();
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
