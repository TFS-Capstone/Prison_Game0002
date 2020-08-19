using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMapToggle : MonoBehaviour
{
    public GameObject miniMapCanvas;
    // Start is called before the first frame update
    void Start()
    {
        miniMapCanvas = GameObject.FindGameObjectWithTag("MiniMap");
        miniMapCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
