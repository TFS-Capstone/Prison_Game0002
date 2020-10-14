using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Car = GameObject.Find("Car");
        GameManager.instance.Player = GameObject.Find("player");
        GameManager.instance.Player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
