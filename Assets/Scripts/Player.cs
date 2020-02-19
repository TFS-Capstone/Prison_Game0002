using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

    private CharacterController cc;

    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravity;
    public bool camMode;
        
    Vector3 moveDirection;

    public Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        
        cc = GetComponent<CharacterController>();        
        speed = 6.0f;
        jumpSpeed = 8.0f;
        rotationSpeed = 5.0f;
        gravity = 9.81f;       
        moveDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camMode)
        {
            if (cc.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                //transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;                        
            }
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
        }

        else if (camMode)
        {
            
        }

        //end game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


}
