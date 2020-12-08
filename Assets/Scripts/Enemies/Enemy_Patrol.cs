using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_Patrol : MonoBehaviour
{
    
    
    //if the player is disguised
    [HideInInspector]
    public bool disguised = false;
    //the nav mesh manager
    NavMeshAgent nmAgent;
    //where the enemy is heading
    [SerializeField]
    Transform target;
    //the player
    public Transform player;
    //the preset patrols
    public GameObject[] patrolPath;
    //the current index of patrolPath[]
    int patrolIndex = 0;
    //the min distance to the node to switch to the next node
    public float nodeDistance = 2;
    public Animator GAnimator;
    float moveSpeed;


    //if the AI is in it's wandering state
    bool isWandering = false;
    //if the player has been seen
    [HideInInspector]
    public bool playerIsSeen = false;
    //the distance that the player needs to be to get away from the AI
    public float getAwayDistance = 25;

    #region wander variables
    //the time to wait before going to the next wander path
    float nextWanderPathWaitTime = 3;

    //the time that the enemy will search for
    [SerializeField]
    float searchTime = 15;
    float searchTimeRemaining = 0;

    GameObject searchCenter = null;
    [SerializeField]
    float searchDistance = 20;
    //temp variable for instantiating the search center

    [SerializeField]
    GameObject empty;
    #endregion
    bool distracted = false;

    #region clamps
    [SerializeField]
    float MAXX = 200;
    [SerializeField]
    float MINX = 200;
    [SerializeField]
    float MAXZ = 200;
    [SerializeField]
    float MINZ = 200;
    #endregion
    [SerializeField]
    LayerMask def = 0;
    List<GameObject> wanderNodes = new List<GameObject>();
    void Start()
    {
        //find the navMeshAgent
        nmAgent = GetComponent<NavMeshAgent>();
        //if there is a patrol path, then patrol
        if (patrolPath.Length > 0)
        {
            target = patrolPath[patrolIndex].transform;
            isWandering = false;
        }
        else //if not then wander
            isWandering = true;
        
        //if there is a navMesh and a target, then set the destination to the target;
        if (nmAgent && target)
            nmAgent.SetDestination(target.position);

        //reset the search time remaining
        searchTimeRemaining = searchTime;
    }

    void Update()
    {
        moveSpeed = nmAgent.velocity.x + nmAgent.velocity.y + nmAgent.velocity.z;
        if (moveSpeed < 0)
            moveSpeed = -moveSpeed;

        GAnimator.SetFloat("GWalking", moveSpeed);


        //check if the agent is null, because apparently that's something that has to be done every update loop?
        if (nmAgent == null)
        {
            nmAgent = GetComponent<NavMeshAgent>();
        }
        //check if the player is disguised
        
        //if the player is not seen
        if (!playerIsSeen)
        {
            //find out if the player is disguised
            disguised = GameManager.instance.disguised;
            //do this here so that the player cannot change in front of the guards
            

            if (distracted)
            {
                //Debug.Log("Still Distracted");
                //check the distance to the distraction hit (now target) and if its less
                if (Vector3.Distance(transform.position, target.position) < nodeDistance)
                {
                    //Debug.Log("Stopped distracted");
                    //the enemy wanders a bit looking for the player
                    isWandering = true;
                    //the enemy is no longer distracted
                    distracted = false;
                    playerIsSeen = false;
                    
                    //the search center is exactly where the enemy is standing
                    searchCenter = Instantiate(empty, transform.position, transform.rotation);
                    wanderNodes.Add(searchCenter);
                    FindNewLocation();
                }
                
            }
            else
                if (isWandering) //check if the enemy is in it's wander state (searching an area randomly)
                {

                    //wander him around
                    Wander();
                }
                else
                {
                //if there is a navMeshAgent and a target, then set the destination to the target
                if (nmAgent && target)
                         nmAgent.SetDestination(target.position);
                     //if the enemy is within the set minDistance, then go to the next patrol index
                    if (Vector3.Distance(transform.position, target.position) < nodeDistance)
                    {
                        //next patrol index
                        patrolIndex++;
                        //sets it back to zero when it reaches the end
                        patrolIndex %= patrolPath.Length;
                        //changes the target to the next index
                        target = patrolPath[patrolIndex].transform;

                        //checks again if there is still a navMeshAgent and a target, and sets the destination to the new target
                        if (nmAgent && target)
                        nmAgent.SetDestination(target.position);
                    }
                }
        }
        else
        {
            //if the player is seen, then find the distance to the player from the enemy
            float playerDst = Vector3.Distance(player.position, transform.position);
            //Debug.Log(playerDst);

            if (Physics.Linecast(transform.position, player.position, out RaycastHit hitInfo, def))
            {
                //Debug.Log("hit an object: " + hitInfo.collider.gameObject);
                Debug.Log("player hid behind an object");

                //create a search center at the enemy's position
                searchCenter = Instantiate(empty, transform.position, transform.rotation);
                //add it to be deleted later
                wanderNodes.Add(searchCenter);
                //create the first search point at the player's location
                GameObject newPoint = Instantiate(empty, player.transform.position, transform.rotation);
                //set the target to the point above
                target = newPoint.transform;
                //check if there is still a navMeshAgent and a target
                if (nmAgent && target)
                    nmAgent.SetDestination(target.position);
                //after setting the new position, destroy it
                wanderNodes.Add(newPoint);

                //the player is no longer seen
                playerIsSeen = false;
                //the enemy enters a state where it wanders for a while
                isWandering = true;


            }
            else
            if (playerDst >= getAwayDistance) //if the player is out side of the getaway distance
            {
                Debug.Log("player got away");
                //the player is no longer seen
                playerIsSeen = false;
                //the enemy enters a state where it wanders for a while
                isWandering = true;
                //the search center is exactly where the enemy is standing
                searchCenter = Instantiate(empty, transform.position, transform.rotation);
                //add it to be deleted later
                wanderNodes.Add(searchCenter);
                //create the first search point at the player's location
                GameObject newPoint = Instantiate(empty, player.transform.position, transform.rotation);
                //set the target to the point above
                target = newPoint.transform;
                //check if there is still a navMeshAgent and a target
                if (nmAgent && target)
                    nmAgent.SetDestination(target.position);
                //after setting the new position, destroy it
                wanderNodes.Add(newPoint);


            }
            else
            {
                if (!disguised)
                {
                    distracted = false;
                    //change the target to the player
                    target = player;
                    //checks again if there is still a navMeshAgent and a target, and sets the destination to the new target
                    if (nmAgent && target)
                        nmAgent.SetDestination(target.position);
                }
            }
            
        }
    }


    void Wander()
    {
      
        //if there is still search time remaining
        if (searchTimeRemaining >= 0)
        {
            //take away some search time
            searchTimeRemaining -= Time.deltaTime;
            if (Vector3.Distance(transform.position, target.position) <= nodeDistance)
            {
                FindNewLocation();
            }
        }
        else
        {
            //don't wander if there is no search time left
            isWandering = false;
            //destroy the nodes created to not clog up the scene
            for (int i = 1; i < wanderNodes.Count; i++)
            {
                Destroy(wanderNodes[i]);
                
            }
            //set the target back to the patrol index
            target = patrolPath[patrolIndex].transform;
            //reset the remaining search time
            searchTimeRemaining = searchTime;
        }
        
   
    }


    void FindNewLocation()
    {
        //find the min and max distance away from the search center
        float minX = searchCenter.transform.position.x - searchDistance;
        float maxX = searchCenter.transform.position.x + searchDistance;
        float minZ = searchCenter.transform.position.z - searchDistance;
        float maxZ = searchCenter.transform.position.z + searchDistance;

        float newLocX = Mathf.Clamp(Random.Range(minX, maxX),MINX,MAXX);
        float newLocZ = Mathf.Clamp(Random.Range(minZ, maxZ), MINZ, MAXZ);

        //get a new locataion within the provided space
        Vector3 newLoc = new Vector3(newLocX, transform.position.y, newLocZ);
        //create a new point at the vector above
        GameObject newPoint = Instantiate(empty, newLoc, transform.rotation);
        //set a new location for the nav mesh targert
        target = newPoint.transform;
        //check if there is still a navMeshAgent and a target
        if (nmAgent && target)
            nmAgent.SetDestination(target.position);
        //after setting the new position, destroy it
        wanderNodes.Add(newPoint);
    }

    public void Distract(GameObject hitLoc)
    {
        //set a new target to the location where the projectile hit
        target = hitLoc.transform;
        wanderNodes.Add(hitLoc);
        //check this again
        if (nmAgent && target)
            nmAgent.SetDestination(target.position);
        //the enemy is distracted
        distracted = true;
    }



    //make the player lose
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameManager.instance.lose();
        }
    }
}
