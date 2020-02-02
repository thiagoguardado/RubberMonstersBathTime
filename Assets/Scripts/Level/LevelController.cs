using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private void Start()
    {
        // StartLevel();
    }

    private void Awake()
    {
        Events.Drain.Finish += FinishLevel;
    }

    private void OnDestroy()
    {
        Events.Drain.Finish -= FinishLevel;
    }

    void StartLevel()
    {
        Events.Level.Start.SafeInvoke();

        Debug.Log("Level Started");
    }

    void FinishLevel()
    {
        Events.Level.Finish.SafeInvoke();

        Debug.Log("Level Finished");
    }


    public void Pause(bool isPaused)
    {
        if (isPaused)
        {
            Events.Level.Pause.SafeInvoke();
        }
        else
        {
            Events.Level.Unpause.SafeInvoke();
        }
    }
}
