using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float speedMultiplier = 1;

    public Animator playerController;
    public Text aniText;
    void Update()
    {
        //playerController.gameObject.SetActive(true);
      
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMultiplier = 2;
            GameManager.instance.playerSpeed = 2;
            
            playerController.SetBool("IsRunning", true);
          

            //aniText.text = playerController.GetBool("IsRunning").ToString();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            speedMultiplier = 0.5f;
            GameManager.instance.playerSpeed = 0.5f;
            
            playerController.SetBool("IsSneaking", true);
            //aniText.text = playerController.GetBool("IsSneaking").ToString();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            speedMultiplier = 1;
            GameManager.instance.playerSpeed = 1;
            playerController.SetBool("IsSneaking", false);
            playerController.SetBool("IsRunning", false);
        }
        PlayerMovement();
        aniText.text = "Walking: " + playerController.GetFloat("MoveSpeed") + " Running: " + playerController.GetBool("IsRunning") + " Sneaking: " + playerController.GetBool("IsSneaking");

       
        

    }
    void PlayerMovement()

    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * (speed * speedMultiplier) * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
        playerController.SetFloat("MoveSpeed", 1);
        Debug.Log("PlayerMovement");
    }
}
