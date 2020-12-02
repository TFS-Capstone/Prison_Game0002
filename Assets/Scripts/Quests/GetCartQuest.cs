using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetCartQuest : Quest
{
    private UnityAction getCartListener;



    private void Awake()
    {
        if (questName == "")
            questName = "Get a Cart";
        if (description == "")
            description = "Find a cart to aid in your escape";
        if (questNumber <= 0)
            questNumber = 4;

        goal = new GatherGoal(1, 10, 10, this);

        getCartListener = new UnityAction(CheckIfCart);
    }

    private void OnEnable()
    {
        EventManager.StartListening("GetCartQuest", getCartListener);
    }
    private void OnDisable()
    {
        EventManager.StopListening("GetCartQuest", getCartListener);
    }

    public override void Complete()
    {
        base.Complete();
        this.completed = true;
        EventManager.StopListening("GetCartQuest", getCartListener);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckIfCart()
    {
        goal.ItemObtained(goal.itemRequired, goal.itemRequiredID);
    }
}
