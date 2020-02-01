using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public int OriginalSlot;
    public Transform TargetPosition;
    public int TargetSlot;

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
        Quaternion targetRotation = TargetPosition.rotation;
        var scale = transform.localScale;
        if(OriginalSlot != TargetSlot)
        {
            targetRotation *= Quaternion.Euler(0, 0, 180);
            scale.y = -Mathf.Abs(scale.y);
        }
        else
        {
            scale.y = Mathf.Abs(scale.y);
        }
        transform.localScale = scale;
        transform.localRotation = Quaternion.Lerp(transform.rotation, targetRotation, 20 * Time.deltaTime);
    }
}
