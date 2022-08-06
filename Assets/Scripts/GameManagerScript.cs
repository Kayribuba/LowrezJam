using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Level { Previous, Next, Current}

public class GameManagerScript : MonoBehaviour
{
    public event EventHandler<bool> GamePauseEvent;
    public bool gameIsPaused { get; private set; } = true;

    int currentSceneIndex;
    float targetEndRealTime = float.MaxValue;
    bool gameEnded;


    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if (targetEndRealTime <= Time.realtimeSinceStartup)
            ReloadLevel();

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
        GamePauseEvent?.Invoke(this, true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void UnPauseGame()
    {
        GamePauseEvent?.Invoke(this, false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}