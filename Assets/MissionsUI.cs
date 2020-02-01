using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsUI : MonoBehaviour
{
    private MissionsController missionsController;
    public List<MissionSlotUI> slots;

    private void Awake()
    {
        missionsController = FindObjectOfType<MissionsController>();

        Events.Missions.NewMission += AddMission;
        Events.Missions.FulfillMission += EliminateMission;
    }

    private void Start()
    {
        RefreshSlots();
    }

    private void AddMission(Mission mission)
    {
        RefreshSlots();
    }

    private void EliminateMission(Mission mission)
    {
        RefreshSlots();
    }

    private void RefreshSlots()
    {
        int i = 0;
        for (i = 0; i < missionsController.ActiveMissions.Count; i++)
        {
            slots[i].Setup(missionsController.ActiveMissions[i]);
        }

        for (int j = i; j < slots.Count; j++)
        {
            slots[j].Hide();
        }
    }
}
