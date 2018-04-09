using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] layers;
    public float[] layersVelocity;
    public float smooth;

    private Transform _camera;
    private Vector3 _previewCameraPosition;

    void Start()
    {
        _camera = Camera.main.transform;
        _previewCameraPosition = _camera.position;
    }

    void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            var parallax = (_previewCameraPosition.x - _camera.position.x) * layersVelocity[i];
            var targetX = layers[i].position.x - parallax;

            var newPosition = new Vector3(targetX, layers[i].position.y, layers[i].position.z);

            layers[i].position = Vector3.Lerp(layers[i].position, newPosition, smooth * Time.deltaTime);
        }

        _previewCameraPosition = _camera.position;
    }
}
