using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecondKeycardQuest : Quest
{
    private UnityAction keyListener2;
    Inventory invent;


    Notifications notif;
    private void Awake()
    {
        //invent = gameObject.GetComponent<Inventory>();
        if (questName == "")
            questName = "Second keycard";
        if (description == "")
            description = "Obtain a level 2 keycard";
        if (questNumber <= 0)
            questNumber = 3;
        goal = new GatherGoal(1, 2, 2, this);

        invent = GetComponent<Inventory>();
        keyListener2 = new UnityAction(doSomething);
        notif = GameObject.FindGameObjectWithTag("Notifications").GetComponent<Notifications>();
    }

    private void OnEnable()
    {
        EventManager.StartListening("SecondKeycardQuest", keyListener2);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SecondKeycardQuest", keyListener2);
    }

    public override void Complete()
    {
        base.Complete();
        this.completed = true;
        notif.setMessage(notif.KeycardCollected, notif.questCompleteText);
        EventManager.StopListening("SecondKeycardQuest", keyListener2);
    }
    void doSomething()
    {
        goal.ItemObtained(goal.itemRequired, goal.itemRequiredID);
    }
    private void Update()
    {
        if (invent.keycard==2)
        {
            doSomething();
        }
    }
}
