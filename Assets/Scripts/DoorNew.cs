using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNew : MonoBehaviour
{

    //the type of the door (usually based on colour
    [SerializeField]
    int doortype;
    //the type of keycard that the player has (0 if none)
    [SerializeField]
    int keytype;
    //the time it takes for the door to open to full
    [SerializeField]
    float animTime;
    //if the door is closed
    bool closed = true;
    //stops the door from changing directions mid animation
    bool stop = true;
    //so that the door cannot be opened while it is closing and vice-versa
    bool runOnce = true;
    //if the door can be opened or closed
    bool canOpen = true;
    void Update()
    {
        //checks the key type that the player has
        keytype = GameManager.instance.keycardType;
        //stops the door from opening or closing while it is already doing so
        if (!runOnce)
        {
            StartCoroutine(doorTime());
            runOnce = true;
        }
        if (!stop)
        {//opens the door
            if (closed)
            {
                transform.Translate(Vector3.right * Time.deltaTime, Space.Self);
            }//closes the door
            else
            {
                transform.Translate(-Vector3.right * Time.deltaTime, Space.Self);
            }
        }
        
    }

    public void Open()
    {
            if (keytype == doortype)
            {
                if (closed && canOpen)
                {//sets the door so it opens
                    canOpen = false;
                    closed = false;
                    stop = false;
                    runOnce = false;
                }
                else if (!closed && canOpen)
                {//sets the door so it closes
                    canOpen = false;
                    closed = true;
                    stop = false;
                    runOnce = false;
                }
            }
            else
            {//if the player has the wrong key, then do this:
                Debug.Log("Wrong Key! Needed: " + doortype + ". Had: " + keytype);
            }

    }

    IEnumerator doorTime()
    {//so that the door doesn't go wonky when opening and closing
        yield return new WaitForSeconds(animTime);
        stop = true;
        canOpen = true;
    }
}


