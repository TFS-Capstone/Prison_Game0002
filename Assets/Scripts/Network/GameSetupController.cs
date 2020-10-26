using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameSetupController : MonoBehaviour
{
    private PhotonView myPhotonView;
    [SerializeField]
    GameObject dustinPrefab, roryPrefab;
    [SerializeField]
    int dustinChoice;
    [SerializeField]
    int roryChoice;
    [SerializeField]
    Transform playerSpawnPoint, carSpawnPoint;
    [SerializeField]
    GameObject dustinSelect, rorySelect, startButton, disconnectButton;

    int myChoice;
    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        myChoice = 0;
        dustinChoice = 0;
        roryChoice = 0;
        //this is a test to create something, ideally this will be called after the characters have been selected
        //CreatePlayer();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePlayer(int choice)
    {
        if (choice == 1)
        {
            Debug.Log("Creating player");
            // path: Photon; PhotonUnityNetworking; Resources; PhotonPrefabs
            GameObject p = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), playerSpawnPoint.position, Quaternion.identity);

        }
        else if (choice ==2)
        {
            Debug.Log("Creating car");
            // path: Photon; PhotonUnityNetworking; Resources; PhotonPrefabs
            GameObject p = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Car"), carSpawnPoint.position, Quaternion.identity);

        }
        //if (p.GetPhotonView().IsMine)
        //{
        //    Camera mainCam = p.GetComponentInChildren<Camera>();
        //    mainCam = Camera.main;

        //}
    }

    public void SetPlayer(int choice)
    {
        if (choice == 1 && dustinChoice != 1 && myChoice == 0)
        {
            //dustin Selected

            //disable the choice ui
            myChoice = choice;
            Debug.Log("my choice is " + myChoice);
            myPhotonView.RPC("RPC_SendPlayerChoice", RpcTarget.All, choice);
        }

        else if (choice == 2 && roryChoice != 2 && myChoice == 0)
        {
            //rory selected

            //disable the choice ui
            myChoice = choice;
            Debug.Log("my choice is " + myChoice);
            myPhotonView.RPC("RPC_SendPlayerChoice", RpcTarget.All, choice);
        }

        //CreatePlayer();
    }


    [PunRPC]
    public void RPC_SendPlayerChoice(int choice)
    {
        //rpc to send and synce to all players
        
        if (choice ==1)
        {
            dustinChoice = 1;
            Debug.Log("syncing player choices " + choice);
        }
        else if (choice ==2)
        {
            roryChoice = 2;
            Debug.Log("syncing player choices " + choice);
        }

    }

    [PunRPC]
    public void RPC_LoadBothPlayers()
    {
        CreatePlayer(myChoice);
    }

    [PunRPC]
    public void RPC_RemoveSelectionOpts()
    {
        dustinSelect.SetActive(false);
        rorySelect.SetActive(false);
        startButton.SetActive(false);

        disconnectButton.SetActive(true);
    }
    public void StartGame()
    {
        // only the master can start the server / game
        if (PhotonNetwork.IsMasterClient)
        {
            if (dustinChoice == 1 && roryChoice == 2)
            {
                Debug.Log("spawning both players");

                myPhotonView.RPC("RPC_LoadBothPlayers", RpcTarget.All);
                myPhotonView.RPC("RPC_RemoveSelectionOpts", RpcTarget.All);
            }
        }
        
    }

    

}
