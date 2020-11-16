using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherGoal : Goal
{
    public int itemID;

    public GatherGoal(int amountNeeded, int itemID, Quest quest)
    {
        countCurrent = 0;
        countNeeded = amountNeeded;
        completed = false;
        this.quest = quest;
        this.itemID = itemID;
    }

    void ItemGathered(int itemID)
    {
        if (this.itemID == itemID)
        {
            Increment(1);
        }
    }

}
