using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySFX : MonoBehaviour
{
    public SFXDispatcher joinDispatcher;
    public SFXDispatcher breakDispatcher;
    private ToyBody toyBody;

    private void Awake()
    {
        toyBody = GetComponentInParent<ToyBody>();

        toyBody.Joined += joinDispatcher.PlaySfxOnce;
        toyBody.Splitted += breakDispatcher.PlaySfxOnce;
    }

    private void OnDestroy()
    {
        toyBody.Joined -= joinDispatcher.PlaySfxOnce;
        toyBody.Splitted -= breakDispatcher.PlaySfxOnce;
    }
}
