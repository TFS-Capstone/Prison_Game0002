using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesRework : MonoBehaviour
{
    List<Transform> children;
    public float rayDistance;
    public Camera camera1;
    public bool rotation;
    public bool isConnected;
    Transform[] kids;
    LayerMask ignore;

    public int[] possibleRotations;
    public List<PipesRework> nodeOrder;


    public Material original;
    public Material connected;
    // Start is called before the first frame update
    void Start()
    {
        nodeOrder = new List<PipesRework>();
        //Debug.Log(transform.childCount);
        //camera1 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        for (int i = 0; i < transform.childCount; i++)
        {
            //if (transform.GetChild(i).gameObject.activeSelf.Equals(true))
            //{
            //    //Debug.Log(transform.GetChild(i).gameObject);
            //    //children.Add(transform.GetChild(i));
            //    kids[i] = transform.GetChild(i).transform;


            //}
        }
       // Debug.Log(children.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("ray hit" + hit.collider.name);
                if (hit.transform == this.transform && rotation == true)
                {
                    //Debug.Log("rotating");
                    transform.Rotate(0, 90, 0);
                    
                }
            }
            


        }
        CheckConnections();

        foreach (PipesRework node in nodeOrder)
        {
            if (node.isConnected)
            {
                //CheckConnections();
            }
        }
        //Debug.Log(children.Count);
        if (isConnected)
        {
            gameObject.GetComponent<Renderer>().material = connected;
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material = connected;


            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = original;
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material = original;
            }
        }
        


    }

    void CheckConnections()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf.Equals(true))
            {
                RaycastHit hit2;
                Ray ray1 = new Ray(child.transform.position, child.transform.forward);


                Physics.Raycast(ray1, out hit2, rayDistance);
                Debug.DrawRay(ray1.origin, child.transform.forward * rayDistance, Color.green);


                if (hit2.collider != null)
                {
                    Debug.Log(hit2.collider);
                    if (hit2.collider.gameObject.GetComponentInParent<PipesRework>().isConnected)
                    {
                        for (int i = 0; i < possibleRotations.Length; i++)
                        {
                            //Debug.Log(transform.rotation.y);
                            Debug.Log(transform.eulerAngles.y);
                            //Debug.Log(possibleRotations[0]);
                            //Debug.Log(possibleRotations[1]);
                            if (transform.eulerAngles.y == possibleRotations[i])
                            {
                                Debug.Log("connecting " + transform.eulerAngles.y + " " + possibleRotations[i].ToString());
                                isConnected = true;
                                rotation = false;

                                break;
                            }
                            else
                            {
                                isConnected = false;


                            }
                        }
                    }
                        




                    //Debug.Log("reached");
                }

            }

        }
    }
}
