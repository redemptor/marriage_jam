using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour 
{
	[Header("Settings Shake")]
	[Range(0f,1f)]
	public float power = 0.045f;
	[Range(0f, 2f)]
	public float durantion = 1.0f;
	[Range(1f,5f)]
	public float slowDownAmount = 1.0f;
	public bool shouldShake;
	private Transform _mainCamera;
	private Vector3 _startPosition;
	private float _initialDuration;
	private Camera _cam;

	void Start ()
	{
		_cam = GetComponent<Camera>();
		_mainCamera = _cam.transform;
		_startPosition = _mainCamera.localPosition;
		_initialDuration = durantion;
	}
	
	void Update () 
	{
		_startPosition = _mainCamera.localPosition;

		if(shouldShake)
		{
			if(durantion > 0)
			{
				Handheld.Vibrate();
				_mainCamera.localPosition = _startPosition + Random.insideUnitSphere * power;
				durantion -= Time.deltaTime * slowDownAmount;
			}
			else
			{
				shouldShake = false;
				durantion = _initialDuration;
				_mainCamera.localPosition = _startPosition;
			}
		}
	}
}
