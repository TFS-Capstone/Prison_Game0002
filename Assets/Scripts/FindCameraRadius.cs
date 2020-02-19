using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCameraRadius : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] availCams;

    public GameObject player;
    public int maxCamAngles;
    public int currentCams;
    public float findRange = 20.0f;

    public Camera[] cameras;
    private int currentCameraIndex;
    //in inspector, this has to be the same layer tha Camera object nodes are on
    public LayerMask layer;

    public bool inRange;


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
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            //GetComponent<Player>().camMode = true;
            currentCameraIndex++;
            Debug.Log("C button has been pressed. Switching to the next camera");
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
                currentCameraIndex = 0;
                
                cameras[currentCameraIndex].gameObject.SetActive(true);
                cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();

            }
        }

        if (Input.GetKeyDown(KeyCode.V) && currentCameraIndex != 0)
        {
            currentCameraIndex--;
            Debug.Log("C button has been pressed. Switching to the next camera");
            if (currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex + 1].gameObject.SetActive(false);
                cameras[currentCameraIndex + 1].gameObject.GetComponent<CameraToggle>().FindCam();
                cameras[currentCameraIndex].gameObject.SetActive(true);
                cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();

            }
            else
            {
                cameras[currentCameraIndex + 1].gameObject.SetActive(false);
                cameras[currentCameraIndex + 1].gameObject.GetComponent<CameraToggle>().FindCam();
                currentCameraIndex = 0;
                
                cameras[currentCameraIndex].gameObject.SetActive(true);
                cameras[currentCameraIndex].gameObject.GetComponent<CameraToggle>().FindCam();

            }
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

    

}
