﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    [Range(0f, 2f)]
    public float smothTime = 0.5f;
    [Header("Camera Limit")]
    public Vector3 minCameraPos = new Vector3 (-7.95f, 0f, -10f);
    public Vector3 maxCameraPos = new Vector3 (14.35f, 0f, -10f);
    [HideInInspector]
    public bool camFollow = true;

    private Vector2 velocity;

    private void FixedUpdate()
    {
        CameraFollow(camFollow);
        LimitCamera();
    }

    private void CameraFollow(bool active)
    {
        if(active)
        {
            var posX = Mathf.SmoothDamp(transform.position.x + 0.02f, target.position.x, ref velocity.x, smothTime);
            var posY = transform.position.y;
            //Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smothTime);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }

    private void LimitCamera()
    {
        transform.position = new Vector3 
        (
            Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z)
        );
    }
}
