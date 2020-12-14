using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // player material stuff
    public Renderer playerClothesMesh = null;
    public Renderer playerPantsMesh = null;
    public Material prisonOutfit = null;
    public Material guardOutfit = null;
    public Material Disguise2 = null;
    public bool isDisguised = false;
    /*
    //other materials for item selection
    public Material redkey;
    public Material bluekey;
    public Material greenkey;

    public Material item1;
    public Material item2;
    */
    bool holdingProj = false;
    bool projIsOut = false;
    public Transform handLocation;
    [SerializeField]
    Transform pushablePushLocation;
    public GameObject curPushingObj;

    [SerializeField]
    GameObject disguise = null; //The type of disguise that the player is holding, will change based on what is picked up
    [SerializeField]
    public GameObject item = null; //the type of item the player is holding, may change from number later
    [SerializeField]
    public int keycard = 0; //type of keycard the player is holding, probably staying a number
    [SerializeField]
    public GameObject keycardObject = null;
    
    public GameObject throwableObject = null; // type of throwable object the player is carrying
    public bool pushing = false;
    //Item Selection

    [SerializeField]
    Material highlightMaterial;

    [SerializeField]
    Camera cam;
    [SerializeField]
    GameObject player;
    Material originalMat;
    GameObject temp;


    Transform _selected;

    GameObject door;
    [SerializeField]
    MinigameTrigger minigame;
    //End of item selection
    void Start()
    {
        //playerColour = GetComponent<Renderer>();
        prisonOutfit = playerClothesMesh.material;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        minigame = player.GetComponent<MinigameTrigger>();
        if (!pushablePushLocation)
        {
            Debug.LogError("no place to set push objects from, fix before trying to push an object");
        }
    }

    
    void LateUpdate()
    {
        //selection stuff

        if (_selected != null)
            if (_selected.gameObject.tag == "item" || _selected.gameObject.tag == "disguise" || _selected.gameObject.tag == "keycard" || _selected.gameObject.tag == "Door" 
                || _selected.gameObject.tag == "Throwable" || _selected.gameObject.tag == "Pushable" || _selected.gameObject.tag == "Minigame")
            {
            var selectionRenderer = _selected.GetComponent<Renderer>();
            selectionRenderer.material = originalMat;
            _selected = null;
        }


        var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 12))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            
            if (selection != null)
            {
                


                if (selection.gameObject.tag == "item" || selection.gameObject.tag == "disguise" || selection.gameObject.tag == "keycard" || selection.gameObject.tag == "Door"  
                    || selection.gameObject.tag == "Throwable" || selection.gameObject.tag == "Pushable" || selection.gameObject.tag == "Minigame")    
                {
                    originalMat = selectionRenderer.material;
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
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }


        //if the player presses 'F' and has a disguise, they will change into that disguise
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (disguise != null)
            {
                if (isDisguised)
                {
                    playerClothesMesh.material = prisonOutfit;
                    playerPantsMesh.material = prisonOutfit;
                    isDisguised = false;
                    GameManager.instance.disguised = false;
                }
                else
                {
                    if (disguise.GetComponent<Items>().type == 4)
                    {
                        playerClothesMesh.material = guardOutfit;
                        playerPantsMesh.material = guardOutfit;
                        isDisguised = true;
                        GameManager.instance.disguised = true;
                    }

                    else if (disguise.GetComponent<Items>().type == 5)
                    {
                        playerClothesMesh.material = Disguise2;
                        playerPantsMesh.material = Disguise2;
                        isDisguised = true;
                        GameManager.instance.disguised = true;
                    }
                }                
            }
        }       

        if (holdingProj)
        {
            throwableObject.transform.position = handLocation.position;
            throwableObject.transform.rotation = handLocation.rotation;
            
            if(Input.GetMouseButtonDown(1))
            {
                if (projIsOut)
                {
                    projIsOut = false;
                    throwableObject.SetActive(false);
                }
                else
                {
                    projIsOut = true;
                    throwableObject.SetActive(true);
                }
            }
            if(projIsOut && Input.GetMouseButtonDown(0))
            {
                Debug.Log("Throw");
                Vector3 throwForce = (transform.forward * 5) + new Vector3(0, 10, 0);
                throwableObject.GetComponent<Rigidbody>().AddForce(throwForce,ForceMode.Impulse);
                throwableObject = null;
                holdingProj = false;
                projIsOut = false;
            }

        }





    }
    public void PickUp()
    {
        var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.distance < 20 && hit.transform.gameObject.tag == "item")
            {
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
            {
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
            {
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
                        EventManager.TriggerEvent("FirstKeycardQuest");
                    }
                    else if (keycardObject.GetComponent<Items>().type == 2)
                    {
                        keycard = 2;
                        GameManager.instance.keycardType = 2;
                        EventManager.TriggerEvent("SecondKeycardQuest");
                    }
                    else if (keycardObject.GetComponent<Items>().type == 3)
                    {
                        keycard = 3;
                        GameManager.instance.keycardType = 3;
                        EventManager.TriggerEvent("ThirdKeycardQuest");
                    }
                    Debug.Log(keycardObject);

                }
            }
            else if (hit.distance < 20 && hit.transform.gameObject.tag == "Door")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    door = hit.transform.gameObject;
                    door.GetComponent<DoorNew>().Open();

                }
            }
            else if (hit.distance < 20 && hit.transform.gameObject.tag == "Throwable")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (throwableObject != null)
                    {
                        throwableObject.transform.position = hit.point;
                        throwableObject.SetActive(true);
                    }
                    throwableObject = hit.transform.gameObject;
                    throwableObject.SetActive(false);
                    //holdingProj = true;
                    gameObject.GetComponent<Shoot>().projectileToSpawn = throwableObject;

                    //Debug.Log(throwableObject);

                }
            }
            else if (hit.distance < 20 && hit.transform.gameObject.tag == "Pushable")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!pushing)
                    {
                        EventManager.TriggerEvent("GetCartQuest");
                        pushing = true;
                        curPushingObj = hit.transform.gameObject;
                        Debug.Log(curPushingObj);
                        hit.transform.parent = pushablePushLocation.transform;
                        hit.transform.position = pushablePushLocation.transform.position;
                        hit.transform.rotation = pushablePushLocation.transform.rotation;
                        //hit.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+2);

                    }
                    else if (pushing)
                    {
                        pushing = false;
                        hit.transform.parent = null;
                    }
                }
            }
            else if (hit.distance < 20 && hit.transform.gameObject.tag == "Minigame")
            {
                int gameType = hit.transform.gameObject.GetComponent<MinigameType>().type;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (gameType == 1)
                    {
                        minigame.startMinigame();
                    }
                    else if (gameType == 2)
                    {
                        minigame.startMinigame2();
                    }
                    else
                    {
                        Debug.LogError("Minigame type not set");
                    }
                }
                    
                
            }


        }
    }



}
