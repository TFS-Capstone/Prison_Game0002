using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    // Start is called before the first frame update
    public static GameManager instance
    {
        get { return _instance; }
        set { instance = value; }
    }
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

    // Update is called once per frame
    void Update()
    {

    }

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

    public void lose()
    {
        Debug.Log("lose");

    }


    /*
     //this is a template for a variable
    public int health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (healthText)
            {
                healthText.text = _health.ToString();
            }
        }

    }
    */

}
