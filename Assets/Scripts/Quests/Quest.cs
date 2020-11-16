using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string questName;
    public string description;
    public Goal goal;
    public bool completed;
    public List<string> itemRewards;

    public virtual void Complete()
    {
        Debug.Log("Quest Completed!");
        GrantReward();
    }

    public void GrantReward()
    {
        Debug.Log("Greanting reward");
        foreach (string item in itemRewards)
        {
            Debug.Log("Rewarded with: " + item);
        }
        Destroy(this);
    }

}
