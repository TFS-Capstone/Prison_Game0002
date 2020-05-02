using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PingObject : MonoBehaviour
{
    private float lifeTime;
    private GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 4.0f;
        Destroy(gameObject, lifeTime);
        character = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(character.transform);
    }

    
}
