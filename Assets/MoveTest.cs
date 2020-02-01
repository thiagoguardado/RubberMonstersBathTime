using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    Rigidbody rigidbody;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(Vector3.right * speed * Input.GetAxis("Horizontal"));
        rigidbody.AddForce(Vector3.forward * speed * Input.GetAxis("Vertical"));
    }
}
