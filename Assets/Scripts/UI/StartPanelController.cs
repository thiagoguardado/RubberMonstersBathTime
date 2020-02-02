using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelController : MonoBehaviour
{
    private Animator animator;
    private SFXDispatcher sFXDispatcher;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sFXDispatcher = GetComponent<SFXDispatcher>();

        Events.Level.ShowTitle += OpenPanel;
    }

    void OnDestroy()
    {
        Events.Level.ShowTitle -= OpenPanel;
    }

    private void OpenPanel()
    {
        animator.SetBool("opened", true);
    }

    public void StartGame()
    {
        animator.SetBool("opened", false);

        sFXDispatcher.PlaySfxOnce();

        // start game
        Events.Level.Start.SafeInvoke();
    }

    public void Quit()
    {
        Application.Quit();

        sFXDispatcher.PlaySfxOnce();
    }
}
