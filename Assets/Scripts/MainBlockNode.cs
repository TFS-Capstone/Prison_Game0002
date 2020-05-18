using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlockNode : MonoBehaviour
{
    public int number;
    public bool connect;
    public bool rotation;
    public Camera camera1;
    public Material original;
    public Material connected;
    BlockNode[] nodes = new BlockNode[4];
    // Start is called before the first frame update
    void Start()
    {
        nodes = GetComponentsInChildren<BlockNode>();
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].number = number;
            nodes[i].connectnumber = number;
        }
        if(connect == true)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].connect = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.transform == this.transform && rotation==true)
                {
                    transform.Rotate(0, 90, 0);
                }
            }
        }
        if (rotation == true) {
            if (connect == false)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (nodes[i].connect == true && nodes[i].connectnumber < number)
                    {
                        connect = true;
                    }
                }
            }
            if (connect == true)
            {
                bool keep = false;
                for (int i = 0; i < nodes.Length; i++)
                {
                    if (nodes[i].connect == true && nodes[i].connectnumber < number)
                    {
                        keep = true;
                    }
                }
                if (keep == true)
                {
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        nodes[i].connect = true;
                        nodes[i].gameObject.GetComponent<Renderer>().material = connected;
                        this.gameObject.GetComponent<Renderer>().material = connected;
                    }
                }
                if (keep == false)
                {
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        connect = false;
                        nodes[i].connect = false;
                        nodes[i].connectnumber = number;
                        nodes[i].gameObject.GetComponent<Renderer>().material = original;
                        this.gameObject.GetComponent<Renderer>().material = original;
                    }
                }
            }
        }
        else
        {
            if (nodes[0].connect == true)
            {
                connect = true;
                nodes[0].gameObject.GetComponent<Renderer>().material = connected;
                this.gameObject.GetComponent<Renderer>().material = connected;
            }
        }
    }
}
