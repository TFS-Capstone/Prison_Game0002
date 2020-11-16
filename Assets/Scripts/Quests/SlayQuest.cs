using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayQuest : Quest
{
    private void Awake()
    {
        questName = "Slayer";
        description = "slay them";
        goal = new KillGoal(5, 0, this);
    }

    public override void Complete()
    {
        base.Complete();

    }
}
