using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    //the way we are pushing the objects is by attaching them to the player, in the Inventory script. This script will have the way to rest the object by the AI
    Rigidbody rb;
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public float pushForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (pushForce <=0)
        {
            pushForce = 10;
        }
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetObjectPosition();
            Debug.Log("reseting");
        }
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        Vector3 contactPoint = collision.GetContact(0).point;
    //        Vector3 direction = contactPoint - transform.position;
    //        direction.y = 0;
    //        direction = -direction.normalized;
    //        rb.AddForce(direction * pushForce, ForceMode.Impulse);
    //        Debug.Log("pushed " + direction);
    //    }
    //}
    public void ResetObjectPosition()
    {
        gameObject.transform.parent = null;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        
    }
}
