using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public GameObject parentGO;
    public int waterCost = 1;
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
            DestroyBullet();

        if (moveTargetTime <= Time.time)
        {
            transform.position += (Vector3)movement;
            CheckCollisions();
            moveTargetTime = Time.time + waitTime;
        }
    }

    void CheckCollisions()
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size * 1.25f, 0);

        foreach (Collider2D collision in collisions)
        {
            if (collision.gameObject != parentGO && parentGO != null)
            {
                if (collision.CompareTag("Wall"))
                {
                    DestroyBullet();
                }
                else if (collision.CompareTag("Enemy"))
                {
                    collision.GetComponent<StationaryEnemyHealthScript>()?.TakeDamage();
                    DestroyBullet();
                }
            }
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size * 1.25f);
    //}

    public void SetFlightVector(Vector2 vector, float gridSize)
    {
        if(Mathf.Abs(vector.x) > 0 && Mathf.Abs(vector.y) > 0)
        {
            vector /= 0.7f;
            //waitTime /= 0.7f;
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
