using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemCheck : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject keycardCheck;
    public int keycardCheckID;
    public int itemToCheckID;

    private void Start()
    {
        playerInventory = FindObjectOfType<Inventory>();
        Debug.Log(playerInventory.gameObject);
    }

    private void Update()
    {
        if (keycardCheck != null)
        {
            if (playerInventory.keycardObject == keycardCheck || playerInventory.keycard == keycardCheckID)
            {
                Debug.Log("KeyCard collected");
                Destroy(this);
            }
        }
        if (playerInventory.item != null)
        {
            if (playerInventory.item.GetComponent<Items>().type == itemToCheckID)
            {
                Debug.Log("Item Collected");
                Destroy(this);
            }
        }
    }
}
