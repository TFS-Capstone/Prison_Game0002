using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //RaycastHit hit;
    CharacterController cc;

    // Variables to control Character
    public float speed = 6.0f;          // Speed of Character
    public float jumpSpeed = 8.0f;      // Jump speed of Character
    public float rotateSpeed = 5.0f;    // Speed of rotation for Character
    public float gravity = 9.81f;       // Speed Character falls to ground after jump or fall

    // Used to select Controller function to work with
    // - SimpleMove = 0;
    // - Move = 1;
    public int type = 0;

    // Used by Move() to move the Player
    Vector3 moveDirection = Vector3.zero;

    // Projectile variables
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (type == 0)
        {
            // Rotates Character based on left and right keys
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            // Transforms direction from local space to world space
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            // Stores value to move Character based on up and down keys
            float curSpeed = speed * Input.GetAxis("Vertical");

            // Actually moves the Character
            // - Gravity is automatically added
            // - Character cannot jump only fall because y-axis is ignored
            cc.SimpleMove(forward * curSpeed);
        }
        // - Move()
        else if (type == 1)
        {
            if (cc.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

                //moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                // Rotates Character based on left and right keys
                transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

                // Transforms direction from local space to world space.
                moveDirection = transform.TransformDirection(moveDirection);

                // Multiplies Vector3 by speed to make it not 1
                moveDirection *= speed;

                // Checks if Jump key was pressed (SpaceBar)
                if (Input.GetButtonDown("Jump"))
                    // Applies a jump speed in 'y' to make Character jump
                    moveDirection.y = jumpSpeed;
            }

            // Handles gravity for Character so it falls back down after a jump
            moveDirection.y -= gravity * Time.deltaTime;

            // Actually moves the Character
            cc.Move(moveDirection * Time.deltaTime);
        }
        else if(type == 2)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = 0;
            cc.SimpleMove(forward * curSpeed);
        }

        //moved into the inventory system
        /*
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
        {
            if (hit.distance < 20 && hit.transform.gameObject.tag == "interactable")
            {
                Debug.Log("hit");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        */
    }
}
