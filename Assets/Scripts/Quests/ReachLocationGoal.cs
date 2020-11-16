using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachLocationGoal : Goal
{
    public Transform location;
    public GameObject objectNeededToReachLocation;
   
    
    public ReachLocationGoal(Transform location, GameObject objectNeeded, float minX, float maxX, float minZ, float maxZ, Quest quest)
    {
        this.location = location;
        this.objectNeededToReachLocation = objectNeeded;
        completed = false;
        
        this.minX = minX;
        this.maxX = maxX;
        this.minZ = minZ;
        this.maxZ = maxZ;
        this.quest = quest;
    }
}
