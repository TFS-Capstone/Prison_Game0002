using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    /*
     * documentation https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
     * scripting api https://doc-api.photonengine.com/en/pun/v2/index.html
     */


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //connect to best master server
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the master " + PhotonNetwork.CloudRegion + " server");
        //base.OnConnectedToMaster();
    }
}
