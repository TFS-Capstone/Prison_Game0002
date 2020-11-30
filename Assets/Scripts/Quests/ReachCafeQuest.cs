using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReachCafeQuest : Quest
{

    private UnityAction reachCafeListener;
    public float minX=0;
    public float minZ=0;
    public float maxX=0;
    public float maxZ=0;

    private void Awake()
    {
        questName = "Cafe Tier";
        description = "Find and reach the Cafeteria";
        questNumber = 2;
        if ( minX ==0 || maxX == 0|| minZ == 0 || maxZ == 0)
        {
            goal = new ReachLocationGoal(this.gameObject, 0, 15, 8, 18, this);
        }
        else
        {
            goal = new ReachLocationGoal(this.gameObject, minX, maxX, minZ, maxZ, this);
        }
        

        reachCafeListener = new UnityAction(CheckWithinBounds);
    }
    private void OnEnable()
    {
        EventManager.StartListening("CheckPlayerBounds", reachCafeListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("CheckPlayerBounds", reachCafeListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckWithinBounds()
    {
        goal.CheckBounds(goal.itemRequired, goal.minX, goal.maxX, goal.minZ, goal.maxZ);
    }
}
