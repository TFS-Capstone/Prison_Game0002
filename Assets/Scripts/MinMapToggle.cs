using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMapToggle : MonoBehaviour
{
    public GameObject miniMapCanvas;
    GameObject miniMapCam;
    Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        miniMapCanvas = GameObject.FindGameObjectWithTag("MiniMap");
        miniMapCanvas.SetActive(false);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        miniMapCam = GameObject.FindGameObjectWithTag("MiniMapCam");
    }

    // Update is called once per frame
    void Update()
    {
        miniMapCam.transform.position = new Vector3(playerPos.position.x, miniMapCam.transform.position.y, playerPos.position.z);
    }
}
