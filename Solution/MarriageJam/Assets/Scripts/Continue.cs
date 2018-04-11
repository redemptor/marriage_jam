using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour {

	public Text gameOver;
	public Text continueText;

	public Image yesButton;

	public SpriteRenderer continueImage;
	public SpriteRenderer textBox;

	enum States{gameOverIn, gameOverOut, continueIn, continueOut, continueText, buttonShow, sceneChange, neutro}
	States _state = States.neutro;
	public string textContinue;
	// Use this for initialization
	void Start () {
		_state = States.gameOverIn;
	}
	
	// Update is called once per frame
	void Update () {
		switch(_state){
			case States.gameOverIn:
			StartCoroutine("FadeInText", gameOver);
			_state = States.neutro;
			break;
			case States.gameOverOut: 
			StartCoroutine("FadeOutText", gameOver);
			_state = States.neutro;
			break;
			case States.continueIn:
			StartCoroutine("FadeInImage", continueImage);
			_state = States.neutro;
			break;
			case States.continueText:
			StartCoroutine("TextEnter", continueText);
			_state = States.neutro;
			break;
			case States.continueOut:
			StartCoroutine("FadeOutImage", continueImage);
			_state = States.neutro;
			break;
			case States.sceneChange:
			StartCoroutine("SceneChange", continueImage);
			_state = States.neutro;
			break;
			case States.buttonShow:
			StartCoroutine("FadeInButton", yesButton);
			_state = States.neutro;
			break;
			case States.neutro:
			break;
		}
	}

	IEnumerator FadeInText(Text text){
		for(float f = 0f;f <=1f;f+=0.01f){
			Color c = text.color;
			c.a = f;
			text.color = c;

			yield return null;
		}
		_state = States.gameOverOut;
		Debug.Log("terminou FadeInText");
	}

	IEnumerator FadeOutText(Text text){
		for(float f = 1f;f >= 0f;f -= 0.01f){
			Color c = text.color;
			c.a = f;
			text.color = c;

			yield return null;
		}
		_state = States.continueIn;
		Debug.Log("terminou FadeOutText");
	}

	IEnumerator FadeInImage(SpriteRenderer image){
		for(float f = 0f;f <=1f;f+=0.01f){
			Color c = image.color;
			c.a = f;
			image.color = c;

			yield return null;
		}

		for(float f = 0f;f <= 1f;f+=0.01f){
			Color c = textBox.color;
			c.a = f;
			textBox.color = c;

			yield return null;
		}
		_state = States.continueText;
		Debug.Log("terminou FadeInImage");
	}

	IEnumerator FadeOutImage(SpriteRenderer image){
		for(float f = 0.5f; f >= 0f; f -= 0.01f){
			Color c = image.color;
			c.a = f;
			image.color = c;
			continueText.color = c;
			yesButton.color = c;
			textBox.color = c;

			yield return null;
		}
		_state = States.sceneChange;
		Debug.Log("terminou FadeOutImage");
	}

	IEnumerator SceneChange(){
        GameManager.difficulty = Difficulty.Normal;
        GameManager.NumPlayers = 2;

		SceneManager.LoadScene("Level 1");
		yield return null;
		Debug.Log("terminou SceneChange");
	}

	IEnumerator TextEnter(Text text){
		int characters = 0;
		text.text = "";

		while(characters <= textContinue.Length-1){
			text.text += textContinue[characters];
			characters+=1;
			yield return new WaitForSeconds(0.07f);
		}
		_state = States.buttonShow;
	}

	IEnumerator FadeInButton(Image text){
		for(float f = 0f;f <=1f;f+=0.01f){
			Color c = text.color;
			c.a = f;
			text.color = c;

			yield return null;
		}
		_state = States.neutro;
		Debug.Log("terminou FadeInButton");
	}

	public void buttonYesClick(){
		_state = States.continueOut;
	}
}
