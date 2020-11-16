using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class suspicionMeter : MonoBehaviour
{
    //instance for suspicion meter
    static suspicionMeter _instance = null;

    #region old variables idk anymore
    //constant values for time between adding, and the values at which the meter does somthing new
    const float WAIT_TIME = 5, MAX1 = 5, MAX2 = 7, MAX3 = 10;
    //use this boolean to say that the player is in the camera range in area 1
    public bool area1;
    //booleans that allow the meter to wait before adding to the meter again
    bool canAdd = true;
    bool canRemove = true;
    //the actual number that the meter is at for area 1
    [HideInInspector]
    public float area1Metric;
    //the temporary text box for the meter
    //public Text meter;
    #endregion

    //the enemies
    [SerializeField]
    GameObject[] enemies = null;
    //range that the enemy can hear from (goes through walls)
    public float range = 20;



    void Start()
    {
        //instacnce stuff
        if (_instance)
            Destroy(gameObject);
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this);
        }
        //other stuff in start
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
    }

    //creates the instance for the suspicion meter
    public static suspicionMeter instance
    {
        get { return _instance; }
        set { instance = value; }
    }

    void FixedUpdate()
    {
   
        
        #region old out of date
        /*
        //updates the text value for area 1
        //meter.text = area1Metric.ToString();

        //if the player is in the camera, and its not running another add, it adds to the meter
        if (area1 && canAdd)
        {
            canAdd = false;
            StartCoroutine(add());
        }

        //if the suspicion is above the set value for level 1 but not level 2, then do the function 1
        if (area1Metric >= MAX1 && area1Metric < MAX2)
        {
            area1AboveNum1();
        }
        //if the suspicion is above the set value for level 1 and level 2 then do the function 2
        else if (area1Metric > MAX1 && area1Metric >= MAX2)
        {
            area1AboveNum2();
        }

        //if the suspicion for the area is above 0, and the player is not in camera sight, then take away from the meter if it is not running another remove
        if (area1Metric > 0 && !area1 && canRemove)
        {
            canRemove = false;
            StartCoroutine(remove());
        }
        */
        #endregion
        
    }

    public void DistractEnemies(Transform projHitLoc)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(enemies[i].transform.position, projHitLoc.position) < range)
            {
                enemies[i].GetComponent<Enemy_Patrol>().Distract(projHitLoc);
            }
        }
    }

    #region old shit that I don't want to look at
    //increse suspiscion for the first area
    public void increseArea1()
    {
        area1Metric += 1;
    }
    //decrease suspiscion for the first area
    public void decreseArea1()
    {
        area1Metric -= 1;
    }

    //when the meter is above the first threashold, do this
    void area1AboveNum1()
    {
        Debug.Log("SUSPICION LEVEL 1");
    }
    //when the meter is above the second threashold, do this
    void area1AboveNum2()
    {
        GameManager.instance.lose();
    }

    //add to meter every WAIT_TIME seconds
    IEnumerator add()
    {
        yield return new WaitForSeconds(WAIT_TIME);
        canAdd = true;
        increseArea1();

    }
    //remove from meter every WAIT_TIME seconds
    IEnumerator remove()
    {
        yield return new WaitForSeconds(WAIT_TIME);
        canRemove = false;
        decreseArea1();
    }
    #endregion

}
