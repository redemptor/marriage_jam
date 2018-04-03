using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float smothTime;

    private Vector2 velocity;

    private void FixedUpdate()
    {
        var posX = Mathf.SmoothDamp(transform.position.x + 0.02f, target.position.x, ref velocity.x, smothTime);
        var posY = transform.position.y;//Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smothTime);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
