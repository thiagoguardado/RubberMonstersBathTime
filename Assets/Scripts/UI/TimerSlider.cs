using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        Events.Timer.TimerStarted += Reset;
        Events.Timer.TimerTick += Refresh;
    }

    private void OnDestroy()
    {
        Events.Timer.TimerStarted -= Reset;
        Events.Timer.TimerTick -= Refresh;
    }

    private void Reset()
    {
        Refresh(1f);
    }

    private void Refresh(float value)
    {
        slider.value = value;
    }
}
