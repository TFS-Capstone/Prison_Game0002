using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetAccessToCamsQuest : Quest
{

    private UnityAction accessCamsListener;
    Notifications notif;

    private void OnEnable()
    {
        EventManager.StartListening("AccessCamsQuest", accessCamsListener);
    }
    private void OnDisable()
    {
        EventManager.StopListening("AccessCamsQuest", accessCamsListener);
    }
    void Start()
    {
        
    }
    private void Awake()
    {
        if (questName == "")
        {
            questName = "Get Access to cams";
        }
        if (description == "")
        {
            description = "Find and hack the camera system";
        }
        if (questNumber <= 0)
        {
            questNumber = 2;
        }
        accessCamsListener = new UnityAction(GainCamAccess);
        notif = GameObject.FindGameObjectWithTag("Notifications").GetComponent<Notifications>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GainCamAccess()
    {
        this.completed = true;
        this.Complete();
        notif.setMessage(notif.camsAccessed, notif.questCompleteText);
    }
}
