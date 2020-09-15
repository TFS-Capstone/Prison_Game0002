using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // player material stuff
    public Renderer playerColour;
    public Material original;
    public Material Disguise1;
    public Material Disguise2;
    public bool isDisguised = false;

    //other materials for item selection
    public Material redkey;
    public Material bluekey;
    public Material greenkey;
    //item materials
    public Material item1;
    public Material item2;


    [SerializeField]
    GameObject disguise = null; //The type of disguise that the player is holding, will change based on what is picked up
    [SerializeField]
    GameObject item = null; //the type of item the player is holding, may change from number later
    [SerializeField]
    int keycard = 0; //type of keycard the player is holding, probably staying a number
    [SerializeField]
    GameObject keycardObject = null;
    //Item Selection

    [SerializeField]
    Material highlightMaterial; //the object that the player is looking at will turn this material

    [SerializeField]
    Camera cam; //camera for raycasts

    Material originalMat; //the objects original material
    GameObject temp; //   \_(0-0)_/


    Transform _selected; //for the selected object

    GameObject door;
    //End of item selection
    void Start()
    {
        //sets the player colour variable to the correct one
        playerColour = GetComponent<Renderer>();
        //grabs the player's camera
        cam = GetComponentInChildren<Camera>();
    }

    
    void LateUpdate()
    {
        //selection stuff

        if (_selected != null)
            if (_selected.gameObject.tag == "item" || _selected.gameObject.tag == "disguise" || _selected.gameObject.tag == "keycard" || _selected.gameObject.tag == "Door")
            {
            var selectionRenderer = _selected.GetComponent<Renderer>(); //grabs the selected object's renderer
            selectionRenderer.material = originalMat; //sets the variable to the selected object's material so it can be reset later
            _selected = null; //something here
            }

        //send out the ray
        var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //do the raycast
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            
            if (selection != null)
            {
                if (selection.gameObject.tag == "item" || selection.gameObject.tag == "disguise" || selection.gameObject.tag == "keycard" || selection.gameObject.tag == "Door")    
                {
                    originalMat = selectionRenderer.material; //grab the original material of the item (or door)
                    selectionRenderer.material = highlightMaterial; //change the object's matieral to highlighted
                }
             
                _selected = selection;
            }
                
            
        }


        //end of selection stuff
        
        /*
        Left click with an item, that will depend on what the player is looking at
        -- looking at an item and clicking will swap the current item out for the new one
        -- looking at an interactable surface with the correct item, will do whatever it is intended to do
        -- looking at an interactable surface with an incorrect item, will yield "cannot use this item here"
        -- looking at a door with the correct keycard will open the door
        -- looking at a door with the wrong keycard will yield "This keycard does not work here"
       */
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.distance < 20 && hit.transform.gameObject.tag == "item")
            {//pick up item stuff
                //Debug.Log("hit");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (item != null)
                    {
                        item.transform.position = hit.point;
                        item.SetActive(true);
                    }
                    item = hit.transform.gameObject;
                    item.SetActive(false);
                    //destroying the object breaks a few things
                    //Destroy(hit.transform.gameObject);
                    Debug.Log(item);
                }
            }
            else if (hit.distance < 20 && hit.transform.gameObject.tag == "disguise")
            {//pick up disguise stuff
                if (Input.GetKeyDown(KeyCode.E))
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
            else if (hit.distance < 20 && hit.transform.gameObject.tag == "keycard") 
            {//pick up keycard stuff
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (keycardObject != null)
                    {
                        keycardObject.transform.position = hit.point;
                        keycardObject.SetActive(true);

                    }
                    keycardObject = hit.transform.gameObject;
                    keycardObject.SetActive(false);
                    if (keycardObject.GetComponent<Items>().type == 1)
                    {
                        keycard = 1;
                        GameManager.instance.keycardType = 1;
                    }
                    else if (keycardObject.GetComponent<Items>().type == 2)
                    {
                        keycard = 2;
                        GameManager.instance.keycardType = 2;
                    }
                    else if (keycardObject.GetComponent<Items>().type == 3)
                    {
                        keycard = 3;
                        GameManager.instance.keycardType = 3;
                    }
                    Debug.Log(keycardObject);

                }
            }
            else if (hit.distance < 20 && hit.transform.gameObject.tag == "Door")//if the player hit a door
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    door = hit.transform.gameObject;
                    door.GetComponent<DoorNew>().Open(); //open the door (or close)
                    
                }
            }
        }
        //if the player presses 'F' and has a disguise, they will change into that disguise
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (disguise != null)
            {
                if (isDisguised) //if the player is disguised, than take off the disguise
                {
                    playerColour.material = original;
                    isDisguised = false;
                    GameManager.instance.disguised = false;
                }
                else
                {
                    if (disguise.GetComponent<Items>().type == 4) //if the player is not disguised, and they have the first disguise, then disguise
                    {
                        playerColour.material = Disguise1;
                        isDisguised = true;
                        GameManager.instance.disguised = true;
                    }

                    else if (disguise.GetComponent<Items>().type == 5) //if the player is not disguised, and they have the second disguise, then disguise
                    {
                        playerColour.material = Disguise2;
                        isDisguised = true;
                        GameManager.instance.disguised = true;
                    }
                }
                
            }
        }
       
    }

}
