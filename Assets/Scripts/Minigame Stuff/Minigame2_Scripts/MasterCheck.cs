using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCheck : MonoBehaviour
{
    public int[] KeyMaster = new int[3];
    public GameObject[] EndKeys = new GameObject[3];
    public bool minigame2End = false;
    public bool minigameReset = false;
    // Start is called before the first frame update
    void Start()
    {
        Redo();
       
        //Debug.Log(KeyMaster[0]);
        //Debug.Log(KeyMaster[1]);
        //Debug.Log(KeyMaster[2]);
       
        //Debug Log for Endkeys
    }

    // Update is called once per frame
    void Update()
    {
        if (minigame2End == true)
        {
           
            for (int i = 0; i < 3; i++)
            {
                switch (KeyMaster[i])
                {
                    case 0:
                        EndKeys[i].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        Debug.Log("red");
                        break;
                    case 1:
                        EndKeys[i].GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                        Debug.Log("ylw");
                        break;
                    case 2:
                        EndKeys[i].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        Debug.Log("grn");
                        break;
                    case 3:
                        EndKeys[i].GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                        Debug.Log("cyn");
                        break;
                    case 4:
                        EndKeys[i].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                        Debug.Log("blu");
                        break;
                    case 5:
                        EndKeys[i].GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
                        Debug.Log("mgn");
                        break;

                    default:
                        break;
                }/* Show colours of Endkeys once game is finished.
                Red = 0, Yellow = 1, Green = 2, Cyan = 3, Blue = 4, Magenta = 5
                 
                 */
            }
        }
        if (minigameReset == true)
        {
            for (int i = 0; i < 3; i++)
            {
                KeyMaster[i] = Random.Range(0, 6);
            }
            minigameReset = false;
            Debug.Log(KeyMaster[0]);
            Debug.Log(KeyMaster[1]);
            Debug.Log(KeyMaster[2]);
        }
    }

    public void Redo()
    {
        for (int i = 0; i < 3; i++)
        {
            KeyMaster[i] = Random.Range(0, 6);
            EndKeys[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            // Randomly generate the end keys

        }

        minigameReset = true;
    }
}
