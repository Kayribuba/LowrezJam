using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    [SerializeField] Sprite[] foodSprites;
    [SerializeField] SpriteRenderer sr;
    void Start()
    {
        float randomNum = Random.Range(0, foodSprites.Length - 1);
        Debug.Log(randomNum);
        
        if (foodSprites.Length >= 1)
        {
            switch (randomNum)
            {
                case 0:
                    sr.sprite = foodSprites[0];
                    break;
                case 1:
                    sr.sprite = foodSprites[1];
                    break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
