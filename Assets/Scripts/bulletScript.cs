using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public Vector2 MoveDir;
    [SerializeField] GameObject popAnim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    [SerializeField] float bulletLifespan = 10f;
    [SerializeField] bool isEnemyBullet;

    float deathTargetTime = float.MaxValue;

    void Start()
    {
        deathTargetTime = Time.time + bulletLifespan;
    }
    void Update()
    {
        if (deathTargetTime <= Time.time)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != gameObject)
        {
            if (collision.CompareTag("Wall"))
            {
                DestroyBullet();
            }
            else if (collision.CompareTag("Enemy"))
            {
                if (!isEnemyBullet)
                    collision.GetComponent<StationaryEnemyHealthScript>()?.TakeDamage();

                DestroyBullet();
            }
            else if (collision.CompareTag("Player") && isEnemyBullet)
            {
                if (isEnemyBullet)
                    //deal damage to the player script

                    DestroyBullet();
            }
        }
    }

    public void SetFlightVector(Vector2 vector)
    {
        MoveDir = vector;
        rb.velocity = MoveDir * speed;
    }

    private void DestroyBullet()
    {
        if (popAnim != null)
            Instantiate(popAnim, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
