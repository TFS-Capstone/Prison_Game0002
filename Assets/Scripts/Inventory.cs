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
    Material highlightMaterial;

    [SerializeField]
    Camera cam;

    Material originalMat;

    Transform _selected;
    //End of item selection
    void Start()
    {
        playerColour = GetComponent<Renderer>();
        cam = GetComponentInChildren<Camera>();
    }

    
    void LateUpdate()
    {
        //selection stuff

        if (_selected != null)
            if (_selected.gameObject.tag == "item" || _selected.gameObject.tag == "disguise" || _selected.gameObject.tag == "keycard")
            {
            var selectionRenderer = _selected.GetComponent<Renderer>();
            selectionRenderer.material = originalMat;
            _selected = null;
        }


        var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();

            if (selection != null)
            {
                if (selection.gameObject.tag == "keycard")
                {
                    if (selection.gameObject.name == "RedKeycard")
                    {
                        originalMat = redkey;
                    }
                    else if (selection.gameObject.name == "BlueKeycard")
                    {
                        originalMat = bluekey;
                    }
                    else if (selection.gameObject.name == "GreenKeycard")
                    {
                        originalMat = greenkey;
                    }
                }
                else if (selection.gameObject.tag == "disguise")
                {
                    if (selection.gameObject.name == "blue")
                    {
                        originalMat = Disguise1;
                    }
                    else if (selection.gameObject.name == "pink")
                    {
                        originalMat = Disguise2;
                    }
                }
                else if (selection.gameObject.tag == "item")
                {
                    if (selection.gameObject.name == "item1")
                    {
                        originalMat = item1;
                    }
                    else if (selection.gameObject.name == "item1")
                    {
                        originalMat = item2;
                    }
                }


                if (selection.gameObject.tag == "item" || selection.gameObject.tag == "disguise" || selection.gameObject.tag == "keycard")    
                {   
                    selectionRenderer.material = highlightMaterial;
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
                    item.SetActive(false);
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
            else if (hit.distance < 20 && hit.transform.gameObject.tag == "keycard")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (keycardObject != null)

                    {
                        keycardObject.transform.position = hit.point;
                        keycardObject.SetActive(true);

                    }
                    keycardObject = hit.transform.gameObject;
                    keycardObject.SetActive(false);
                    if (keycardObject.name == "RedKeycard")
                    {
                        keycard = 1;
                        GameManager.instance.keycardType = 1;
                    }
                    else if (keycardObject.name == "GreenKeycard")
                    {
                        keycard = 2;
                        GameManager.instance.keycardType = 2;
                    }
                    else if (keycardObject.name == "BlueKeycard")
                    {
                        keycard = 3;
                        GameManager.instance.keycardType = 3;
                    }
                    Debug.Log(keycardObject);

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
                    GameManager.instance.disguised = false;
                }
                else
                {
                    if (disguise.name == "blue")
                    {
                        playerColour.material = Disguise1;
                        isDisguised = true;
                        GameManager.instance.disguised = true;
                    }

                    else if (disguise.name == "pink")
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
