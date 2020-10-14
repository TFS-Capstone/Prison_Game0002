using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{

    private PhotonView myPhotonView;
    [SerializeField]
    int multiplayerSceneIndex;
    [SerializeField]
    int menuSceneIndex;
    int playerCount;
    int roomSize;
    [SerializeField]
    int minPlayersToStart;

    // text variables for displays
    [SerializeField]
    Text roomCountDisplay;
    [SerializeField]
    Text timerToStartDisplay;

    // bools for if the timer can count down
    bool readyToCountDown;
    bool readyToStart;
    bool startingGame;
    // countdown timer
    float timerToStartGame;
    float notFullGameTimer;
    float fullGameTimer;

    [SerializeField]
    float maxWaitTime;
    [SerializeField]
    float maxFullGameWaitTime;







    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameWaitTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;
        PlayerCountUpdate();
    }

    // Update is called once per frame
    
    void PlayerCountUpdate()
    {
        //updates player count when player joins
        //displays player count
        //triggers countdown timer
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = playerCount + "/" + roomSize;

        if (playerCount == roomSize)
        {
            readyToStart = true;
        }
        else
        {
            readyToCountDown = false;
            readyToStart = false;
        }   
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();

        //send master clients countdown to all players in order to synce time
        if(PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        }
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        //rpc for syncing the countdown timer to those that join
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if(timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }

    void Update()
    {
        WaitingForMorePlayers();
    }
    void WaitingForMorePlayers()
    {
        if (playerCount <= 1)
        {
            ResetTimer();
        }

        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;

        }
        else if (readyToCountDown)
        {
            notFullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
        }

        // format and display countdown timer
        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;
        //if countdown reaches 0
        if (timerToStartGame <=0f)
        {
            if (startingGame)
                return;
            StartGame();
        }
    }

    void ResetTimer()
    {
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    void StartGame()
    {
        //multiplayer scene is loaded

        startingGame = true;
        if(!PhotonNetwork.IsMasterClient)
            return;        
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}
