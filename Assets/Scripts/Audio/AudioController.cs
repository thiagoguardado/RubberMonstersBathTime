using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AudioUnit
{
    public string id;
    public List<AudioClip> audioClips;
}

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public List<AudioUnit> audiosList;
    public AudioSource sfxPlayer;
    public BGMPlayer baseBGM;
    public BGMPlayer gameplayBGM;
    public float bgmLerpDuration = 0.5f;
    public float bgmBackgroundVolume = 0.2f;

    private void Awake()
    {
        Instance = this;

        Events.Level.Start += StartGamePlayBGM;
        Events.Level.Finish += StopGamePlayBGM;
        Events.Drain.Start += ReduceGameplayBGMVolume;
        Events.Drain.Stop += RestoreGameplayBGMVolume;
        Events.Drain.Finish += StopGamePlayBGM;
    }

    private void OnDestroy()
    {
        Events.Level.Start -= StartGamePlayBGM;
        Events.Level.Finish -= StopGamePlayBGM;
        Events.Drain.Start -= ReduceGameplayBGMVolume;
        Events.Drain.Stop -= RestoreGameplayBGMVolume;
        Events.Drain.Finish -= StopGamePlayBGM;
    }

    private void Start()
    {
        StartBaseBGM();
    }

    public void PlaySfxOnce(string audioID)
    {
        foreach (var item in audiosList)
        {
            if (item.id == audioID)
            {
                sfxPlayer.PlayOneShot(GetAudioClipFromList(item));
            }
        }
    }

    internal void PlaySfxOnceOnCaller(string audioID, AudioSource audioSource)
    {
        foreach (var item in audiosList)
        {
            if (item.id == audioID)
            {
                audioSource.PlayOneShot(GetAudioClipFromList(item));
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
                audioSource.clip = GetAudioClipFromList(item);
                audioSource.Play();
            }
        }
    }

    private AudioClip GetAudioClipFromList(AudioUnit unit)
    {
        return unit.audioClips[UnityEngine.Random.Range(0, unit.audioClips.Count)];
    }

    private void StartBaseBGM()
    {
        baseBGM.StartBGM(bgmLerpDuration, 1f);
    }
    private void StartGamePlayBGM()
    {
        baseBGM.SetVolume(bgmLerpDuration, bgmBackgroundVolume);
        gameplayBGM.StartBGM(bgmLerpDuration, 1f);
    }
    private void StopGamePlayBGM()
    {
        baseBGM.SetVolume(bgmLerpDuration, 1f);
        gameplayBGM.Stop(bgmLerpDuration);
    }
    private void ReduceGameplayBGMVolume()
    {
        gameplayBGM.SetVolume(bgmLerpDuration, bgmBackgroundVolume);
    }
    private void RestoreGameplayBGMVolume()
    {
        gameplayBGM.SetVolume(bgmLerpDuration, 1f);
    }
}
