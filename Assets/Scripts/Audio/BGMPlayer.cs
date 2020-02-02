using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    private float targetVolume = 1f;
    private Coroutine coroutine;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = audioClip;
        audioSource.volume = 0f;
    }

    public void StartBGM(float lerpDuration, float volume)
    {
        audioSource.Play();

        SetVolume(lerpDuration, volume);
    }

    public void SetVolume(float lerpDuration, float volume)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(ChangeVolume(lerpDuration, volume));
    }

    public void Stop(float lerpDuration)
    {
        audioSource.Stop();

        SetVolume(lerpDuration, 0f);
    }

    private IEnumerator ChangeVolume(float duration, float targetVolume)
    {
        float timer = 0f;
        float initialVolume = audioSource.volume;
        while (timer <= duration)
        {
            audioSource.volume = Mathf.Lerp(initialVolume, targetVolume, timer / duration);

            timer += Time.deltaTime;
            if (timer > duration) timer = duration;

            yield return null;
        }
    }
}
