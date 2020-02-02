using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AudioUnit
{
    public string id;
    public AudioClip audioClip;
}

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioSource bgm;
    public AudioSource sfx;
    public List<AudioUnit> audiosList;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySfxOnce(string audioID)
    {
        foreach (var item in audiosList)
        {
            if (item.id == audioID)
            {
                sfx.PlayOneShot(item.audioClip);
            }
        }
    }

    internal void PlaySfxOnceOnCaller(string audioID, AudioSource audioSource)
    {
        foreach (var item in audiosList)
        {
            if (item.id == audioID)
            {
                audioSource.PlayOneShot(item.audioClip);
            }
        }
    }

    internal void PlaySfxLoopOnCaller(string audioID, AudioSource audioSource)
    {
        if (audioSource == null) return;
        
        foreach (var item in audiosList)
        {
            if (item.id == audioID)
            {
                audioSource.loop = true;
                audioSource.clip = item.audioClip;
                audioSource.Play();
            }
        }
    }
}
