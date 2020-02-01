using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    private float angle;
    private bool clockwise;

    void Start()
    {
        angle = transform.rotation.eulerAngles.y;
    }

    void FixedUpdate()
    {
        angle += speed * Time.deltaTime * (clockwise ? 1 : -1);
        if (angle > 360) angle -= 360;
        else if (angle < -360) angle += 360;
        Vector3 rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, angle, rotation.z);
    }
}
