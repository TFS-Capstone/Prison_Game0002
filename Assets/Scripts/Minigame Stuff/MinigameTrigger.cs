using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    
    RaycastHit hit;
    public GameObject minigameUI;
    public MainBlockNode node;
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public GameObject player;
    public GameObject minigame;
    public GameObject minigame2;

    // Start is called before the first frame update
    void Start()
    {
        minigameUI.SetActive(false);
        camera2.enabled = false;
        minigame.SetActive(false);
        minigame2.SetActive(false);
        camera3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (node.connect == true)
        {
            //Debug.Log("connected");
            EventManager.TriggerEvent("CamsAccessable");
            EventManager.TriggerEvent("AccessCamsQuest");
        }
        if (minigameUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitMinigame();
            }
            if (node.connect == true)
            {
                ExitMinigame();
                EventManager.TriggerEvent("CamsAccessable");
                EventManager.TriggerEvent("AccessCamsQuest");
                

            }
        }
        if (minigame2.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitMinigame2();
            }
            if (CheckKey.minigame2End == true)
            {
                ExitMinigame2();
            }
            
        }
        
    }
    public void ExitMinigame() //exits the minigame
    {
        player.GetComponent<PlayerCharacterController>().enabled = true; //enables character movement
        //player.GetComponentInChildren<PlayerCameraController>().enabled = true; //enables camera movement
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        minigameUI.SetActive(false);
        minigame.SetActive(false);
        camera2.enabled = false;
        camera1.enabled = true;
    }
    public void startMinigame() //starts the minigame
    {
        player.GetComponent<PlayerCharacterController>().enabled = false; //disable character movement
        //player.GetComponentInChildren<PlayerCameraController>().enabled = false; //disable camera movement
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        minigameUI.SetActive(true);
        minigame.SetActive(true);
        camera1.enabled = false;
        camera2.enabled = true;
    }
    public void startMinigame2()
    {
        player.GetComponent<PlayerCharacterController>().enabled = false; //disable character movement
        //player.GetComponentInChildren<PlayerCameraController>().enabled = false; //disable camera movement
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        minigame2.SetActive(true);
        camera1.enabled = false;
        camera3.enabled = true;
    }
    public void ExitMinigame2() //exits the minigame
    {
        player.GetComponent<PlayerCharacterController>().enabled = true; //enables character movement
        //player.GetComponentInChildren<PlayerCameraController>().enabled = true; //enables camera movement
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        minigame2.SetActive(false);
        camera3.enabled = false;
        camera1.enabled = true;
    }
}
