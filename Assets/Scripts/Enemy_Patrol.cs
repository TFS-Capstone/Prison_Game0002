using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_Patrol : MonoBehaviour
{

    public bool isLooking = false;

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
        /*
        float playerDistance = Vector3.Distance(player.position, transform.position);
        float playerBackDistance = Vector3.Distance(playerBack.position, transform.position);
        if (playerDistance > playerBackDistance)
        {
            isLooking = false;
            nmAgent.speed = 2.5f;
        }
        else
        {
            isLooking = true;
            nmAgent.speed = 0;
        }
        */
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
            target = player;
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
            target = patrolPath[patrolIndex].transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("You lose");
        }
    }
}
