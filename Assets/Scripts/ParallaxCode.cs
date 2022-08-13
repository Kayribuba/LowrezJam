using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] struct SpritesToMove
{
    public GameObject spriteGO;
    [Range(0,1000)] public float speed;
}

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxCode : MonoBehaviour
{
    [SerializeField] SpritesToMove[] spritesToMove;

    Vector2 SpawnPoint;
    SpriteRenderer spriteRenderer;
    float gridSize = .15625f;

    void Start()
    {
        if (FindObjectOfType<GridSizer>() != null)
            gridSize = FindObjectOfType<GridSizer>().GetGridSize();

        spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < spritesToMove.Length; i++)
        {
            SpawnNewStar(i, false);
        }
    }
    public void SpawnNewStar(int ID, bool atTop)
    {
        Vector3 targetPosition = transform.position;

        if (atTop)
            targetPosition.y += spriteRenderer.sprite.bounds.extents.y * transform.lossyScale.y;
        else
            targetPosition.y -= spriteRenderer.sprite.bounds.extents.y * transform.lossyScale.y;

        targetPosition.y += spritesToMove[ID].spriteGO.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * transform.lossyScale.y;

        GameObject GOInstantiated = Instantiate(spritesToMove[ID].spriteGO, targetPosition, Quaternion.identity);

        GOInstantiated.transform.localScale = transform.localScale;
        GOInstantiated.GetComponent<scrollingStars>().SetStart(spritesToMove[ID].speed, this, ID);
    }
}
