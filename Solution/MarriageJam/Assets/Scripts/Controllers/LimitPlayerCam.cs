using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitPlayerCam : MonoBehaviour 
{
	public GameObject limitA;
	public GameObject limitB;

	void Start ()
	{
		limitA.SetActive(false);
		limitB.SetActive(false);
	}

	public void LimitCamActive(bool active)
	{
		limitA.SetActive(active);
		limitB.SetActive(active);
	}
}
