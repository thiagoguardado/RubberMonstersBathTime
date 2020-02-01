using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
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
        transform.localRotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpValue);
    }

    public void Update()
    {
        if(!enabled || TargetPosition == null)
        {
            return;
        }

        UpdatePosition(false);
    }
}
