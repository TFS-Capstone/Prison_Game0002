using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

    private Color current;
    private Color active;
    private Color notActive;
    private SpriteRenderer SpRend;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        SpRend = GetComponentInChildren<SpriteRenderer>();
        cam = GetComponentInChildren<Camera>();
        notActive = Color.white;
        active = Color.blue;
        current = notActive;
    }

    // Update is called once per frame
    void Update()
    {
        SpRend.color = current;
        if (cam == null)
        {
            cam = GetComponentInChildren<Camera>();            
        }        
        else if (cam != null)
        {
            if (cam.isActiveAndEnabled)
            {
                current = active;
            }
            else
            {
                current = notActive;
            }
        }
    }
}
