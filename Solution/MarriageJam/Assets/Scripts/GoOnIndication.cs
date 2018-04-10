using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOnIndication : MonoBehaviour
{
	public GameObject goImage;
	[Range(2,10)]
	public int blinkCycle = 4;
	[Range(0.1f,0.5f)]
	public float blinkDelay = 0.3f;
	private bool activeSignaling = true;

	private void Start()
	{
		goImage.SetActive(false);	
	}
	
	public void TurnOnSignaling()
	{
		if(goImage != null && activeSignaling)
		{
			StartCoroutine(BlinkImage(blinkDelay));
			activeSignaling = false;
		}
	}

	IEnumerator BlinkImage(float delay)
	{
		for (int i = 0; i < blinkCycle; i++)
		{
            yield return new WaitForSeconds(delay);
			goImage.SetActive(true);
			yield return new WaitForSeconds(delay);
			goImage.SetActive(false);
		}
		goImage.SetActive(false);
		activeSignaling = true;
	}
}
