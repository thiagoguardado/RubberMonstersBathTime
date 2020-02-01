using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceInduced : MonoBehaviour
{
    public Vector3 initialVelocity;
    public float maxVelocity = 200f;
    private Rigidbody rigidbody;

    private void Awake(){
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigidbody.velocity = initialVelocity;
    }

    private void Update()
    {
        // clamp velocity
        if (rigidbody.velocity.magnitude > maxVelocity)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
        }
    }

    public void AddForce(Vector3 force)
    {
        rigidbody.AddForce(force,ForceMode.Force);
    }
}
