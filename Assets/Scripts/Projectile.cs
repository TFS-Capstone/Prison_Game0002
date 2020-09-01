using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rigidB;
    public float shootForce = 20;
    public Vector3 vel;
    public Vector3 acceleration;
    public Vector3 initialDirection;
    [SerializeField]
   
    // Start is called before the first frame update
    void OnEnable()
    {
        acceleration.y = -9.8f;
        rigidB = GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.zero;
        vel += shootForce * initialDirection;
        
        ApplyForce();
    }

    // Update is called once per frame
    void Update()
    {
        vel += acceleration * Time.deltaTime;
        transform.position += vel * Time.deltaTime;
        SpinObjectInAir();

        if (transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }

    void ApplyForce()
    {
        //rigidB.AddRelativeForce(Vector3.forward * shootForce);

    }

    void SpinObjectInAir()
    {
        //spin arrow via rigidbody trigonometry
        //float _yVelocity = rigidB.velocity.y;
        //float _zVelocity = rigidB.velocity.z;
        //float _xVelocity = rigidB.velocity.x;
        //float _combinedVelocity = Mathf.Sqrt(_zVelocity * _zVelocity + _xVelocity * _xVelocity);

        //float _fallAngle = -1*Mathf.Atan2(_yVelocity, _combinedVelocity) * 180/Mathf.PI;
        //transform.eulerAngles = new Vector3(_fallAngle, transform.eulerAngles.y, transform.eulerAngles.z);

        //spin arrow via trig based on the vector trajectory
        float yVelocity = vel.y;
        float zVelocity = vel.z;
        float xVelocity = vel.x;
        float combinedVelocity = Mathf.Sqrt(zVelocity * zVelocity + xVelocity * xVelocity);
        float fallAngle = -1 * Mathf.Atan2(yVelocity, combinedVelocity) * 180 / Mathf.PI;
        transform.eulerAngles = new Vector3(fallAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // the collision needs to have a non-kinematic rigidbody for these to register.
        //do stuff
        Destroy(gameObject);
    }
}
