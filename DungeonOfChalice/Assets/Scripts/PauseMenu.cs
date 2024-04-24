using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    Canvas canvas;
    private bool isPaused = false; 
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            canvas.enabled = true;
            Time.timeScale = 0; 
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            canvas.enabled = false;
            Time.timeScale = 1; 
            isPaused = false;
        }
    }

    public void Resume()
    {
        canvas.enabled = false;
        Time.timeScale = 1;
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
