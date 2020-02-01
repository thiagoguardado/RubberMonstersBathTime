using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Axis { X, Y, Z }

public class FloatingBehaviour : MonoBehaviour
{

    public Axis axis;
    public float turnRate = 1f;
    public float maxAngle = 30f;
    public float lerpFactor = 0.5f;
    
    private float angle;
    private float timer = 0f;
    private Quaternion targetRotation;


    private void Update()
    {
        UpdateAxisAngle();

    }

    private void UpdateAxisAngle()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        angle = maxAngle * Mathf.Sin(Time.time * turnRate);

        switch (axis)
        {
            case Axis.X:
                euler.x = angle;
                break;
            case Axis.Y:
                euler.y = angle;
                break;
            case Axis.Z:
                euler.z = angle;
                break;
            default:
                break;
        }

        targetRotation = Quaternion.Euler(euler);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, lerpFactor);
    }
}
