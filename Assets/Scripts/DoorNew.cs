using System.Collections;
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
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (keytype == doortype)
                {
                    if (closed && canOpen)
                    {
                        canOpen = false;
                        closed = false;
                        stop = false;
                        runOnce = false;
                    }
                    else if (!closed && canOpen)
                    {
                        canOpen = false;
                        closed = true;
                        stop = false;
                        runOnce = false;
                    }
                } else
                {
                    Debug.Log("Wrong Key! Needed: " + doortype + ". Had: " + keytype);
                }
              

            }
            
                
            
        }
    }


    IEnumerator doorTime()
    {
        yield return new WaitForSeconds(animTime);
        stop = true;
        canOpen = true;
    }
}


