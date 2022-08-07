using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gridSize;

    [SerializeField] GameObject bg;
    [SerializeField] float interval = 0.01f;
    [SerializeField][Range(0,100)] float ReloadSpeedPercent = 50f;

    Vector2 bgTopLeft;
    Vector2 targetPosition;
    float targetTime = float.MinValue;
    bool isReloading;

    void Start()
    {
        if (GetComponent<PlayerAttack>() != null)
            GetComponent<PlayerAttack>().ReloadEvent += PlayerMovement_ReloadEvent;

        gridSize = bg.GetComponent<GridSizer>().GetGridSize();
        GetComponent<PlayerAttack>()?.SetGridSize(gridSize);

        bgTopLeft.x = bg.transform.position.x - bg.transform.lossyScale.x / 2;
        bgTopLeft.y = bg.transform.position.y + bg.transform.lossyScale.y / 2;

        Vector2 nextPos = bgTopLeft;
        nextPos.x += transform.lossyScale.x / 2;
        nextPos.y -= transform.lossyScale.y / 2;
        transform.position = nextPos;
    }

    private void PlayerMovement_ReloadEvent(object sender, bool e)
    {
        isReloading = e;
    }

    private void Update()
    {
        if (targetTime < Time.time)
        {
            float speedInterval = isReloading ? interval / (ReloadSpeedPercent / 100) : interval;

            targetPosition = transform.position;

            if (Input.GetKey(KeyCode.D))
                targetPosition.x += gridSize;
            else if (Input.GetKey(KeyCode.A))
                targetPosition.x -= gridSize;

            //,

            Collider2D[] collidedObjects = Physics2D.OverlapBoxAll(targetPosition, transform.lossyScale * 0.9f, 0);

            foreach (Collider2D col in collidedObjects)
            {
                if (col.CompareTag("Wall"))
                {
                    targetPosition.x = transform.position.x;
                }
                else if (col.CompareTag("Enemy"))
                {
                    targetPosition.x = transform.position.x;
                    //31
                }
            }

            collidedObjects = null;
            //

            if (Input.GetKey(KeyCode.W))
                targetPosition.y += gridSize;
            else if (Input.GetKey(KeyCode.S))
                targetPosition.y -= gridSize;

            collidedObjects = Physics2D.OverlapBoxAll(targetPosition, transform.lossyScale * 0.9f, 0);

            foreach(Collider2D col in collidedObjects)
            {
                if (col.CompareTag("Wall"))
                {
                    targetPosition.y = transform.position.y;
                }
                else if (col.CompareTag("Enemy"))
                {
                    targetPosition.y = transform.position.y;
                    //31
                }
            }

            transform.position = targetPosition;
            targetTime = Time.time + speedInterval;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetPosition, transform.lossyScale * 0.9f);
    }
}
