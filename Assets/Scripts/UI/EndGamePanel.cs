using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    public Text ScoreText;
    public Text BathTime;
    public Text Missions;
    public Text BestScore;

    private int score;
    private string bathTime;
    private int missions;
    private int bestScore;

    private void Awake()
    {
        Events.Level.Finish += UpdateInfo;
    }
    private void OnDestroy(){

        Events.Level.Finish -= UpdateInfo;
    }

    private void UpdateInfo()
    {
        score = ScoreSystem.Instance.CurrentScore;
        bestScore = ScoreSystem.Instance.BestScore;
        missions = ScoreSystem.Instance.MissionsCompleted;
        var timeSpan = TimeSpan.FromSeconds(ScoreSystem.Instance.BathTime);
        bathTime = timeSpan.ToString("c");
        SetupUI();
    }

    private void SetupUI()
    {
        ScoreText.text = $"{score}";
        BestScore.text = $"{bestScore}";
        Missions.text = $"{missions}";
        BathTime.text = $"{bathTime}";
    }

}
