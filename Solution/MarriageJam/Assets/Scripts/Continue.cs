using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{

    public Text gameOver;
    public Text continueText;

    public Button[] buttons;
    public Image[] imageButton;

    public SpriteRenderer continueImage;
    public SpriteRenderer textBox;

    enum States { gameOverIn, gameOverOut, continueIn, continueOut, continueText, buttonShow, sceneChange, neutro, canContinue }
    States _state = States.neutro;
    public string textContinue;
    // Use this for initialization
    void Start()
    {
        SoundManager.instance.StopMusic();

        foreach (var button in buttons)
        {
            button.interactable = false;
        }

        _state = States.gameOverIn;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
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
                foreach (var imgButton in imageButton)
                {
                    StartCoroutine("FadeInButton", imgButton);
                }

                _state = States.neutro;
                break;
            default:
                break;
        }
    }

    IEnumerator FadeInText(Text text)
    {
        for (float f = 0f; f <= 1f; f += 0.01f)
        {
            Color c = text.color;
            c.a = f;
            text.color = c;

            yield return null;
        }
        _state = States.gameOverOut;
    }

    IEnumerator FadeOutText(Text text)
    {
        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            Color c = text.color;
            c.a = f;
            text.color = c;

            yield return null;
        }
        _state = States.continueIn;
    }

    IEnumerator FadeInImage(SpriteRenderer image)
    {
        for (float f = 0f; f <= 1f; f += 0.01f)
        {
            Color c = image.color;
            c.a = f;
            image.color = c;

            yield return null;
        }

        for (float f = 0f; f <= 1f; f += 0.01f)
        {
            Color c = textBox.color;
            c.a = f;
            textBox.color = c;

            yield return null;
        }

        SoundManager.instance.PlayMusicContinue();

        _state = States.continueText;
    }

    IEnumerator FadeOutImage(SpriteRenderer image)
    {
        for (float f = 0.5f; f >= 0f; f -= 0.01f)
        {
            Color c = image.color;
            c.a = f;
            image.color = c;
            continueText.color = c;
            //yesButton.color = c;
            textBox.color = c;

            yield return null;
        }
        _state = States.sceneChange;
    }

    IEnumerator SceneChange()
    {
        GameManager.difficulty = Difficulty.Normal;
        GameManager.NumPlayers = 2;

        SceneManager.LoadScene("Level 1");
        yield return null;
    }

    IEnumerator TextEnter(Text text)
    {
        int characters = 0;
        text.text = "";

        while (characters <= textContinue.Length - 1)
        {
            text.text += textContinue[characters];
            characters += 1;
            yield return new WaitForSeconds(0.1f);
        }
        _state = States.buttonShow;
    }

    IEnumerator FadeInButton(Image text)
    {
        for (float f = 0f; f <= 1f; f += 0.01f)
        {
            Color c = text.color;
            c.a = f;
            text.color = c;

            yield return null;
        }

        foreach (var button in buttons)
        {
            button.interactable = true;
        }

        _state = States.canContinue;
    }

    public void buttonYesClick()
    {
        if (_state == States.canContinue)
        {
            _state = States.continueOut;
        }
    }

    public void buttonNoClick()
    {
        if (_state == States.canContinue)
        {
            Debug.Log("close...");
            Application.Quit();
        }
    }
}
