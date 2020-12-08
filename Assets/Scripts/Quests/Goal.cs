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

    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

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

    public virtual void ItemObtained(GameObject item, int itemID)
    {
        if (item == itemRequired && !completed || itemID == itemRequiredID && !completed)
        {
            this.completed = true;
            
            Debug.Log("Item required obtained");
            quest.Complete();
        }
    }

    public virtual void GoalUpdate(int countNeeded, GameObject itemRequired, int itemRequiredID, float minX, float minZ, float maxX, float maxZ)
    {

    }

    public virtual bool CheckBounds(GameObject toCheck, float minX, float maxX, float minZ, float maxZ)
    {
        //Debug.Log(toCheck);
        // check if the object is within the bounds laid out in the quest
        if (toCheck.transform.position.x > minX && toCheck.transform.position.z > minZ
            && toCheck.transform.position.x < maxX && toCheck.transform.position.z < maxZ)
        {
            this.completed = true;
            quest.Complete();
            return true;
        }
        else
            return false;

    }
    public virtual void GainCamAccess()
    {

    }

}
