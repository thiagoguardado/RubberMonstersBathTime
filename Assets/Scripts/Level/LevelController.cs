using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public float levelDuration;

    private TimerSystem timerSystem;

    private void Awake()
    {
        timerSystem = FindObjectOfType<TimerSystem>();

        Events.Timer.TimerFinished += HandleFinishTimer;
    }

    private void OnDestroy()
    {
        Events.Timer.TimerFinished += HandleFinishTimer;
    }

    private void HandleFinishTimer()
    {
        Events.Level.LevelFinished.SafeInvoke();

        Debug.Log("Level Finished");
    }

    void Start()
    {
        timerSystem.StartTimer(levelDuration);

        Events.Level.LevelStarted.SafeInvoke();
        
        Debug.Log("Level Started");
    }


}
