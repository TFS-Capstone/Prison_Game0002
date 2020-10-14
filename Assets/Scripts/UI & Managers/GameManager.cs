using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //variables ---------------------------------------
    //if the player is disguised
    public bool disguised = false;
    //the type of keycard the player is holding
    public int keycardType = 0;
    //whether the game is paused
    public  bool GameIsPause = false;
    //instance for the GameManager
    static GameManager _instance = null;

    public float playerSpeed;



    //player objects
    bool PlayerType = true;
    public GameObject Player;
    public GameObject Car;
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
        if (SceneManager.GetActiveScene().Equals("CurrentWhitebox"))
            Player.SetActive(false);
    }

    // Update has pause menu checks
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(!PlayerType)
            {
                Car.SetActive(true);
                Player.SetActive(false);
                PlayerType = true;
            }
            else
            {
                Car.SetActive(false);
                Player.SetActive(true);
                PlayerType = false;
            }
        }

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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Win");

    }
    //loads the lose scene
    public void lose()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver");
    }
    //lose scene go back to title scene
    public void Restart()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Title");
    }
    //pause menu go back to title scene
    public void Mainmenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Title");
    }
    //main menu start game and find the UI
    public void StartGame()
    {
        SceneManager.LoadScene("CurrentWhitebox");

    }
    //grabs the pause menu for the GameManager
    

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
