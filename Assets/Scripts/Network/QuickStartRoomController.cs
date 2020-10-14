using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    int multiplayerSceneIndex; // number for the build index to the multiplay scene


    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() // callback func for successfully creating or joining a room
    {
        Debug.Log("Joined Room");
        StartGame();
    }

    private void StartGame() // load into multiplayer scene
    {
        //check if master client
        if (PhotonNetwork.IsMasterClient)
        {
            
            Debug.Log("Starting Game");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex); 
        }
    }
}
