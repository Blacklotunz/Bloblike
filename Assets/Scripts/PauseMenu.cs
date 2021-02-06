﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI, gameOverMenuUI;

    private void Start()
    {
        GameEvents.current.onPlayerDie += GameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverMenuUI.activeInHierarchy)
            {
                return;
            }

            if (GameIsPaused)
            {
                this.Resume();
            }
            else
            {
                this.Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }

    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        LevelMap.levelMap = new List<DictionaryEntry>();
        SceneManager.LoadScene(1);
    }


    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        LevelMap.ResetMap();
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
