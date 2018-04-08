﻿using UnityEngine;

[ExecuteInEditMode]
public class CameraFix : MonoBehaviour
{
    [Range(1, 4)]
    public int pixelScale = 1;

    private UnityEngine.Camera _camera;

    void Update()
    {
        if (_camera == null)
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _camera.orthographic = true;
        }
        _camera.orthographicSize = Screen.height * (0.005f / pixelScale);
    }
}