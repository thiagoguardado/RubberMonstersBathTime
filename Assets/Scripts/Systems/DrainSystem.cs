using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainSystem : MonoBehaviour
{
    public float startingSeconds = 30f;
    private float drainLevel { get { return Mathf.Clamp01(1 - timer / startingSeconds); } }
    private bool isDraining = false;
    private bool isPaused = false;
    private float timer = 0f;

    private void Awake()
    {
        Events.Level.Start += UnpauseDrain;
        Events.Level.Pause += PauseDrain;
        Events.Level.Unpause += UnpauseDrain;

        Events.Missions.MaxMissionsReached += StartDrain;
        Events.Missions.MaxMissionsCleared += StopDrain;
    }

    private void OnDestroy()
    {
        Events.Level.Start -= UnpauseDrain;
        Events.Level.Pause -= PauseDrain;
        Events.Level.Unpause -= UnpauseDrain;

        Events.Missions.MaxMissionsReached -= StartDrain;
        Events.Missions.MaxMissionsCleared -= StopDrain;
    }

    private void Start()
    {
        Reset();
    }

    private void StartDrain()
    {
        Events.Drain.Start.SafeInvoke();
        isDraining = true;
    }

    private void StopDrain()
    {
        Events.Drain.Stop.SafeInvoke();
        isDraining = false;
    }

    private void Reset()
    {
        timer = 0f;

        isPaused = false;
        isDraining = false;

        Events.Drain.Tick.SafeInvoke(drainLevel);
    }

    private void PauseDrain()
    {
        isPaused = true;
    }

    private void UnpauseDrain()
    {
        isPaused = false;
    }

    private void Update()
    {
        if (!isPaused && isDraining)
        {
            timer += Time.deltaTime;

            Events.Drain.Tick.SafeInvoke(drainLevel);

            if (drainLevel <= 0) FinishDrain();
        }

        
    }

    private void FinishDrain()
    {
        StopDrain();
        Events.Drain.Finish.SafeInvoke();
    }
}
