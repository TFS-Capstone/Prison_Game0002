using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstKeycardQuest : Quest
{

    private UnityAction keyListener;
    Inventory invent;
    Notifications notif;
    private void Awake()
    {
        //invent = gameObject.GetComponent<Inventory>();
        if (questName == "")
            questName = "First keycard";
        if (description == "")
            description = "Obtain a level 1 keycard";
        if (questNumber <=0)
            questNumber = 1;
        goal = new GatherGoal(1, 1, 1, this);
        

        keyListener = new UnityAction(doSomething);
        notif = GameObject.FindGameObjectWithTag("Notifications").GetComponent<Notifications>();
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
        notif.setMessage(notif.KeycardCollected, notif.questCompleteText);
        EventManager.StopListening("FirstKeycardQuest", keyListener);
    }
    void doSomething()
    {
        goal.ItemObtained(goal.itemRequired, goal.itemRequiredID);
        
        
    }
    
}
