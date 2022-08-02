using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject bg;
    [SerializeField] float interval = 0.01f;

    Vector2 bgTopLeft;
    Vector2 targetPosition;
    float gridSize;
    float targetTime = float.MinValue;

    void Start()
    {
        gridSize = bg.GetComponent<GridSizer>().GetGridSize();

        bgTopLeft.x = bg.transform.position.x - bg.transform.lossyScale.x / 2;
        bgTopLeft.y = bg.transform.position.y + bg.transform.lossyScale.y / 2;

        Vector2 nextPos = bgTopLeft;
        nextPos.x += transform.lossyScale.x / 2;
        nextPos.y -= transform.lossyScale.y / 2;
        transform.position = nextPos;
    }
    private void Update()
    {
        if (targetTime < Time.time)
        {
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
            }

                transform.position = targetPosition;
                targetTime = Time.time + interval;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetPosition, transform.lossyScale * 0.9f);
    }
}
