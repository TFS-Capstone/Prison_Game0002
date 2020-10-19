using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerCameraController : MonoBehaviour
{
    private PhotonView pv;
    [SerializeField]
    float playerRotation = 1;
    [SerializeField]
    Transform target, player;
    float mouseX, mouseY;

    bool isPaused;

    Camera cam;
    Transform _selected;
    void Start()
    {
        pv = GetComponentInParent<PhotonView>();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (pv.IsMine)
        {
            cam = GetComponent<Camera>();
            Debug.Log("Cam " + cam);
            Debug.Log("pv " + pv);
        }
        
    }
    void Update()
    {
        if (pv.IsMine)
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
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isPaused = true;
            }

            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                isPaused = false;
            }
        }
        
            
    }

    void LateUpdate()
    {
        if (pv.IsMine)
        {
            if (!isPaused)
                CamControl();
        }
        
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
