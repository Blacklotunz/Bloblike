﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI, gameOverMenuUI;
    public RoomTemplates roomTemplateToSave;

    private void Start()
    {
        GameEvents.current.onPlayerDie += GameOver;
        roomTemplateToSave = GameObject.Find("RoomTemplates").GetComponent<RoomTemplates>();
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
        LevelMap.levelMap = new List<GameObject>();
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

    public void Save()
    {
        FileManager.WriteToFile("SaveFile.json", JsonUtility.ToJson(roomTemplateToSave));
    }

    public void Load()
    {
        string LevelState;
        FileManager.LoadFromFile("SaveFile.json", out LevelState);
        Debug.Log(LevelState);
       
    }

   

}
