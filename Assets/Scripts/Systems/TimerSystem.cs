using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    private float timer = 0f;
    private float timerTick = 0f;
    private bool isRunnning = false;
    private bool isPaused = false;

    private void Awake()
    {
        Events.Level.Start += StartTimer;
        Events.Level.Pause += () => PauseTimer(true);
        Events.Level.Unpause += () => PauseTimer(false);
        Events.Level.Finish += FinishTimer;
    }
    private void OnDestroy()
    {
        Events.Level.Start -= StartTimer;
        Events.Level.Pause -= () => PauseTimer(true);
        Events.Level.Unpause -= () => PauseTimer(false);
        Events.Level.Finish -= FinishTimer;
    }

    private void Update()
    {
        if (isRunnning && !isPaused)
        {
            timer += Time.deltaTime;
            timerTick = Time.deltaTime;

            TriggerTimers();
        }
    }

    public void StartTimer()
    {
        this.timer = 0f;
        this.timerTick = 0f;
        this.isRunnning = true;

        TriggerTimers();
    }

    public void PauseTimer(bool isPaused)
    {
        if (isRunnning)
        {
            // pause
            if (!this.isPaused && isPaused)
            {
                this.isPaused = isPaused;
            }

            // unpause
            if (this.isPaused && !isPaused)
            {
                this.isPaused = isPaused;
            }
        }
    }

    public void FinishTimer()
    {
        isRunnning = false;
    }

    private void TriggerTimers()
    {
        Events.Timer.TickOverall.SafeInvoke(timer);
        Events.Timer.Tick.SafeInvoke(timerTick);
    }
}
