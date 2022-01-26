using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
   
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
    
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("M PRESSED!");

            if (GameIsPaused)
            {               
                Debug.Log("Game PAUSED!");

                Resume();
            }
            else
            {
                Debug.Log("Game RESUMED!");

                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  
        optionMenuUI.SetActive(false);  
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void LoadMenu()
    {
        Debug.Log("Loading!");

    }
    
}
