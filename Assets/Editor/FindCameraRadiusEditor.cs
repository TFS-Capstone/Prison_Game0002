using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(FindCameraRadius))]
public class FindCameraRadiusEditor : Editor
{
    private void OnSceneGUI()
    {
        FindCameraRadius fcr = (FindCameraRadius)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fcr.transform.position, Vector3.up, Vector3.forward, 360, fcr.findRadius);

        Handles.color = Color.blue;
        foreach (Camera inRangeCam in fcr.inRangeCams)
        {
            //Handles.DrawLine(fcr.transform.position, inRangeCam.transform.position);
        }
    }
}
