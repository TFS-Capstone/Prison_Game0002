using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckKey : MonoBehaviour
{
    public static bool minigame2End = false;
    public static bool minigame2Win = false;
    public static bool minigame2Lose = false;
    public Camera cameraD;
    bool clickReady = true;
    public GameObject MasterKey, winText;
    public GameObject[] KeySet = new GameObject[7];
    public GameObject[] CheckSet = new GameObject[7];
    public int Cube1, Cube2, Cube3;
    public int round = 0;
    int rightPlace, wrongPlace = 0;

    MasterCheck mC;


    // Start is called before the first frame update
    void Start()
    {
        mC = MasterKey.GetComponent<MasterCheck>();
        //get the MasterCheck script
        for (int i = 0; i < 7; i++)
        {
            KeySet[i].SetActive(false);
            //disable all the keys
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mC.minigame2End);
        KeySet[round].SetActive(true);
        if (Input.GetMouseButton(0))
        {
            var ray = cameraD.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100) && clickReady == true) //raycasting for mouse click
            {
                clickReady = false;
                StartCoroutine(ClickCooldown()); //co-routine to limit click speed
                    if (hit.collider.tag == "Checker")
                    {
                        if (Cube1 == mC.KeyMaster[0] && Cube2 == mC.KeyMaster[1] && Cube3 == mC.KeyMaster[2])
                        {
                            minigame2End = true;
                            minigame2Win = true;
                        mC.minigame2End = true;
                        winText.GetComponent<TextMesh>().text = "Win!";
                        GameManager.instance.playerCardLevel = 1;
                        } // if win conditions are met
                    if (round < 7)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            KeySet[round].GetComponentsInChildren<BoxCollider>()[i].enabled = false;
                            
                        }
                        if (Cube1 == mC.KeyMaster[0])
                        {
                            rightPlace++;
                            Debug.Log("right1");
                        }
                            else if (Cube1 == mC.KeyMaster[1] || Cube1 == mC.KeyMaster[2])
                        {
                            wrongPlace++;
                            Debug.Log("wrong1");
                        }

                        if (Cube2 == mC.KeyMaster[1])
                        {
                            rightPlace++;
                            Debug.Log("right2");
                        }
                            else if (Cube2 == mC.KeyMaster[0] || Cube2 == mC.KeyMaster[2])
                        {
                                wrongPlace++;
                            Debug.Log("wrong2");
                        }

                        if (Cube3 == mC.KeyMaster[2])
                        {
                            rightPlace++;
                            Debug.Log("right3");
                        }
                          else if (Cube3 == mC.KeyMaster[1] || Cube3 == mC.KeyMaster[0])
                            {
                                wrongPlace++;
                            Debug.Log("wrong3");
                        }

                        if (rightPlace > 0)
                        {
                            for (int p = 0; p < rightPlace; p++)
                                CheckSet[round].GetComponentsInChildren<Renderer>()[p].material.SetColor("_Color", Color.red);
                        }
                        if (wrongPlace > 0)
                        {
                            for (int p = 0; p < wrongPlace; p++)
                                CheckSet[round].GetComponentsInChildren<Renderer>()[2 - p].material.SetColor("_Color", Color.grey);
                        }

                        Cube1 = 0;
                        Cube2 = 0;
                        Cube3 = 0;
                        rightPlace = 0;
                        wrongPlace = 0;    
                        round++;
                    } //advancing to the next round
                        if (round == 7 && minigame2Win == false)
                        {
                        round = 0;
                        Cube1 = 0;
                        Cube2 = 0;
                        Cube3 = 0;
                        rightPlace = 0;
                        wrongPlace = 0;

                        //winText.GetComponent<TextMesh>().text = "Lose!";
                        for (int i = 0; i < 7; i++)
                        {
                            KeySet[i].SetActive(true);

                            for (int j = 0; j <= 2; j++)
                            {
                                CheckSet[i].GetComponentsInChildren<Renderer>()[j].material.SetColor("_Color", Color.white);
                                KeySet[i].GetComponentsInChildren<BoxCollider>()[j].enabled = true;
                                KeySet[i].GetComponentsInChildren<Renderer>()[j].material.SetColor("_Color", Color.white);
                            }
                            if ( i > 0)
                            KeySet[i].SetActive(false);
                            mC.minigameReset = true;
                        }
                    } //if all the rounds are spent
                       
                    }
                if (hit.collider.tag == "Cube1")
                {
                    Debug.Log(round);
                    if (Cube1 == 5)
                        Cube1 = 0;
                    else
                        Cube1++;

                    switch (Cube1)
                    {
                        case 0:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        
                            break;
                        case 1:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                           
                            break;
                        case 2:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                            
                            break;
                        case 3:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                            
                            break;
                        case 4:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                           
                            break;
                        case 5:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                            
                            break;

                        default:
                            break;
                    }

                } //When you click on the top cube
                if (hit.collider.tag == "Cube2")
                {
                    if (Cube2 == 5)
                        Cube2 = 0;
                    else
                        Cube2++;

                    switch (Cube2)
                    {
                        case 0:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                           
                            break;
                        case 1:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                          
                            break;
                        case 2:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                           
                            break;
                        case 3:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                            
                            break;
                        case 4:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                           
                            break;
                        case 5:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                           
                            break;

                        default:
                            break;
                    }

                } //when you click on the middle cube
                if (hit.collider.tag == "Cube3")
                {
                    if (Cube3 == 5)
                        Cube3 = 0;
                    else
                        Cube3++;

                    switch (Cube3)
                    {
                        case 0:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                          
                            break;
                        case 1:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                           
                            break;
                        case 2:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                           
                            break;
                        case 3:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                            
                            break;
                        case 4:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                          
                            break;
                        case 5:
                            hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                            
                            break;

                        default:
                            break;
                    }

                }//when you click the bottom cube
            }
        }
      
        // When you press "Check", it should check the colors, input the findings into the smaller squares, and increment a round
       
    }
   IEnumerator ClickCooldown()
    {
        yield return new WaitForSeconds(0.15f);
        clickReady = true;
    }//co-routine to limit click speed to controllable levels
}
