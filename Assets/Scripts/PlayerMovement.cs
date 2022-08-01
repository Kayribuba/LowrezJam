using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject bg;
    [SerializeField] float interval = 0.01f;

    Vector2 bgTopLeft;
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
            Vector2 targetPosition = transform.position;

            if (Input.GetKey(KeyCode.D))
                targetPosition.x += gridSize;
            else if (Input.GetKey(KeyCode.A))
                targetPosition.x -= gridSize;

            if (Input.GetKey(KeyCode.W))
                targetPosition.y += gridSize;
            else if (Input.GetKey(KeyCode.S))
                targetPosition.y -= gridSize;

            transform.position = targetPosition;

            targetTime = Time.time + interval;
        }
    }

}
