using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLineScript : MonoBehaviour
{
    [SerializeField] GameManagerScript GM;
    void Start()
    {
        if (GM == null)
            GM = FindObjectOfType<GameManagerScript>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GM.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
