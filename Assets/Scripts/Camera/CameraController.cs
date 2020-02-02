using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Events.Level.Start += MoveToPlay;
        Events.Level.Finish += MoveToMenu;
    }

    private void OnDestroy()
    {
        Events.Level.Start -= MoveToPlay;
        Events.Level.Finish -= MoveToMenu;
    }

    private void MoveToPlay()
    {
        animator.SetBool("inGame", true);
    }

    private void MoveToMenu()
    {
        animator.SetBool("inGame", false);
    }


}
