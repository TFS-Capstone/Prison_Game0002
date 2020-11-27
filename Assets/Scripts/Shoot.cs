using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    
    public GameObject projectileToSpawn;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform projSpawnPoint;
    //projectile and projectilePrefab will be whatever item the player picks up to throw, may not need both of these
    bool readyToFire = false;

    private Rigidbody _projRb;
    private Projectile _projectile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //here we can update the inventory of the player to search for an object that can be thrown, rather than using a prefab
        /*
         * if (inventory has throwable item && projectileToSpawn == null)
         *      projectileToSpawn = throwable item in inventory
         *      
         * if (inventory does not have throwable item && projectileToSpawn != null)
         *      projectileToSpawn = null;
         */


        if(!readyToFire && Input.GetMouseButton(1))
        {
            

            //if the item exists
            if (projectileToSpawn)
            {
                readyToFire = true;
                projectileToSpawn.SetActive(true);
                Debug.Log("ready to fire");

                // spawn the item to throw / fire
                // can change the spawn position to another position, such as the hand, by creating a reference to a new position
                projectile = Instantiate(projectileToSpawn, projSpawnPoint.position, transform.rotation) as GameObject;
                // set the parent of the spawned object to the player
                projectile.transform.parent = transform;
                gameObject.GetComponent<Inventory>().throwableObject.SetActive(false);
            }
            
        }
        else if(readyToFire && Input.GetMouseButtonUp(1))
        {
            readyToFire = false;
            projectileToSpawn.SetActive(false);
            if (projectile)
            {
                Debug.Log("despawning object if still available");
                _projectile = null;
                _projRb = null;
                Destroy(projectile);
                //disable / destroy the object as it was not thrown
                // esentially place back into inventory
            }
            
        }

        // if the projectile is ready to be tossed
        if (projectile)
        {
            ShootLogic();
        }
    }

    void ShootLogic()
    {
        _projectile = projectile.GetComponent<Projectile>();
        _projRb = projectile.GetComponent<Rigidbody>();

        float maxAimDistance = 25.0f;
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, maxAimDistance))
        {
            Debug.Log("looking at: " + hit.transform.name);
            Debug.DrawRay(transform.position, ray.direction*4, Color.green, 0.2f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            projectile.transform.parent = null;

            projectileToSpawn = null;
            Inventory invent = GetComponent<Inventory>();
            invent.throwableObject = null;

            _projectile.initialDirection = ray.direction;
            _projectile.enabled = true;
            projectile = null;
            _projectile = null;
            _projRb = null;
            readyToFire = false;
            // remove throable item from inventory
        }
    }
}
