using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThirdKeycardQuest : Quest
{
    private UnityAction keyListener3;
    Inventory invent;
    Notifications notif;
    private void Awake()
    {
        if (questName == "")
            questName = "Third keycard";
        if (description == "")
            description = "Obtain a level 3 keycard";
        if (questNumber <= 0)
            questNumber = 3;
        goal = new GatherGoal(1, 3, 3, this);

        keyListener3 = new UnityAction(doSomething);
        invent = GetComponent<Inventory>();

        notif = GameObject.FindGameObjectWithTag("Notifications").GetComponent<Notifications>();
    }

    private void OnEnable()
    {
        EventManager.StartListening("ThirdKeycardQuest", keyListener3);
    }

    private void OnDisable()
    {
        EventManager.StopListening("ThirdKeycardQuest", keyListener3);
    }


    public override void Complete()
    {
        base.Complete();
        this.completed = true;
        notif.setMessage(notif.KeycardCollected, notif.questCompleteText);
        EventManager.StopListening("ThirdKeycardQuest", keyListener3);
    }


    private void Update()
    {
        if (invent.keycard == 3)
        {
            doSomething();
        }
    }

    void doSomething()
    {
        goal.ItemObtained(goal.itemRequired, goal.itemRequiredID);


    }
}
