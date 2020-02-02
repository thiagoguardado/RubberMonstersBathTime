using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsUI : MonoBehaviour
{
    private MissionsController missionsController;
    public List<MissionSlotUI> slots;
    public ToyBodyConfiguration toyBodyConfiguration;

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
        int missionCount = missionsController.ActiveMissions.Count;
        int j = 0;
        for (int i = missionCount; i > 0; i--)
        {
            Mission mission = missionsController.ActiveMissions[i - 1];
            slots[j].Setup(mission, FindPart(mission.leftPartID, EBodyPartSlot.LEFT), FindPart(mission.rightPartID, EBodyPartSlot.RIGHT));
            j++;
        }

        for (int k = j; k < slots.Count; k++)
        {
            slots[k].Hide();
        }
    }

    private Sprite FindPart(string id, EBodyPartSlot part)
    {
        foreach (var bodyPart in toyBodyConfiguration.BodyPartConfigurations)
        {
            if (bodyPart.ToyId == id)
            {
                if (bodyPart.TargetSlot == part)
                {
                    return bodyPart.uiImage;
                }
            }
        }
        return null;
    }

}
