using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTemp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            GameManager.instance.win();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}