using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Renderer playerColour;
    public Material original;
    public Material Disguise1;
    public Material Disguise2;
    public bool isDisguised = false;
    RaycastHit hit;
    [SerializeField]
    GameObject disguise = null; //The type of disguise that the player is holding, will change based on what is picked up
    [SerializeField]
    GameObject item = null; //the type of item the player is holding, may change from number later
    [SerializeField]
    int keycard = 0; //type of keycard the player is holding, probably staying a number

    void Start()
    {
        playerColour = GetComponent<Renderer>();
    }

    
    void Update()
    {
        /*
        Left click with an item, that will depend on what the player is looking at
        -- looking at an item and clicking will swap the current item out for the new one
        -- looking at an interactable surface with the correct item, will do whatever it is intended to do
        -- looking at an interactable surface with an incorrect item, will yield "cannot use this item here"
        -- looking at a door with the correct keycard will open the door
        -- looking at a door with the wrong keycard will yield "This keycard does not work here"
       */
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
        {
            if (hit.distance < 20 && hit.transform.gameObject.tag == "item")
            {
                //Debug.Log("hit");
                if (Input.GetMouseButtonDown(0))
                {
                    if (item != null)
                    {
                        item.transform.position = hit.point;
                        item.SetActive(true);
                    }
                    item = hit.transform.gameObject;
                    item.SetActive(false) ;
                    //destroying the object breaks a few things
                    //Destroy(hit.transform.gameObject);
                    Debug.Log(item);
                }
            }
            else if (hit.distance < 20 && hit.transform.gameObject.tag == "disguise")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (disguise != null)
                    {
                        disguise.transform.position = hit.point;
                        disguise.SetActive(true);
                    }
                    disguise = hit.transform.gameObject;
                    disguise.SetActive(false);
                    //destroying the object breaks a few things
                    //Destroy(hit.transform.gameObject);
                    Debug.Log(disguise);
                }
            }
        }



        //if the player presses 'F' and has a disguise, they will change into that disguise
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (disguise != null)
            {
                if (isDisguised)
                {
                    playerColour.material = original;
                    isDisguised = false;
                }
                else
                {
                    if (disguise.name == "blue")
                    {
                        playerColour.material = Disguise1;
                        isDisguised = true;
                    }

                    else if (disguise.name == "pink")
                    {
                        playerColour.material = Disguise2;
                        isDisguised = true;
                    }
                }
                
            }
        }
       
    }

}
