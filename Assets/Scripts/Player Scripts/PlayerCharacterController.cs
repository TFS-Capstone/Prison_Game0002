using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using Photon.Pun;
=======
using UnityEngine.UI;

>>>>>>> Greg_Mallin-Minigame_2

public class PlayerCharacterController : MonoBehaviour
{
    private PhotonView pv;
    [SerializeField]
    float speed;
    [SerializeField]
    float speedMultiplier = 1;

<<<<<<< HEAD
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = true;
        }
        else
        {
            GetComponentInChildren<Camera>().enabled = false;
=======
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
>>>>>>> Greg_Mallin-Minigame_2
        }
    }

    void Update()
    {
        if (pv.IsMine)
        {
<<<<<<< HEAD
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
            PlayerMovement();
        }
        
=======
            speedMultiplier = 1;
            GameManager.instance.playerSpeed = 1;
            playerController.SetBool("IsSneaking", false);
            playerController.SetBool("IsRunning", false);
        }
        PlayerMovement();
        aniText.text = "Walking: " + playerController.GetFloat("MoveSpeed") + " Running: " + playerController.GetBool("IsRunning") + " Sneaking: " + playerController.GetBool("IsSneaking");

       
        

>>>>>>> Greg_Mallin-Minigame_2
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
