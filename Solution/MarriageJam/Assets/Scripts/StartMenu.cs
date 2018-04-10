using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	Renderer r;
	// Use this for initialization
	void Start () {
		r = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey){
			StartCoroutine("fadeOut");
		}
	}

	IEnumerator fadeOut(){
		for(float f = 1f; f >= 0f; f -= 0.01f){
			Color c = r.material.color;
			c.a = f;
			r.material.color = c;

			yield return null;
		}
		sceneChange();
	}

	void sceneChange(){
		SceneManager.LoadScene("Level 1");
	}
}
