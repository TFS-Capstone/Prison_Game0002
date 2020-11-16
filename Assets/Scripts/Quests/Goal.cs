using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal 
{
    public int countNeeded;
    public int countCurrent;
    public bool completed;
    public GameObject itemRequired;
    public int itemRequiredID;
    public Quest quest;

   public void Increment(int amount)
    {
        countCurrent = Mathf.Min(countCurrent + 1, countNeeded);
        if(countCurrent >= countNeeded && !completed)
        {
            this.completed = true;
            Debug.Log("goal number Completed");
            quest.Complete();
        }
    }

    public void ItemObtained(GameObject item, int itemID)
    {
        if (item == itemRequired || itemID == itemRequiredID && !completed)
        {
            this.completed = true;
            Debug.Log("Item required obtained");
            quest.Complete();
        }
    }
}
