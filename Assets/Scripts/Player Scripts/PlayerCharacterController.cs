using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    
    [SerializeField]
    float speed;
    float pushSpeed;
    [SerializeField]
    float speedMultiplier = 1;
    [HideInInspector]
    public int type;

    bool pushing = false;
    private void Start()
    {
        
        type = 0;
        pushSpeed = speed / 2;
    }

    void Update()
    {
        pushing = GetComponent<Inventory>().pushing;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMultiplier = 2;
            GameManager.instance.playerSpeed = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speedMultiplier = 0.5f;
            GameManager.instance.playerSpeed = 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            speedMultiplier = 1;
            GameManager.instance.playerSpeed = 1;
        }
        if (type ==  0)
        {
            PlayerMovement();
        }
        
    }
    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        if (!pushing)
        {
            Vector3 playerMovement = new Vector3(hor, 0f, ver) * (speed * speedMultiplier) * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        }
        else if (pushing)
        {
            Vector3 playerMovement = new Vector3(hor, 0f, ver) * (pushSpeed * speedMultiplier) * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        }
        
        
    }
}
