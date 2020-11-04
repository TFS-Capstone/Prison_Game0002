using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCameraController : MonoBehaviour
{
    //private PhotonView pv;
    [SerializeField]
    float playerRotation = 1;
    [SerializeField]
    Transform target, player;
    float mouseX, mouseY;

    bool isPaused;

    [HideInInspector]
    public int type = 0;
    Camera cam;
    Transform _selected;
    void Start()
    {


        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
        //Debug.Log("Cam " + cam);

    }
    void Update()
    {
        if (_selected != null)
            if (_selected.gameObject.name != "item" || _selected.gameObject.name != "disguise" || _selected.gameObject.name != "keycard" || _selected.gameObject.name != "floor")
            {
                var selectionRenderer = _selected.GetComponent<Renderer>();
                selectionRenderer.enabled = true;
                _selected = null;
            }
        var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3.5f))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            if (selection.gameObject.name != "item" || selection.gameObject.name != "disguise" || selection.gameObject.name != "keycard" || selection.gameObject.name != "floor")
            {
                selectionRenderer.enabled = false;
            }
            _selected = selection;
        }




        if (GameManager.instance.GameIsPause)
        {
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            isPaused = true;
        }

        else
        {
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
        }

    }

    void LateUpdate()
    {
        if (!isPaused && type == 0)
            CamControl();

    }


    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * playerRotation;
        mouseY -= Input.GetAxis("Mouse Y") * playerRotation;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);

    }

   
}
