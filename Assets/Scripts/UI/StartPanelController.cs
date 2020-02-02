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
    }

    public void StartGame()
    {
        animator.SetTrigger("start");

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
