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

    public Transform cam;
    public CharacterController controller;
    public float turnSmoothTime = 0.1f;
    float smoothVelocity;

    bool pushing = false;
    private void Start()
    {
        
        type = 0;
        pushSpeed = speed / 2;
    }

    void FixedUpdate()
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
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            if (!pushing)
            {
                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }
            }
            else if (pushing)
            {

                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    controller.Move(moveDir.normalized * pushSpeed * Time.deltaTime);
                }
            }
        }
        
    }

}
