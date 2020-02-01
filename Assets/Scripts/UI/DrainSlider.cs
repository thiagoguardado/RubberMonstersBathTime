using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrainSlider : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        Events.Drain.Tick += Refresh;
    }

    private void OnDestroy()
    {
        Events.Drain.Tick -= Refresh;
    }

    private void Start()
    {
        Reset();
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
