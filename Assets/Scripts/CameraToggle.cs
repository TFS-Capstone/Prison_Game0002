using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    
    
    private GameObject parentCam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            FindCam();
        }
    }

    public void FindCam()
    {
        if(GetComponent<Camera>())
        {
            if (GetComponent<Camera>().enabled == true)
                GetComponent<Camera>().enabled = false;
            else if (GetComponent<Camera>().enabled == false)
                GetComponent<Camera>().enabled = true;
        }
        else
        {
            Debug.Log("no cam");
        }
    }
}
