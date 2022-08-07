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
                case 2:
                    sr.sprite = foodSprites[2];
                    break;
                case 3:
                    sr.sprite = foodSprites[3];
                    break;
                case 4:
                    sr.sprite = foodSprites[4];
                    break;
                case 5:
                    sr.sprite = foodSprites[5];
                    break;
                case 6:
                    sr.sprite = foodSprites[6];
                    break;
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
