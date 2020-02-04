using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FovFixer : MonoBehaviour
{
    private Camera _camera;
    public float TargetRatio = 16.0f / 9.0f;
    public float OriginalHFov = 30.0f;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Start()
    {

    }

    void Update()
    {
        if(Application.isEditor && !Application.isPlaying)
        {
            UpdateCameraFov();
        }
    }
    // Update is called once per frame
    void UpdateCameraFov()
    {
        if(_camera == null)
        {
            _camera = GetComponent<Camera>();
        }

        if(_camera.aspect < TargetRatio)
        {
            float targetH = Camera.VerticalToHorizontalFieldOfView(OriginalHFov, 16.0f/9.0f);
            _camera.fieldOfView = Camera.HorizontalToVerticalFieldOfView(targetH, _camera.aspect);
        }
        else
        {
            _camera.fieldOfView = OriginalHFov;
        }
    }
}
