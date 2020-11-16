using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public int enemyID;

    public KillGoal(int amountNeeded, int enemyID, Quest quest)
    {
        countCurrent = 0;
        countNeeded = amountNeeded;
        completed = false;
        this.quest = quest;
        this.enemyID = enemyID;
    }

    void EnemyKilled(int enemyID)
    {
        if (this.enemyID == enemyID)
        {
            Increment(1);
        }
    }
}
