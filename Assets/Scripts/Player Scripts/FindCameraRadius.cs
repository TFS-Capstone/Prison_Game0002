using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FindCameraRadius : MonoBehaviour
{
    
    public GameObject player;
    Camera pcCam;
    PlayerCameraController pCamCScript;
    PlayerCharacterController pCharCScript;
    

    [Range(0,360)]
    public float findRadius;
    //in inspector, this has to be the same layer tha Camera object nodes are on
    public LayerMask CamMask;

    public List<Camera> inRangeCams = new List<Camera>();

    public Camera[] cameras;
    [SerializeField]
    private int currentCameraIndex;
    public int lastCameraIndex;
    

    


    void Start()
    {
        //pcCam = gameObject.GetComponentInChildren<Camera>();
        pcCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        pCamCScript = GetComponentInChildren<PlayerCameraController>();
        pCharCScript = GetComponent<PlayerCharacterController>();

        currentCameraIndex = 0;
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.GetComponent<CameraToggle>().FindCam();
            cameras[i].gameObject.SetActive(false);

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    FindCamsInRange();
        //}
        if (Input.GetKeyDown(KeyCode.V) && currentCameraIndex!=0)
        {
            NextCamera();
        }

        if (Input.GetKeyDown(KeyCode.Z) && currentCameraIndex != 0)
        {
            PreviousCamera();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            FindCamsInRange();            
        }       

    }


    
    private void FindCamsInRange()
    {
        inRangeCams.Clear();
        cameras = new Camera[0];

        //float camDistance;
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, findRadius, CamMask);
        

        for (int i = 0; i <targetsInRadius.Length; i++)
        {
            Camera camTarget = targetsInRadius[i].GetComponentInChildren<Camera>();
            inRangeCams.Add(camTarget);

            //camDistance = Vector3.Distance(transform.position, targetsInRadius[i].transform.position);
            //if (inRangeCams.Count > 1)
            //{
                
            //}
        }
        

        inRangeCams.Insert(0, pcCam);

        cameras = inRangeCams.ToArray();

        if (cameras.Length > 1)
        {
            if (cameras.Length > lastCameraIndex)
            {
                //currentCameraIndex = 0;
                lastCameraIndex = 0;
            }
            AccessCameraSystem();
        }
        

    }

    private void PreviousCamera()
    {
        Debug.Log("Z button has been pressed. Switching to the previous camera");
        
        if (currentCameraIndex - 1 != 0)
        {
            currentCameraIndex--;

            cameras[currentCameraIndex + 1].gameObject.GetComponent<Cameras>().playerControlled = false;
            cameras[currentCameraIndex + 1].gameObject.GetComponent<CameraToggle>().FindCam();
            cameras[currentCameraIndex].gameObject.GetComponent<Cameras>().playerControlled = true;
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
        }
        else if (currentCameraIndex - 1 == 0)
        {
            currentCameraIndex = cameras.Length - 1;

            cameras[lastCameraIndex].gameObject.GetComponent<Cameras>().playerControlled = false;
            cameras[lastCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
            cameras[currentCameraIndex].gameObject.GetComponent<Cameras>().playerControlled = true;
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
        }

        lastCameraIndex = currentCameraIndex;
    }
    
    private void NextCamera()
    {

        //GetComponent<Player>().camMode = true;

        currentCameraIndex++;

        Debug.Log("V button has been pressed. Switching to the next camera");
        if (currentCameraIndex < cameras.Length)
        {
            cameras[currentCameraIndex - 1].gameObject.GetComponent<Cameras>().playerControlled = false;
            cameras[currentCameraIndex - 1].gameObject.GetComponent<CameraToggle>().FindCam();
            cameras[currentCameraIndex].gameObject.GetComponent<Cameras>().playerControlled = true;
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
        }
        else
        {
            cameras[currentCameraIndex - 1].gameObject.GetComponent<Cameras>().playerControlled = false;
            cameras[currentCameraIndex - 1].gameObject.GetComponent<CameraToggle>().FindCam();
            currentCameraIndex = 1;

            cameras[currentCameraIndex].gameObject.GetComponent<Cameras>().playerControlled = true;
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
        }
        lastCameraIndex = currentCameraIndex;
    }

    private void AccessCameraSystem()
    {
        if (currentCameraIndex != 0)
        {
            Debug.Log("C button pressed. returning to main camera");
            //gameObject.GetComponent<PlayerCharacterController>().type = 0; //Enable character movement
            //pCharCScript.type = 0;
            //gameObject.GetComponentInChildren<PlayerCameraController>().type = 0; //disable character look movement
            //pCamCScript.type = 0;
            GetComponent<MinMapToggle>().miniMapCanvas.SetActive(false);

            cameras[currentCameraIndex].gameObject.GetComponent<Cameras>().playerControlled = false;
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();

            currentCameraIndex = 0;

            //cameras[currentCameraIndex].gameObject.GetComponent<Cameras>().playerControlled = true;
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
        }
        else if(currentCameraIndex == 0)
        {
            Debug.Log("C button pressed. Accessing cams");
            //gameObject.GetComponent<PlayerCharacterController>().type = 1; //disable character movement
            //pCharCScript.type = 1;
            //gameObject.GetComponentInChildren<PlayerCameraController>().type = 1; //disable character look movement
            //pCamCScript.type = 1;
            GetComponent<MinMapToggle>().miniMapCanvas.SetActive(true);
            if (lastCameraIndex != 0)
            {
                
                cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();

                currentCameraIndex = lastCameraIndex;

                cameras[currentCameraIndex].gameObject.GetComponent<Cameras>().playerControlled = true;
                cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
            }
            else
            {
                currentCameraIndex++;
                lastCameraIndex++;
                
                cameras[currentCameraIndex - 1].gameObject.GetComponent<CameraToggle>().FindCam();
                cameras[currentCameraIndex].gameObject.GetComponent<Cameras>().playerControlled = true;
                cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
            }
        }
    }
}
