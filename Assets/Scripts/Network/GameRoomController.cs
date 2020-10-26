using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRoomController : MonoBehaviourPunCallbacks
{
    public int menuSceneIndex;
    private PhotonView myPhotonView;
    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DisconnectGame();
        }
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //disconnect all players
        DisconnectGame();
        
    }

    [PunRPC]
    public void RPC_DisconnectAllPlayers()
    {
        
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void DisconnectGame()
    {
        myPhotonView.RPC("RPC_DisconnectAllPlayers", RpcTarget.Others);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}
