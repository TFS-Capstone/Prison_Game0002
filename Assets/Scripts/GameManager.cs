﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //variables ---------------------------------------


    //whether the game is paused
    public static bool GameIsPause = false;
    //the pause menu GameObject
    public GameObject PauseMenuUI;
    //instance for the GameManager
    static GameManager _instance = null;


    //end of variables --------------------------------

    //creates the GameManager instance
    public static GameManager instance
    {
        get { return _instance; }
        set { instance = value; }
    }

    //start with don't destroy on load
    void Start()
    {
        if (_instance)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

    }

    // Update has pause menu checks
    void Update()
    {
        //checking if correct scene is active before being able to pause
        if (SceneManager.GetActiveScene().name == "Sheraaz" || SceneManager.GetActiveScene().name == "Alex")
        { 
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (GameIsPause)
                    Resume();
                else
                    Pause();
            }
        
        }
    }

    //pause game
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPause = true;
    }
    //resume game
    public void Resume()
    {
        Time.timeScale = 1;
        GrabPauseMenu();
        PauseMenuUI.SetActive(false);
        
        GameIsPause = false;
    }

    //quit the game
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void win()
    {
        Debug.Log("win");

    }
    //loads the lose scene
    public void lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    //lose scene go back to title scene
    public void Restart()
    {
        SceneManager.LoadScene("Title");
    }
    //pause menu go back to title scene
    public void Mainmenu()
    {
        SceneManager.LoadScene("Title");
    }
    //main menu start game and find the UI
    public void StartGame()
    {
        SceneManager.LoadScene("Sheraaz");
        PauseMenuUI = GameObject.Find("Canvas");
    }
    //grabs the pause menu for the GameManager
    public void GrabPauseMenu()
    {
        PauseMenuUI = GameObject.FindGameObjectWithTag("Pause");
        PauseMenuUI.SetActive(false);
        GameIsPause = false;
        Time.timeScale = 1;
    }
    //template for GameManager variables
    /*
     //this is a template for a variable
    public int health
    {
        get { return _health; }
        set {_health = value;}
    }
    */


}
