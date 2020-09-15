using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    //player speed
    [SerializeField]
    float speed;
    //is used for sprint and slow walk
    [SerializeField]
    float speedMultiplier = 1;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {//sprint
            speedMultiplier = 2;
            GameManager.instance.playerSpeed = 2;
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {//slow walk
            speedMultiplier = 0.5f;
            GameManager.instance.playerSpeed = 0.5f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl))
        {//if either are let go of, then normal speed
            speedMultiplier = 1;
            GameManager.instance.playerSpeed = 1;
        }
        //call for movement
        PlayerMovement();
    }
    //actual player movement
    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * (speed * speedMultiplier) * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }
}
