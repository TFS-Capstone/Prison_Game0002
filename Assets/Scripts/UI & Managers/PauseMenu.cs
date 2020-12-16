using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlsUI;
    [HideInInspector]
    public bool playerIsInCams;

    [SerializeField]
    AudioSource music = null;
    [SerializeField]
    AudioSource ambience = null;

  

    float ambienceBaseVolume = 0;
    float musicBaseVolume = 0;

    float volumeMultiplier = 1;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ambienceBaseVolume = ambience.volume;
        musicBaseVolume = music.volume;
        
    }



    // Update is called once per frame
    void Update()
    {
        music.volume = musicBaseVolume * volumeMultiplier;
        ambience.volume = ambienceBaseVolume * volumeMultiplier;
        playerIsInCams = GameManager.instance.playerInCams;
        GameManager.instance.GameIsPause = isPaused;
        if (Input.GetKeyDown(KeyCode.Escape) && !playerIsInCams)
        {
            if(isPaused)
            {
                Resume();
                volumeMultiplier = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                volumeMultiplier = 0.5f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            
        }
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        controlsUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
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
