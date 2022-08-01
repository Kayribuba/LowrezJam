using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] GameObject cube;
    [SerializeField] Transform pikselParent;
    [SerializeField] Sprite bg;

    Vector2 gridSize = new Vector2(64, 64);

    private void Start()
    {
        gridSize.x = background.transform.lossyScale.x / 64;
        gridSize.y = background.transform.lossyScale.y / 64;

        DrawCubes();
    }

    void DrawCubes()
    {
        Color colora = Color.red;

        float firstGridY = background.transform.position.y + background.transform.lossyScale.y / 2 - gridSize.y / 2;
        float firstGridX = background.transform.position.x - background.transform.lossyScale.x / 2 + gridSize.x / 2;

        for (int y = 0; y < 64; y++)
        {
            for (int x = 0; x < 64; x++)
            {
                cube.GetComponent<SpriteRenderer>().color = colora;
                cube.transform.localScale = gridSize;

                Instantiate(cube, new Vector3(firstGridX, firstGridY), Quaternion.identity, pikselParent);

                firstGridX += gridSize.x;

                colora.r -= 1f / 64f;
            }

            colora.r = 1;
            colora.g += 1f / 64f;

            firstGridX = background.transform.position.x - background.transform.lossyScale.x / 2 + gridSize.x / 2;
            firstGridY -= gridSize.y;
        }
    }
}
