﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlsUI;
    // Update is called once per frame
    void Update()
    {

        GameManager.instance.GameIsPause = isPaused;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (!isPaused && GameManager.instance.chatMenu.activeSelf == false)
            {
                Pause();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (!isPaused && GameManager.instance.chatMenu.activeSelf == true)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if(Input.GetKeyDown(KeyCode.T) && !isPaused && GameManager.instance.chatMenu.activeSelf == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        controlsUI.SetActive(false);
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("Title");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Controls()
    {
        pauseMenuUI.SetActive(false);
        controlsUI.SetActive(true);
    }

    public void Return()
    {
        pauseMenuUI.SetActive(true);
        controlsUI.SetActive(false);
    }
}
