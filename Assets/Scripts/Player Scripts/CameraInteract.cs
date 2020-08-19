using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraInteract : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Camera cam;

    public GameObject pingIcon;
    // Start is called before the first frame update
    void Start()
    {
        
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
                Ping();
            }
            
        }
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
}
