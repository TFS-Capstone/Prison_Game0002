using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{   
    int doortype = 1;
    [SerializeField]
    int keytype;
    void Update()
    {
        keytype = GameManager.instance.keycardType;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Door")
        {
            Animator anim = other.GetComponentInChildren<Animator>();
            
            
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (keytype == doortype || doortype == 0)
                    {
                        anim.SetTrigger("OpenClose");
                    Debug.Log(doortype);
                    }
                    else
                    {
                        Debug.Log("Wrong keytype... Needed: " + doortype + " Had: " + keytype);
                    }
                }
                    

            
            
            
                
        }
    }


}
