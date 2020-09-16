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
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        minigameUI.SetActive(false);
        camera2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
        {
            if (hit.distance < 20 && hit.transform.gameObject.tag == "Minigame")
            {//if the player hits the minigame trigger and presses f
                if (Input.GetKeyDown(KeyCode.F))
                {
                    startMinigame();
                }
            }
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
            }
        }
    }
    public void ExitMinigame() //exits the minigame
    {
        player.GetComponent<PlayerCharacterController>().enabled = true; //enables character movement
        player.GetComponentInChildren<PlayerCameraController>().enabled = true; //enables camera movement
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        minigameUI.SetActive(false);
        camera2.enabled = false;
        camera1.enabled = true;
    }
    public void startMinigame() //starts the minigame
    {
        player.GetComponent<PlayerCharacterController>().enabled = false; //disable character movement
        player.GetComponentInChildren<PlayerCameraController>().enabled = false; //disable camera movement
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        minigameUI.SetActive(true);
        camera1.enabled = false;
        camera2.enabled = true;
    }
}
