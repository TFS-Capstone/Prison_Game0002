using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachLocationGoal : Goal
{
    public Transform location;
    public GameObject objectNeededToReachLocation;
   
    
    public ReachLocationGoal(GameObject objectNeeded, float minX, float maxX, float minZ, float maxZ, Quest quest)
    {
        //this.location = location;
        //objectNeededToReachLocation = objectNeeded;
        itemRequired = objectNeeded;
        completed = false;
        
        this.minX = minX;
        this.maxX = maxX;
        this.minZ = minZ;
        this.maxZ = maxZ;
        this.quest = quest;
    }

    public override bool CheckBounds(GameObject toCheck, float minX, float maxX, float minZ, float maxZ)
    {
        if (base.CheckBounds(toCheck, minX, maxX, minZ, maxZ))
        {
            Debug.Log("withinBounds");
            completed = true;
            return true;
        }
        else
        {
            //Debug.Log("not within Bounds");
            return false;
        }
            
        

    }
}
