using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNode : MonoBehaviour
{
    public int number = 0;
    public bool connect;
    public int connectnumber;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BlockNode")
        {
            if (other.gameObject.GetComponent<BlockNode>().connect == true)
            {
                connectnumber = other.gameObject.GetComponent<BlockNode>().number;
                connect = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BlockNode")
        {
            if (other.gameObject.GetComponent<BlockNode>().connect == true)
            {
                connect = false;
                connectnumber = number;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BlockNode")
        {
            if (other.transform.gameObject.GetComponent<BlockNode>().connect == true)
            {
                connectnumber = other.gameObject.GetComponent<BlockNode>().number;
                connect = true;
            }
        }
    }
}
