using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]

public class Character : MonoBehaviour
{
    private CharacterController cc;

    public float health;
    public float mana; 
    public float speed;
    public float jumpspeed;
    public float rotationSpeed;
    public float gravity;

    public bool type;
    Vector3 moveDirection;

    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed;

    public bool Item1;
    public bool Item2;
    public bool Item3;

    GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        type = true;
        speed = 6.0f;
        jumpspeed = 8.0f;
        rotationSpeed = 5.0f;
        gravity = 9.81f;
        health = 50.0f;
        mana = 50.0f;
        Item1 = false;
        Item2 = false;
        Item3 = false;
        moveDirection = Vector3.zero;
        gm = GameObject.FindGameObjectWithTag("GameManager");
}

    // Update is called once per frame
    void Update()
    {
        if (!type)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
            float curSpeed = speed * Input.GetAxis("Vertical");
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * curSpeed);
        }
        else
        {
            if (cc.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Item2)
                {
                    Jump();
                }
            }

            if (Item1)
            {
                GainHealth();
            }

            if (Item3)
            {
                GainMana();
            }

            moveDirection.y -= gravity * Time.deltaTime;
            cc.Move(moveDirection * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (gm.GetComponent<GameManager>().Val1() == "GainHealth")
                {
                    Item1 = true;
                    gm.GetComponent<GameManager>().UseItem();
                }
                else if (gm.GetComponent<GameManager>().Val1() == "Jump")
                {
                    Item2 = true;
                    gm.GetComponent<GameManager>().UseItem();
                }
                else if (gm.GetComponent<GameManager>().Val1() == "GainMana")
                {
                    Item3 = true;
                    gm.GetComponent<GameManager>().UseItem();
                }
            }
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpspeed;
        }
    }
    
    public void GainHealth()
    {
        Debug.Log("Health Gained!");
        health = 100.0f;
    }

    public void GainMana()
    {
        Debug.Log("Mana Gained");
        mana = 100.0f;
    }
}
        
            

            
    


    

    

