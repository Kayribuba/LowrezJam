using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyBulletScript : MonoBehaviour
{
    public GameObject parentGO;
    [SerializeField] GameObject popAnim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float waitTime = 0.1f;
    [SerializeField] float bulletLifespan = 10f;

    float deathTargetTime = float.MaxValue;
    float moveTargetTime = float.MaxValue;
    Vector2 movement;

    void Start()
    {
        deathTargetTime = Time.time + bulletLifespan;
    }
    void Update()
    {
        if (deathTargetTime <= Time.time)
            Destroy(gameObject);

        if (moveTargetTime <= Time.time)
        {
            transform.position += (Vector3)movement;
            CheckCollisions();
            moveTargetTime = Time.time + waitTime;
        }
    }

    void CheckCollisions()
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().transform.lossyScale, 0);

        foreach (Collider2D collision in collisions)
        {
            if (collision.gameObject != parentGO && parentGO != null)
            {
                if (collision.CompareTag("Wall") || collision.CompareTag("Enemy"))
                {
                    DestroyBullet();
                }
                else if (collision.CompareTag("Player"))
                {
                    collision.GetComponent<PlayerHealthScript>()?.RecieveDamage(1);
                    DestroyBullet();
                }
            }
        }
    }

    public void SetFlightVector(Vector2 vector, float gridSize)
    {

        if (Mathf.Abs(vector.x) > 0 && Mathf.Abs(vector.y) > 0)
        {
            vector /= 0.7f;
            waitTime /= 0.7f;
        }

        movement = vector * gridSize;
        moveTargetTime = Time.time + waitTime;
    }
    public void SetParentGO(GameObject parentObject)
    {
        parentGO = parentObject;
    }

    private void DestroyBullet()
    {
        if (popAnim != null)
            Instantiate(popAnim, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
