using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public Transform TargetPosition;

    public void Start()
    {
        transform.parent = null;
    }
    public void Update()
    {
        if(!enabled || TargetPosition == null)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, TargetPosition.position, 20 * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.rotation, TargetPosition.rotation, 20 * Time.deltaTime);
    }
}
