using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class README : MonoBehaviour
{
    /*
     * how this project works
     * 
     * the objects called CameraObjs are place holder object 
     * inside they have a Camera object, this has the Cameras script (cam movement) and the CameraToggle script (hold the function to enable the Camera component)
     * 
     * Ideally I would like both the CameraObjs and Camera to move, but currently only the Camera moves.
     * 
     * the player has the player script for movement, hit the ESCAPE key to quit application
     * 
     * FindCameraRadius started out as my attempt to find cameras in a certain range (attached to player) but it was very buggy
     *      it would work, but eventually it would debug.log that there was no cameras in range
     *      
     * FindCameraRadius became the iteration through an array of the camera objects, assigned in inspector
     * this works fine for simple purposes
     *      Press 'C' to switch to next camera, player can still move.
     *      if no more cameras in the array then return to camera 0 which is attached to player.
     *      
     *  {    
     * I am and have been stuck on trying to stop the player's movement when camera 0 is not active
     * and exiting the array on any camera, rather than travelling through all the cameras.
     * A radius check that works consistently would also help
     *  }
     *  
     * plz help moi
     * if you have questions, message me on Discord, I actually have notifications turned on, maybe
     */
}