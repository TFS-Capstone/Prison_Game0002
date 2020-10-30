using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCharacterController : MonoBehaviour
{
    //private PhotonView pv;
    [SerializeField]
    float speed;
    [SerializeField]
    float speedMultiplier = 1;

    private void Start()
    {
        //pv = GetComponent<PhotonView>();
        //if (pv.IsMine)
        //{
        //    GetComponentInChildren<Camera>().enabled = true;
        //}
        //else
        //{
        //    GetComponentInChildren<Camera>().enabled = false;
        //}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMultiplier = 2;
            GameManager.instance.playerSpeed = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speedMultiplier = 0.5f;
            GameManager.instance.playerSpeed = 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            speedMultiplier = 1;
            GameManager.instance.playerSpeed = 1;
        }
        PlayerMovement();
    }
    void PlayerMovement()

    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * (speed * speedMultiplier) * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}
