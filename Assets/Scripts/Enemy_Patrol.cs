using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_Patrol : MonoBehaviour
{
    public bool doLooking = false;
    public bool isLooking = false;
    public bool disguised = false;
    public float speed;
    NavMeshAgent nmAgent;
    public Transform target;
    public Transform player;
    public Transform playerBack;
    public GameObject[] patrolPath;
    public int patrolIndex;
    public float nodeDistance;
    public string pathName;

    public bool generatePath;

    // Start is called before the first frame update
    void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        patrolIndex = 0;
        //nodeDistance = 1.0f;


        if (generatePath)
        {
            patrolPath = GameObject.FindGameObjectsWithTag(pathName);
        }


        if (patrolPath.Length > 0)
            target = patrolPath[patrolIndex].transform;
        
        if (nmAgent && target)
            nmAgent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        disguised = GameManager.instance.disguised;
        if (disguised)
        {
            target = patrolPath[patrolIndex].transform;
        }
        nmAgent = GetComponent<NavMeshAgent>();


        if (nmAgent && target)
            nmAgent.SetDestination(target.position);
        
        if (Vector3.Distance(transform.position, target.position) < nodeDistance)
        {
            patrolIndex++;
            patrolIndex %= patrolPath.Length;
            target = patrolPath[patrolIndex].transform;
            

            if (nmAgent && target)
                nmAgent.SetDestination(target.position);
        }
        
        float playerDistance = Vector3.Distance(player.position, transform.position);
        float playerBackDistance = Vector3.Distance(playerBack.position, transform.position);

        if (doLooking)
        if (playerDistance > playerBackDistance)
        {
            isLooking = false;
            nmAgent.speed = speed;
        }
        else
        {
            isLooking = true;
            nmAgent.speed = 0;
        }

        if (playerBackDistance < 2)
        {
            GameManager.instance.lose();
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
            if (!disguised)
            {
                target = player;
            }
            

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
            target = patrolPath[patrolIndex].transform;
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag.ToString());
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("You Lose");
        }
    }
    */
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Doot");
    }
}
