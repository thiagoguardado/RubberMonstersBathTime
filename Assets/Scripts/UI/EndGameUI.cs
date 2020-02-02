using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    public GameObject[] statePanels;
    private Animator animator;
    private SFXDispatcher sfxDispatcher;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        sfxDispatcher = GetComponent<SFXDispatcher>();

        Events.Level.Start += ClosePanel;
        Events.Level.Finish += OnLevelFinish;
        Events.Level.ShowTitle += ClosePanel;
    }

    private void OnDestroy()
    {
        Events.Level.Start -= ClosePanel;
        Events.Level.Finish -= OnLevelFinish;
        Events.Level.ShowTitle -= ClosePanel;
    }

    private void ClosePanel()
    {
        animator.SetBool("opened", false);
    }

    private void OnLevelFinish()
    {
        animator.SetBool("opened", true);
    }

    public void RestartGame()
    {
        sfxDispatcher.PlaySfxOnce();

        Events.Level.Start.Invoke();
    }

    public void ShowTitle()
    {
        sfxDispatcher.PlaySfxOnce();

        Events.Level.ShowTitle.Invoke();
    }

}
