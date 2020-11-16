using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherGoal : Goal
{
    public int itemID;

    
    public GatherGoal(int amountNeeded, int itemID, int itemRequiredID, Quest quest)
    {
        countCurrent = 0;
        countNeeded = amountNeeded;
        completed = false;
        this.quest = quest;
        this.itemID = itemID;
        this.itemRequiredID = itemRequiredID;
    }

    public override void ItemObtained(GameObject item, int itemID)
    {
        if (this.itemID == itemID)
        {
            
            Increment(1);
        }
        Debug.Log("item # "+ this.itemID);

    }

    //public override void GoalUpdate(int countNeeded, GameObject itemRequired, int itemRequiredID, float minX, float minZ, float maxX, float maxZ)
    //{
        
    //}

}
