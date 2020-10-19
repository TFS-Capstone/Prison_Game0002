using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    [SerializeField]
    GameObject dustinPrefab, roryPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //this is a test to create something, ideally this will be called after the characters have been selected
        CreatePlayer();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating player");
        // path: Photon; PhotonUnityNetworking; Resources; PhotonPrefabs
        GameObject p = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity);
        //if (p.GetPhotonView().IsMine)
        //{
        //    Camera mainCam = p.GetComponentInChildren<Camera>();
        //    mainCam = Camera.main;
            
        //}
    }

    public void SetPlayer(int choice)
    {






        CreatePlayer();
    }
}
