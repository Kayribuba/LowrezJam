using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSizer : MonoBehaviour
{
    float gridSize;

    void Awake()
    {
        gridSize = gameObject.transform.localScale.x / 64;
    }

    public float GetGridSize()
    {
        return gridSize;
    }
}
