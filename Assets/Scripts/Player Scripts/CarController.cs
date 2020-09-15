using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //Rpm numbers for speed
    public float idealRPM = 500f;
    public float maxRPM = 1000f;
    //object's center of gravity
    public Transform centerOfGravity;
    //all of the wheels
    public WheelCollider FL_Wheel, FR_Wheel, RL_Wheel, RR_Wheel;
    //how quick the vehicle turns
    public float turnRadius = 6f;
    //'whatever torque actually does in real life'
    public float torque = 25f;
    //the force of breaking
    public float brakeTorque = 100f;
    //a number to keep the car from rolling
    public float antiRoll = 20000.0f;
    //how the vehicle drives
    public enum DriveMode { Front, Rear, All };
   // setting the drive to rear wheel
    public DriveMode driveMode = DriveMode.Rear;
    //used for center of gravity and anti-rolling
    public Rigidbody rigidbody;

    
    void Start()
    {
        //sets the center of mass to the correct place
        rigidbody.centerOfMass = centerOfGravity.localPosition;
    }

    public float Speed()
    {
        //sets the speed of the vehicle
        return RR_Wheel.radius * Mathf.PI * RR_Wheel.rpm * 60f / 1000f;
    }

    public float Rpm()
    {
        //sets the RPM of the vehicle
        return RL_Wheel.rpm;
    }

    void FixedUpdate()
    {
        //actually drives the vehicle
        float scaledTorque = Input.GetAxis("Vertical") * torque;
        //some math stuff for RP<
        if (RL_Wheel.rpm < idealRPM)
            scaledTorque = Mathf.Lerp(scaledTorque / 10f, scaledTorque, RL_Wheel.rpm / idealRPM);
        else
            scaledTorque = Mathf.Lerp(scaledTorque, 0, (RL_Wheel.rpm - idealRPM) / (maxRPM - idealRPM));
        //checks to not flip the vehicle
        doRollBar(FR_Wheel, FL_Wheel);
        doRollBar(RR_Wheel, RL_Wheel);
        //turns the vehicle
        FR_Wheel.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
        FL_Wheel.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
        //some stuff for the drive mode
        FR_Wheel.motorTorque = driveMode == DriveMode.Rear ? 0 : scaledTorque;
        FL_Wheel.motorTorque = driveMode == DriveMode.Rear ? 0 : scaledTorque;
        RR_Wheel.motorTorque = driveMode == DriveMode.Front ? 0 : scaledTorque;
        RL_Wheel.motorTorque = driveMode == DriveMode.Front ? 0 : scaledTorque;
        
        if (Input.GetKey(KeyCode.Space)) //braking
        {
            FR_Wheel.brakeTorque = brakeTorque;
            FL_Wheel.brakeTorque = brakeTorque;
            RR_Wheel.brakeTorque = brakeTorque;
            RL_Wheel.brakeTorque = brakeTorque;
        }
        else //slowing down
        {
            FR_Wheel.brakeTorque = 0;
            FL_Wheel.brakeTorque = 0;
            RR_Wheel.brakeTorque = 0;
            RL_Wheel.brakeTorque = 0;
        }
    }
    //Roll Bar is used to stop the car from flipping
    void doRollBar(WheelCollider wheelL, WheelCollider wheelR)
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedL = wheelL.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;

        bool groundedR = wheelR.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;

        float antiRollForce = (travelL - travelR) * antiRoll;

        if (groundedL)
            rigidbody.AddForceAtPosition(wheelL.transform.up * antiRollForce, wheelL.transform.position);

        if (groundedR)
            rigidbody.AddForceAtPosition(wheelR.transform.up * antiRollForce, wheelR.transform.position);
    }
}
