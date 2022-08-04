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
            collision.GetComponent<StationaryEnemyHealthScript>()?.TakeDamage();
            DestroyBullet();
        }
    }

    public void SetFlightVector(Vector2 vector) => MoveDir = vector;

    private void DestroyBullet()
    {
        if (popAnim != null)
            Instantiate(popAnim, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
