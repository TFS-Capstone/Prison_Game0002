using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CartReachCafeQuest : Quest
{
    private UnityAction cartReachCafeListener;
    public Inventory invent;
    public GameObject objectNeeded;
    public float minX = 0;
    public float minZ = 0;
    public float maxX = 0;
    public float maxZ = 0;

    private void Awake()
    {
        if (questName == "")
        {
            questName = "Cart to Cafe";
        }
        if (description == "")
        {
            description = "Bring the cart to the cafe";
        }
        if (questNumber <= 0)
        {
            questNumber = 5;
        }

        invent = GetComponent<Inventory>();
        //objectNeeded = invent.curPushingObj;

        if (minX == 0 || maxX == 0 || minZ == 0 || maxZ == 0)
        {
            Debug.LogError("There are no bounds created on the quest " + questName);
        }
        else if (minX > maxX || minZ > maxZ)
        {
            Debug.LogError("Quest " + questName + " has errors in its bounds: a min value is larger than the max value");
        }
        else
        {
            goal = new ReachLocationGoal(this.objectNeeded, minX, maxX, minZ, maxZ, this);
        }

        cartReachCafeListener = new UnityAction(CheckWithinBounds);
    }

    private void OnEnable()
    {
        EventManager.StartListening("CartReachCafeQuest", cartReachCafeListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("CartReachCafeQuest", cartReachCafeListener);
    }


    private void Update()
    {
        Vector3 a = new Vector3(minX, 0, minZ);
        Vector3 b = new Vector3(minX, 0, maxZ);
        Vector3 c = new Vector3(maxX, 0, minZ);
        Vector3 d = new Vector3(maxX, 0, maxZ);
        Debug.DrawLine(a, b);
        Debug.DrawLine(a, c);
        Debug.DrawLine(b, d);
        Debug.DrawLine(c, d);
        if (invent.curPushingObj.name == "Cart")
            objectNeeded = invent.curPushingObj;
    }

    public void CheckWithinBounds()
    {
        if (objectNeeded)
        {
            goal.itemRequired = objectNeeded;
            goal.CheckBounds(goal.itemRequired, goal.minX, goal.maxX, goal.minZ, goal.maxZ);
        }
            
    }
    public override void Complete()
    {
        base.Complete();
        this.completed = true;
        EventManager.StopListening("CartReachCafeQuest", cartReachCafeListener);
    }
}
