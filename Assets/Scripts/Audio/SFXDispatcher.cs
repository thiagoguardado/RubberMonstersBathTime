using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXDispatcher : MonoBehaviour
{
    public string audioID;
    public bool privateAudioSource = false;
    private AudioSource audioSource;

    public void Start()
    {
        if (privateAudioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySfxOnce()
    {
        if (privateAudioSource)
        {
            AudioController.Instance.PlaySfxOnceOnCaller(audioID, audioSource);
        }
        else
        {
            AudioController.Instance.PlaySfxOnce(audioID);
        }
    }

    public void StartPlaySFX()
    {
        AudioController.Instance.PlaySfxLoopOnCaller(audioID, audioSource);
    }

    public void StopPlaySFX()
    {
        if (audioSource != null) audioSource.Stop();
    }
}
