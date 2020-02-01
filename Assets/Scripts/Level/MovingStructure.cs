using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStructure : MonoBehaviour
{
    public float startY;
    public float finishY;
    public float lerpfactor;
    private Vector3 targetPosition;

    void Awake()
    {
        Events.Timer.TimerStarted += SetStartAsGoal;
        Events.Timer.TimerTick += SetInterpolatedAsGoal;
    }

    void OnDestroy()
    {
        Events.Timer.TimerStarted -= SetStartAsGoal;
        Events.Timer.TimerTick -= SetInterpolatedAsGoal;
    }

    void Start()
    {
        targetPosition = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpfactor * Time.deltaTime);
    }

    private void SetStartAsGoal()
    {
        targetPosition = new Vector3(targetPosition.x, startY, targetPosition.z);
    }

    private void SetInterpolatedAsGoal(float ratio)
    {
        targetPosition = new Vector3(targetPosition.x, Mathf.Lerp(startY, finishY, 1f - ratio), targetPosition.z);
    }
}
