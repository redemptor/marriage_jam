using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour 
{
	[Range(0f,1f)]
	public float slowMotionValue = 0.01f;
	[Range(0f,3f)]
	public float slowMotionLenght = 2f; 
	
	void Update ()
	{
		SlowMotionCalculate();

		//Test temp slow motion
		if(Input.GetKeyDown(KeyCode.T))
		{
			SlowMotionActive();
		}
	}

	private void SlowMotionCalculate()
	{
		Time.timeScale += (1f / slowMotionLenght) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
	}

	public void SlowMotionActive()
	{
		Time.timeScale = slowMotionValue;
		Time.fixedDeltaTime = Time.timeScale * 0.03f;	
	}
}
