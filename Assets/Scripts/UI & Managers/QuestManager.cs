using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static bool questsDisplayed = false;
    public GameObject questName;
    public GameObject questDescription;
    public Text questNameText;
    public Text questDescriptionText;

    GameObject player;

    public List<Quest> listOfQuestsActive;
    public List<Quest> listOfQuestsInactive;

    private UnityAction questListener;
    private UnityAction nextQuestListener;

    Quest currentQuest;

    private void Awake()
    {
        questListener = new UnityAction(Quests);
        nextQuestListener = new UnityAction(NextQuestActivate);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        questName.SetActive(false);
        questDescription.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable()
    {
        EventManager.StartListening("Quests", questListener);
        // NextQuest triggered in Quest on complete
        EventManager.StartListening("NextQuest", nextQuestListener);
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.questsDisplayed = questsDisplayed;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EventManager.TriggerEvent("Quests");
        }

    }
    void Quests()
    {
        
        if (questsDisplayed)
        {
            MinimizeQuests();
        }
        else if (!questsDisplayed)
        {
            
            
            QuestListUpdate();
            //Debug.Log(listOfQuests.Count);
            if(listOfQuestsActive.Count > 0)
            {
                QuestTextUpdate();
                DisplayQuests();
            }
            
        }

    }

    public void DisplayQuests()
    {
        questName.SetActive(true);
        questDescription.SetActive(true);
        questsDisplayed = true;
    }

    public void MinimizeQuests()
    {
        questName.SetActive(false);
        questDescription.SetActive(false);
        questsDisplayed = false;
    }

    public void QuestListUpdate()
    {

        listOfQuestsActive = new List<Quest>();
        listOfQuestsInactive = new List<Quest>();
        
        foreach (Quest quest in player.GetComponents<Quest>())
        {
            if (quest.isActiveAndEnabled)
            {
                listOfQuestsActive.Add(quest);
                //Debug.Log(quest);
            }
            if (!quest.isActiveAndEnabled)
            {
                listOfQuestsInactive.Add(quest);
            }

        }
    }

    public void QuestTextUpdate()
    {

        currentQuest = listOfQuestsActive[0];
        //currentQuest = GameObject.FindGameObjectWithTag("Player").GetComponent<Quest>();
        questNameText.text = currentQuest.questName.ToString();
        questDescriptionText.text = currentQuest.description.ToString();

    }

    public void NextQuestActivate()
    {
        //Debug.Log("Called");
        QuestListUpdate();
        QuestTextUpdate();
        listOfQuestsActive.RemoveAt(0);


        Destroy(currentQuest);
        if( listOfQuestsInactive.Count>0)
        {
            listOfQuestsInactive[0].enabled = true;
            listOfQuestsInactive.RemoveAt(0);
        }
        
        //player.GetComponent<Quest>().enabled = true;
    }
}
