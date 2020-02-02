using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    public GameObject[] statePanels;

    private void Start()
    {
        Reset();
        Events.Level.Finish += OnLevelFinish;
    }

    private void OnDestroy()
    {
        Events.Level.Finish -= OnLevelFinish;
    }

    private void OnLevelFinish()
    {
        SetPanelsActive(true);
    }

    private void SetPanelsActive(bool active)
    {
        foreach(GameObject panel in statePanels)
        {
            panel.SetActive(active);
        }
    }

    public void Reset()
    {
        SetPanelsActive(false);
    }
}
