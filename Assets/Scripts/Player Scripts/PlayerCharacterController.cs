﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    
    [SerializeField]
    float speed;
    float pushSpeed;
    float speedMultiplier = 1;
    [HideInInspector]
    public int type;
    public Animator animator;
    public Transform cam;
    public CharacterController controller;
    public float turnSmoothTime = 0.1f;
    float smoothVelocity;
    float timeStill;

    [SerializeField]
    AudioClip[] walkClips = null;



    [SerializeField]
    float walkStepDelay = 0.3f;  
    [SerializeField]
    float runStepDelay = 0.1f;
    [SerializeField]
    float crouchStepDelay = 0.5f;

    //CURRENT HEIGHT TO STAY AT
    float yHeight;
    bool pushing = false;

    bool canPlaySound = true;



    private void Start()
    {
        yHeight = transform.position.y;
        type = 0;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        pushSpeed = speed / 2;

        
    }

    void FixedUpdate()
    {
        timeStill += Time.fixedDeltaTime;
        animator.SetFloat("IdleAnimTrigger", timeStill);
        
        pushing = GetComponent<Inventory>().pushing;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMultiplier = 2;
            GameManager.instance.playerSpeed = 2;
            animator.SetBool("IsRunning", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speedMultiplier = 0.5f;
            GameManager.instance.playerSpeed = 0.5f;
            animator.SetBool("IsCrouched", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            speedMultiplier = 1;
            GameManager.instance.playerSpeed = 1;
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsCrouched", false);
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            
            timeStill = 0;
            animator.SetBool("IsMoving", true);
            if (canPlaySound)
            {
                if (speedMultiplier == 0.5f)
                {
                    int temp = Random.Range(0, walkClips.Length);
                    AudioManager.instance.PlayAudioAtPoint(walkClips[temp], transform);
                    //Debug.Log("Played sound: " + walkClips[temp]);
                    canPlaySound = false;
                    StartCoroutine(waitToPlaySound(walkClips[temp].length + crouchStepDelay));
                }
                else if (speedMultiplier == 1)
                {
                    int temp = Random.Range(0, walkClips.Length);
                    AudioManager.instance.PlayAudioAtPoint(walkClips[temp], transform);
                    //Debug.Log("Played sound: " + walkClips[temp]);
                    canPlaySound = false;
                    StartCoroutine(waitToPlaySound(walkClips[temp].length + walkStepDelay));
                }
                else if (speedMultiplier == 2)
                {
                    int temp = Random.Range(0, walkClips.Length);
                    AudioManager.instance.PlayAudioAtPoint(walkClips[temp], transform);
                    //Debug.Log("Played sound: " + walkClips[temp]);
                    canPlaySound = false;
                    StartCoroutine(waitToPlaySound(walkClips[temp].length + runStepDelay));
                }
            }
            
        }
        else
        {
            animator.SetBool("IsMoving", false);
            
        }

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (type ==  0)
        {
            if (transform.position.y != yHeight)
                transform.position = new Vector3(transform.position.x, yHeight, transform.position.z);
            if (!pushing)
            {
                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * speedMultiplier * Time.deltaTime);
                }
            }
            else if (pushing)
            {

                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * pushSpeed * Time.deltaTime);
                }
            }

            // this is for the quests
            EventManager.TriggerEvent("CheckPlayerBounds");
            EventManager.TriggerEvent("CartReachCafeQuest");
        }
        
    }

    IEnumerator waitToPlaySound(float time)
    {
        yield return new WaitForSeconds(time);
        canPlaySound = true;
    }

}
