using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeStars : MonoBehaviour
{
    float gridSize = 0.15625f;
    public float speedInterval = 0.01f;
    [SerializeField] GameObject starPrefab;
    public bool canspawn = true;
    [SerializeField] float deathTimer;
    Vector2 tp;
    Vector2 sp;
    void Start()
    {
        InvokeRepeating("Down", 0, speedInterval);
        tp = transform.position;
        canspawn = true;
        Destroy(gameObject, deathTimer);

    }

    // Update is called once per frame
    void Update()
    {
        sp = new Vector3(transform.position.x, transform.position.y + 40, transform.position.y);
        if (transform.position.y == -15 && canspawn)
        {
            canspawn = false;
            Instantiate(starPrefab,sp ,Quaternion.identity);
        }
    }
    private void Down()
    {
        tp.y -= gridSize;
        transform.position = tp;
    }
}
