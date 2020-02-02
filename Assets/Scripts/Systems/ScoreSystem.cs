using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }
    public int CurrentScore;
    public int BestScore;
    public int MissionsCompleted;
    public int BathTime;

    private bool inGame = false;

    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        Events.Level.Start += OnLevelStart;
        Events.Level.Finish += OnLevelFinished;
        Events.Missions.FulfillMission += OnMissionCompleted;
        Events.Timer.TickOverall += OnTickOverall;
    }

    private void OnTickOverall(float tickOverall)
    {
        BathTime = Mathf.FloorToInt(tickOverall);
    }

    private void OnMissionCompleted(Mission obj)
    {
        if(!inGame)
        {
            return;
        }

        Debug.Log($"mission completed {obj.Value}");
        MissionsCompleted++;
        CurrentScore += obj.Value;
        if(CurrentScore > BestScore)
        {
            BestScore = CurrentScore;
        }
    }

    private void OnLevelFinished()
    {
        inGame = false;
    }

    private void OnLevelStart()
    {
        inGame = true;
        Reset();
    }

    private void Reset()
    {
        CurrentScore = 0;
        MissionsCompleted = 0;
        BathTime = 0;
    }
}
