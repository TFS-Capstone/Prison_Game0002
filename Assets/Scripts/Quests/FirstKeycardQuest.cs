using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstKeycardQuest : Quest
{

    private UnityAction keyListener;
    Inventory invent;
    private void Awake()
    {
        invent = gameObject.GetComponent<Inventory>();
        questName = "First keycard";
        description = "Obtain a level 1 keycard";
        goal = new GatherGoal(1, 1, this);

        keyListener = new UnityAction(doSomething);
    }

    private void OnEnable()
    {
        EventManager.StartListening("FirstKeycardQuest", keyListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("FirstKeycardQuest", keyListener);
    }
    
    public override void Complete()
    {
        base.Complete();
        this.completed = true;
    }
    void doSomething()
    {
        goal.ItemObtained(goal.itemRequired, goal.itemRequiredID);
    }
    
}
