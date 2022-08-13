using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxCode : MonoBehaviour
{
    [SerializeField] GameObject[] spritesToMove;

    void Start()
    {

        GameObject GO = Instantiate(spritesToMove[0], transform.position, Quaternion.identity);
        float firstGridY = GO.transform.position.y - gameObject.transform.lossyScale.y / 2;
        GO.transform.position = new Vector3(transform.position.x, firstGridY, -10);

        //int i = 1;
        //foreach(GameObject sprite in spritesToMove)
        //{
        //    float targetSize = transform.localScale.x / sprite.transform.localScale.x;
        //    sprite.transform.localScale *= targetSize;

        //    GameObject GOInstantiated = Instantiate(sprite, transform.position, Quaternion.identity);

        //    Debug.Log(GOInstantiated.transform.lossyScale + " " + GOInstantiated.transform.localScale);

        //Vector2 targetLocation = transform.position;
        //targetLocation.y -= transform.lossyScale.y / 2;
        //targetLocation.y += sprite.transform.lossyScale.y / 2;
        //GOInstantiated.transform.position = targetLocation;

        //    GOInstantiated.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<SpriteRenderer>().sortingLayerName;
        //    GOInstantiated.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + i;
        //    i++;
    //   }
}
    void Update()
    {
        
    }
}
