using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    [SerializeField] Sprite[] foodSprites;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] AudioSource AS;
    [SerializeField] GameObject CP;

    private bool boshish = true;
    void Start()
    {
        float randomNum = Random.Range(0, foodSprites.Length);
        CP = GameObject.FindGameObjectWithTag("CheckPointer");
        
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
        if (collision.gameObject.CompareTag("Player") && boshish)
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().Heal();
            boshish = false;
            AS.Play();
            sr.sprite = null;
            CP.transform.position = transform.position;
            Destroy(gameObject, 1);
        }
    }
}
