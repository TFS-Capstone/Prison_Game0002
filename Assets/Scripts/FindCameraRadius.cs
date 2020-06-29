using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FindCameraRadius : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject[] availCams;

    public GameObject player;
    public int maxCamAngles;
    //public int currentCams;
    public float findRange = 20.0f;

    public Camera[] cameras;
    [SerializeField]
    private int currentCameraIndex;
    public int lastCameraIndex;
    //in inspector, this has to be the same layer tha Camera object nodes are on
    public LayerMask layer;

    //public bool inRange;


    void Start()
    {
        //Enabling this start code seems to break the camera movement functionality.

        //currentCameraIndex = 0;
        //for (int i = 1; i < cameras.Length; i++)
        //{
        //    cameras[i].gameObject.SetActive(false);
        //}
        //if (cameras.Length > 0)
        //{
        //    cameras[0].gameObject.SetActive(true);
        //    Debug.Log("Camera, is now enabled");
        //}

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
            AccessCameraSystem();            
        }       

    }


    // my attempt at finding all the cams in range
    //works ocassionally, but repeated use breaks
    private void FindCamsInRange()
    {
        for (int i = 0; i < maxCamAngles; i++)
        {
            Collider[] availableCams = Physics.OverlapSphere(transform.position, findRange, layer);
            Debug.Log("Available cams " + availableCams.Length);
            if (availableCams.Length == 0)
            {
                availableCams = null;
            }
        }
    }

    private void PreviousCamera()
    {
        Debug.Log("Z button has been pressed. Switching to the previous camera");
        
        if (currentCameraIndex - 1 != 0)
        {
            currentCameraIndex--;

            cameras[currentCameraIndex + 1].gameObject.SetActive(false);
            cameras[currentCameraIndex + 1].gameObject.GetComponent<CameraToggle>().FindCam();
            cameras[currentCameraIndex].gameObject.SetActive(true);
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
        }
        else if (currentCameraIndex - 1 == 0)
        {
            currentCameraIndex = cameras.Length - 1;

            cameras[lastCameraIndex].gameObject.SetActive(false);
            cameras[lastCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
            cameras[currentCameraIndex].gameObject.SetActive(true);
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
            cameras[currentCameraIndex - 1].gameObject.SetActive(false);
            cameras[currentCameraIndex - 1].gameObject.GetComponent<CameraToggle>().FindCam();
            cameras[currentCameraIndex].gameObject.SetActive(true);
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
        }
        else
        {
            cameras[currentCameraIndex - 1].gameObject.SetActive(false);
            cameras[currentCameraIndex - 1].gameObject.GetComponent<CameraToggle>().FindCam();
            currentCameraIndex = 1;

            cameras[currentCameraIndex].gameObject.SetActive(true);
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
        }
        lastCameraIndex = currentCameraIndex;
    }

    private void AccessCameraSystem()
    {
        if (currentCameraIndex != 0)
        {
            Debug.Log("C button pressed. returning to main camera");
            gameObject.GetComponent<Character>().type = 0; //Enable character movement

            cameras[currentCameraIndex].gameObject.SetActive(false);
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();

            currentCameraIndex = 0;

            cameras[currentCameraIndex].gameObject.SetActive(true);
            cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
        }
        else //if(currentCameraIndex == 0)
        {
            Debug.Log("C button pressed. Accessing cams");
            gameObject.GetComponent<Character>().type = 2; //disable character movement

            if (lastCameraIndex != 0)
            {
                cameras[currentCameraIndex].gameObject.SetActive(true);
                cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();

                currentCameraIndex = lastCameraIndex;

                cameras[currentCameraIndex].gameObject.SetActive(true);
                cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
            }
            else
            {
                currentCameraIndex++;
                lastCameraIndex++;
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                cameras[currentCameraIndex - 1].gameObject.GetComponent<CameraToggle>().FindCam();
                cameras[currentCameraIndex].gameObject.SetActive(true);
                cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();
            }
        }
    }
}
