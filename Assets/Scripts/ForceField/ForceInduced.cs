using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceInduced : MonoBehaviour
{
    public Vector3 initialVelocity;
    public float maxVelocity = 200f;
    private Rigidbody rb;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = initialVelocity;
    }

    private void Update()
    {
        // clamp velocity
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    public void AddForce(Vector3 force)
    {
        rb.AddForce(force,ForceMode.Force);
    }
}
