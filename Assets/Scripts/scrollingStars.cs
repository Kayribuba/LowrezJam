using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class scrollingStars : MonoBehaviour
{
    [SerializeField] float speed;

    int ID;
    float targetMoveTime;
    float PCHeight;
    float gridsize = .15625f;
    bool newStarSpawned;
    Vector3 PCPos;
    SpriteRenderer sr;
    ParallaxCode PC;

    void Start()
    {
        targetMoveTime = Time.time;

        if (FindObjectOfType<GridSizer>() != null)
            gridsize = FindObjectOfType<GridSizer>().GetGridSize();

        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(targetMoveTime <= Time.time)
        {
            transform.position += gridsize * Vector3.down;
            targetMoveTime = Time.time + 1 / speed;

            if (PCPos.y + PCHeight / 2 >= sr.sprite.bounds.extents.y * transform.lossyScale.y + transform.position.y && !newStarSpawned)
            {
                PC.SpawnNewStar(ID, true);
                newStarSpawned = true;
            }
            if(PCPos.y - PCHeight / 2 >= transform.position.y + sr.sprite.bounds.extents.y * transform.lossyScale.y)
            {
                Destroy(gameObject);
            }
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(PCHeight);
        }
    }

    public void SetStart(float speed, ParallaxCode PC, int ID)
    {
        this.speed = speed;
        this.PC = PC;
        PCHeight = PC.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y * PC.transform.lossyScale.y;
        PCPos = PC.transform.position;
        this.ID = ID;
    }
}
