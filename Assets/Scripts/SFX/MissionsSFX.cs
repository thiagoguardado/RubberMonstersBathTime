using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsSFX : MonoBehaviour
{
    public SFXDispatcher newCardDispatcher;
    public SFXDispatcher cardCompletedDispatcher;

    void Start()
    {
        Events.Missions.NewMission += PlayNewCard;
        Events.Missions.FulfillMission += PlayCardCompleted;
    }

    void OnDestroy()
    {
        Events.Missions.NewMission -= PlayNewCard;
        Events.Missions.FulfillMission -= PlayCardCompleted;
    }

    private void PlayNewCard(Mission m)
    {
        newCardDispatcher.PlaySfxOnce();
    }

    private void PlayCardCompleted(Mission m)
    {
        cardCompletedDispatcher.PlaySfxOnce();
    }

}
