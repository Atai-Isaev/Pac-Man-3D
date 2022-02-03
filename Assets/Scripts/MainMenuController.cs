using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;
    public static bool GameIsWon = false;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject restartMenuUI;
    public GameObject optionsButton;
    public GameObject quitMenuUI;
    public GameObject resumeMenuUI;
    public GameObject gameOverText, gameWinText;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }


    void Update()
    {
  
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("M PRESSED!");

            if (GameIsPaused)
            {
                Debug.Log(GameIsPaused + " Game RESUMED!");

                Resume();
            }
            else
            {
                Debug.Log(GameIsPaused + "Game PAUSED!");

                Pause();
            }
        }
    }

    public void GameOverMenu()
    {
        if (GameIsOver)
        {
            gameOverText.SetActive(true);
            gameWinText.SetActive(false);
        }
        else if(GameIsWon)
        {
            gameWinText.SetActive(true);
            gameOverText.SetActive(false);
        }
        pauseMenuUI.SetActive(true);
        restartMenuUI.SetActive(true);
        resumeMenuUI.SetActive(false);
        optionsButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  
        optionMenuUI.SetActive(false);  
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void LoadMenu()
    {
        Debug.Log("Loading!");
    }
}