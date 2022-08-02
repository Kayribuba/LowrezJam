using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public Vector2 MoveDir;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;

    void Start()
    {
        rb.velocity = MoveDir * speed;
    }
}
