using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Level { Previous, Next, Current}

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;

    public event EventHandler<bool> GamePauseEvent;
    public bool gameIsPaused { get; private set; } = true;

    int currentSceneIndex;
    public float targetEndRealTime = float.MaxValue;
    bool gameEnded;

    public float deneme;

    [SerializeField] GameObject player;

    private void Awake()
    {
        pauseCanvas = GameObject.Find("PauseCanvas");
    }
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        deneme = Time.realtimeSinceStartup;
        if (targetEndRealTime <= Time.realtimeSinceStartup)
        {
            //ReloadLevel();
            Respawn();
            targetEndRealTime = float.MaxValue;
        }
            

        if (!gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (gameIsPaused)
                    UnPauseGame();
                else
                    PauseGame();

                gameIsPaused = !gameIsPaused;
            } 
        }
    }


    public void EndGame()
    {
        gameEnded = true;
        ReloadLevel();
    }
    public void EndGame(float afterSeconds)
    {
        gameEnded = true;
        PauseGame();
        targetEndRealTime = Time.realtimeSinceStartup + afterSeconds;
    }
    public void LoadLevel(int buildIndex)
    {
        UnPauseGame();

        SceneManager.LoadScene(buildIndex);
    }
    public void LoadLevel(Level level)
    {
        UnPauseGame();

        switch (level)
        {
            case Level.Previous:
                if (SceneManager.GetSceneAt(currentSceneIndex - 1).IsValid())
                    SceneManager.LoadScene(currentSceneIndex - 1);
                else
                    Debug.Log("No previous scene can be found.");
                break;

            case Level.Next:
                if (SceneManager.GetSceneAt(currentSceneIndex + 1).IsValid())
                    SceneManager.LoadScene(currentSceneIndex + 1);
                else
                    Debug.Log("No next scene can be found.");
                break;

            case Level.Current:
                SceneManager.LoadScene(currentSceneIndex);
                break;
        }
    }
    void ReloadLevel()
    {
        UnPauseGame();
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void PauseGame()
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(true);
        }
        GamePauseEvent?.Invoke(this, true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void UnPauseGame()
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        }
        GamePauseEvent?.Invoke(this, false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    void Respawn()
    {
        Debug.Log("respawn");
        targetEndRealTime = float.MaxValue;
        UnPauseGame();
        gameEnded = false;
        player.GetComponent<PlayerHealthScript>().Heal();
        player.GetComponent<PlayerHealthScript>().RespawnP();
    }
}
