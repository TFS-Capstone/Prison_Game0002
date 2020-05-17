using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Warden : MonoBehaviour
{
    // Start is called before the first frame update
    public suspicionMeter suspicionMeter;
    [SerializeField]
    private int type;
    private float susMetric;
    public float wardenActiveThreshold;
    [SerializeField]
    GameObject hacker;
    [SerializeField]
    GameObject miniGame;

    private float waitTime;
    public float startWaitTime;

    void Start()
    {
        // find the suspicion meter in the game, so the warden can access the meter's value
        if (suspicionMeter == null)
        {
            suspicionMeter = GameObject.FindGameObjectWithTag("SuspicionMeter").GetComponent<suspicionMeter>();
        }
        
        type = 0;

        // find the hacker in the game. the tag will change to "Hacker" when there are two players
        if (hacker == null)
        {
            hacker = GameObject.FindGameObjectWithTag("Player");
        }
        

        // find the hacking interface. this will later allow the warden to close the interface when the player attempts a hack
        // due to nature of minigameUI, it is not findable in scene, as it is immediately set to inactive. 
        // set in inspector to avoid errors
        if (miniGame == null)
        {
            miniGame = GameObject.FindGameObjectWithTag("MinigameUI");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // type 0 is passive warden. they wait until player's suspicion is high enough before become aware of player's actions
        if (type == 0)
        {
            if (suspicionMeter.area1Metric >= wardenActiveThreshold)
            {
                Debug.Log("Warden active");
                type = 1;
            }
        }
        // type 1 is active warden. they are aware of the player's activities and will block hacking until suspicion is lowered
        else if (type == 1)
        {
            if (miniGame.activeSelf == true && waitTime <= 0)
            {
                hacker.GetComponent<MinigameTrigger>().ExitMinigame();
                Debug.Log("Warden has blocked your access, attempt later");
                waitTime = startWaitTime;
            }
            else if (miniGame.activeSelf == true && waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else if (miniGame.activeSelf == false && waitTime < 0)
            {
                waitTime = 0;
            }
        }
    }
}
