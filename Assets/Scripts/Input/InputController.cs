using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public delegate void ScreenInteract(Vector2 screenPosition);
    public event ScreenInteract StartClick;
    public event ScreenInteract StopClick;
    public event ScreenInteract MoveClick;

    private void Update()
    {
        GetTouch();
        GetMouse();
    }

    private void GetMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (StartClick != null) StartClick(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            if (MoveClick != null) MoveClick(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (StopClick != null) StopClick(Input.mousePosition);
        }
    }

    private void GetTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (StartClick != null) StartClick(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (StopClick != null) StopClick(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (MoveClick != null) MoveClick(touch.position);
            }
        }
    }
}
