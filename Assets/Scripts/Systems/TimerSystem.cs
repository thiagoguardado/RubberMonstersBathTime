using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    private float timer = 0f;
    private float duration = 0f;
    private bool isRunnning = false;
    private bool isPaused = false;

    private void Update()
    {
        if (isRunnning && !isPaused)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, duration);

            TickTimer();

            if (timer >= duration)
            {
                Finish();
            }
        }
    }

    public void StartTimer(float duration)
    {
        this.timer = 0f;
        this.duration = duration;
        this.isRunnning = true;

        Events.Timer.TimerStarted.SafeInvoke();

        TickTimer();
    }

    public void PauseTimer(bool isPaused)
    {
        if (isRunnning)
        {
            // pause
            if (!this.isPaused && isPaused)
            {
                this.isPaused = isPaused;
                Events.Timer.TimerPaused();
            }

            // unpause
            if (this.isPaused && !isPaused)
            {
                this.isPaused = isPaused;
                Events.Timer.TimerUnpaused();
            }
        }
    }

    public void Finish()
    {
        isRunnning = false;

        Events.Timer.TimerFinished.SafeInvoke();
    }

    private void TickTimer()
    {
        Events.Timer.TimerTick.SafeInvoke(1 - timer / duration);
    }
}
