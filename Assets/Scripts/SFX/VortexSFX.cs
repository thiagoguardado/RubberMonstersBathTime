using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexSFX : MonoBehaviour
{
    private SFXDispatcher sFXDispatcher;

    private void Awake()
    {
        sFXDispatcher = GetComponent<SFXDispatcher>();

        Events.Vortex.Start += sFXDispatcher.StartPlaySFX;
        Events.Vortex.Stop += sFXDispatcher.StopPlaySFX;
    }

    private void OnDestroy()
    {
        Events.Vortex.Start -= sFXDispatcher.StartPlaySFX;
        Events.Vortex.Stop -= sFXDispatcher.StopPlaySFX;
    }
}
