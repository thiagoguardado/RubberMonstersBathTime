using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainingSFX : MonoBehaviour
{
    public SFXDispatcher drainingDispatcher;
    public SFXDispatcher finishedDrainingDispatcher;

    private void Awake()
    {
        Events.Drain.Start += drainingDispatcher.StartPlaySFX;
        Events.Drain.Stop += drainingDispatcher.StopPlaySFX;

        Events.Drain.Finish += finishedDrainingDispatcher.PlaySfxOnce;
    }

    private void OnDestroy()
    {
        Events.Drain.Start -= drainingDispatcher.StartPlaySFX;
        Events.Drain.Stop -= drainingDispatcher.StopPlaySFX;

        Events.Drain.Finish -= finishedDrainingDispatcher.PlaySfxOnce;
    }
}
