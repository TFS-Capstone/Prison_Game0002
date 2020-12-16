using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraInteract : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Camera cam;

    float alarmTimer = 5;
    float alarmCooldown = 30; 
    public static bool alarmSounded = false;
    public GameObject pingIcon;
    public GameObject alarmObject;
    // Start is called before the first frame update
    void Start()
    {
        alarmSounded = false;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Camera>().enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                CameraInteraction();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                //Ping();
            }
            if (Input.GetKeyDown(KeyCode.F) && alarmCooldown == 30)
            {
                //Alarm();
            }
            
        }
        if (alarmTimer < 5)
            alarmTimer = alarmTimer + Time.fixedDeltaTime;
        else
            alarmTimer = 5;

        if (alarmCooldown < 30)
            alarmCooldown = alarmCooldown + Time.fixedDeltaTime;
        else
            alarmCooldown = 30;

    }

    private void CameraInteraction()
    {
        Debug.Log("sending ray");
        ray = cam.ScreenPointToRay(new Vector3(0, 0, 0));
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 10, Color.green);

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Door"))
            {
                Debug.Log("hit door");
                Animator anim = hit.collider.GetComponentInChildren<Animator>();
                GameObject door = hit.transform.gameObject;
                door.GetComponent<DoorNew>().Open();
                
            }
        }
    }

    private void Ping()
    {
        Debug.Log("sending ray");
        ray = cam.ScreenPointToRay(new Vector3(0, 0, 0));
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 10, Color.green);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Instantiate(pingIcon, hit.point, Quaternion.identity);
        }
    }
    private void Alarm()
    {
        alarmCooldown = 0;
        alarmTimer = 0;
        Debug.Log("Alarm Sounded");
        alarmSounded = true;
        Instantiate(alarmObject, cam.transform);
    }
}
