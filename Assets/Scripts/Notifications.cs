using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notifications : MonoBehaviour
{
    public GameObject objectiveCompleteObj;
    public Text objCompleteText;
    public GameObject messageObj;
    public Text messageText;
    public GameObject bg;

    Queue messagesQueue;
    Queue objectiveQueue;
    public string objCompText;
    public string questCompleteText;
    public string camsAccessed;
    public string KeycardCollected;
    public string puzzle1Text;
    public string puzzle2Text;

    public bool notificationActive;
    // Start is called before the first frame update
    void Start()
    {
        if (objCompText == "")
        {
            objCompText = "Objective complete";
        }

        if (questCompleteText == "")
            questCompleteText = "Quest Complete! Press Tab to view next quest";
        if (camsAccessed == "")
            camsAccessed = "You now have access to the cams, " +
                "press C to access them, V and Z to switch";
        if (KeycardCollected == "")
            KeycardCollected = "Keycard collected ";
        if (puzzle1Text == "")
            puzzle1Text = "Hack complete. You can now access cams!";
        if (puzzle2Text == "")
            puzzle2Text = "Hack complete. All red doors are now unlocked";

        messagesQueue = new Queue();
        objectiveQueue = new Queue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            
            setMessage(objCompText, "");
        }
        if (messagesQueue.Count > 0)
            setMessage(messagesQueue.Dequeue().ToString(), objectiveQueue.Dequeue().ToString());
    }

    public void setMessage(string message, string objective)
    {

        messagesQueue.Enqueue(message);
        objectiveQueue.Enqueue(objective);

        if (!notificationActive)
        {
            messageText.text = messagesQueue.Dequeue().ToString();
            objCompleteText.text = objectiveQueue.Dequeue().ToString();
            bg.SetActive(true);
            objectiveCompleteObj.SetActive(true);
            messageObj.SetActive(true);
            notificationActive = true;
            StartCoroutine(Timer());
            
        }
        

    }
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(4);

        bg.SetActive(false);
        messageObj.SetActive(false);
        objectiveCompleteObj.SetActive(false);
        notificationActive = false;

    }
}
