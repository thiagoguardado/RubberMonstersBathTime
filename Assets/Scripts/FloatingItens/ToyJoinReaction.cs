using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyJoinReaction : MonoBehaviour
{
    private ToyBody toyBody;
    public GlobalConfigs globalConfigs;
    private float timer = 0f;
    private bool isJoined = false;

    public event Action<float> JoinedTimeSpent;
    public event Action Stopped;
    public event Action CompletedJoinedReaction;

    void Awake()
    {
        toyBody = GetComponent<ToyBody>();

        toyBody.Joined += StartCount;
        toyBody.Splitted += FinishCount;
    }

    void OnDestroy()
    {
        toyBody.Joined -= StartCount;
        toyBody.Splitted -= FinishCount;
    }

    private void FinishCount()
    {
        isJoined = false;
        Stopped.SafeInvoke();
    }

    private void StartCount()
    {
        timer = 0f;
        isJoined = true;
    }

    private void Update()
    {
        if (isJoined)
        {
            timer += Time.deltaTime;

            timer = Mathf.Clamp(timer, 0, globalConfigs.toyTimeAfterJoinBeforeScore);

            JoinedTimeSpent.SafeInvoke(timer / globalConfigs.toyTimeAfterJoinBeforeScore);

            if (timer >= globalConfigs.toyTimeAfterJoinBeforeScore)
            {
                CompletedJoinedReaction.SafeInvoke();
            }
        }
    }

}
