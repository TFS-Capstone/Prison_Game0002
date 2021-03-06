﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNew : MonoBehaviour
{
    [SerializeField]
    int doortype;
    [SerializeField]
    int keytype;
    
    [SerializeField]
    float animTime;

    [SerializeField]
    AudioClip door;
    [SerializeField]
    float clipWaitTime = 1;

    bool closed = true;
    bool stop = true;
    bool runOnce = true;
    bool canOpen = true;
    void Update()
    {
        keytype = GameManager.instance.keycardType;
        if (!runOnce)
        {
            StartCoroutine(doorTime());
            runOnce = true;
        }
        if (!stop)
        {
            if (closed)
            {
                transform.Translate(Vector3.right * Time.deltaTime, Space.Self);
            }
            else
            {
                transform.Translate(-Vector3.right * Time.deltaTime, Space.Self);
            }
        }
        
    }

    public void Open()
    {
            if (keytype == doortype || doortype <= GameManager.instance.playerCardLevel)
            {
                if (closed && canOpen)
                {
                    canOpen = false;
                    closed = false;
                    stop = false;
                    runOnce = false;
                StartCoroutine(waitToPlayClip());
                }
                else if (!closed && canOpen)
                {
                    canOpen = false;
                    closed = true;
                    stop = false;
                    runOnce = false;
                StartCoroutine(waitToPlayClip());
            }
            }
            else
            {
                Debug.Log("Wrong Key! Needed: " + doortype + ". Had: " + keytype);
            }

    }

    IEnumerator doorTime()
    {
        yield return new WaitForSeconds(animTime);
        stop = true;
        canOpen = true;
    }
    IEnumerator waitToPlayClip()
    {
        yield return new WaitForSeconds(clipWaitTime);
        AudioManager.instance.PlayAudioAtPoint(door, transform);
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy hit the door");

            if (doortype <=2)
            {
                if (closed && canOpen)
                {
                    canOpen = false;
                    closed = false;
                    stop = false;
                    runOnce = false;
                    StartCoroutine(closeTime());
                }
                
            }
            else
            {
                //Debug.LogError("This should not happen: enemy is trying to use an exit door");
            }
        }
    }

    IEnumerator closeTime()
    {
        yield return new WaitForSeconds(7);
        if (!closed && canOpen)
        {
            canOpen = false;
            closed = true;
            stop = false;
            runOnce = false;
        }

    }
}


