using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSlotUI : MonoBehaviour
{
    private Mission mission;

    public void Setup(Mission mission)
    {
        gameObject.SetActive(true);
        this.mission = mission;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        mission = null;
    }
}
