using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachLocationCheck : MonoBehaviour
{
    public GameObject itemToCheck;

    private void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == itemToCheck)
        {
            Debug.Log("Item reached location");
            Destroy(this);
        }
    }
}
