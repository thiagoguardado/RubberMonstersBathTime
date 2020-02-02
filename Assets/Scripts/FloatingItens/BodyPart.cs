using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public string Id;
    public int OriginalSlot;
    public Transform TargetPosition;
    public int TargetSlot;
    public GameObject ModelContainer;

    public void Start()
    {
        transform.parent = ToyBodyFactory.Instance.BodyPartParent;
    }

    public void UpdateImmediately()
    {
        UpdatePosition(true);
    }
    public void UpdatePosition(bool immediately)
    {
        float lerpValue = immediately ? 1 : 7 * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, TargetPosition.position, lerpValue);
        if(!immediately && Vector3.Distance(transform.position, TargetPosition.position) < 0.004f)
        {
            transform.position = TargetPosition.position;
        }

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
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpValue);
        float angleDistance = Vector3.Distance(transform.rotation.eulerAngles, targetRotation.eulerAngles);

        if(angleDistance < 6)
        {
            transform.rotation = targetRotation;
        }
    }

    public void LateUpdate()
    {
        if(!enabled || TargetPosition == null)
        {
            return;
        }

        UpdatePosition(false);
    }

    public void DestroyThis()
    {
        ToyBodyFactory.Instance.BodyParts.Remove(this);
        Destroy(gameObject);
    }
}
