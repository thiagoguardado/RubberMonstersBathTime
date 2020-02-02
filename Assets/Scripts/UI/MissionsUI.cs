using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsUI : MonoBehaviour
{
    private Animator animator;
    private MissionsController missionsController;
    public List<MissionSlotUI> slots;
    public ToyBodyConfiguration toyBodyConfiguration;

    private void Awake()
    {
        missionsController = FindObjectOfType<MissionsController>();
        animator = GetComponent<Animator>();

        Events.Missions.NewMission += AddMission;
        Events.Missions.FulfillMission += EliminateMission;

        Events.Level.Start += OpenPanel;
        Events.Level.Finish += ClosePanel;

        Events.Drain.Start += ShakePanel;
        Events.Drain.Stop += StopShake;
    }

    private void OnDestroy()
    {
        Events.Missions.NewMission -= AddMission;
        Events.Missions.FulfillMission -= EliminateMission;

        Events.Level.Start -= OpenPanel;
        Events.Level.Finish -= ClosePanel;
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
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].Mission != null)
            {
                if (mission.Id == slots[i].Mission.Id)
                {
                    slots[i].Fullfill();
                }
            }
        }

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
            if (bodyPart.id == id)
            {
                switch (part)
                {
                    case EBodyPartSlot.LEFT:
                        return bodyPart.LeftUiImage;
                    case EBodyPartSlot.RIGHT:
                        return bodyPart.RightUiImage;
                }
            }
        }
        return null;
    }


    private void ClosePanel()
    {
        animator.SetBool("inGame", false);
    }

    private void OpenPanel()
    {
        RefreshSlots();
        animator.SetBool("inGame", true);
        animator.SetBool("inDanger", false);
    }

    private void StopShake()
    {
        animator.SetBool("inDanger", false);
    }

    private void ShakePanel()
    {
        animator.SetBool("inDanger", true);
    }

}
