using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    bool isPaused;
    private PhotonView pv;
    public float idealRPM = 500f;
    public float maxRPM = 1000f;

    public Transform centerOfGravity;

    public WheelCollider FL_Wheel, FR_Wheel, RL_Wheel, RR_Wheel;

    public float turnRadius = 6f;
    public float torque = 25f;
    public float brakeTorque = 100f;

    public float antiRoll = 20000.0f;

    public enum DriveMode { Front, Rear, All };
    public DriveMode driveMode = DriveMode.Rear;

    public Rigidbody rigidbody;


    
    // Start is called before the first frame update
    void Start()
    {
        //pv = GetComponent<PhotonView>();
        //if (pv.IsMine)
        //{
        //    rigidbody.centerOfMass = centerOfGravity.localPosition;
        //    GetComponentInChildren<Camera>().enabled = true;
        //}
        //else
        //{
        //    GetComponentInChildren<Camera>().enabled = false;
        //}


        rigidbody.centerOfMass = centerOfGravity.localPosition;

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public float Speed()
    {
        return RR_Wheel.radius * Mathf.PI * RR_Wheel.rpm * 60f / 1000f;
    }

    public float Rpm()
    {
        return RL_Wheel.rpm;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float scaledTorque = Input.GetAxis("Vertical") * torque;

        if (RL_Wheel.rpm < idealRPM)
            scaledTorque = Mathf.Lerp(scaledTorque / 10f, scaledTorque, RL_Wheel.rpm / idealRPM);
        else
            scaledTorque = Mathf.Lerp(scaledTorque, 0, (RL_Wheel.rpm - idealRPM) / (maxRPM - idealRPM));

        doRollBar(FR_Wheel, FL_Wheel);
        doRollBar(RR_Wheel, RL_Wheel);

        FR_Wheel.steerAngle = Input.GetAxis("Horizontal") * turnRadius;
        FL_Wheel.steerAngle = Input.GetAxis("Horizontal") * turnRadius;

        FR_Wheel.motorTorque = driveMode == DriveMode.Rear ? 0 : scaledTorque;
        FL_Wheel.motorTorque = driveMode == DriveMode.Rear ? 0 : scaledTorque;
        RR_Wheel.motorTorque = driveMode == DriveMode.Front ? 0 : scaledTorque;
        RL_Wheel.motorTorque = driveMode == DriveMode.Front ? 0 : scaledTorque;

        if (Input.GetKey(KeyCode.Space))
        {
            FR_Wheel.brakeTorque = brakeTorque;
            FL_Wheel.brakeTorque = brakeTorque;
            RR_Wheel.brakeTorque = brakeTorque;
            RL_Wheel.brakeTorque = brakeTorque;
        }
        else
        {
            FR_Wheel.brakeTorque = 0;
            FL_Wheel.brakeTorque = 0;
            RR_Wheel.brakeTorque = 0;
            RL_Wheel.brakeTorque = 0;
        }
    }

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
