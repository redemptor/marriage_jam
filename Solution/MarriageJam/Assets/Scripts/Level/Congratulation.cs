using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Congratulation : MonoBehaviour
{
    public FollowCamera followCamera;
    public Bus bus;
    public SpriteRenderer picture;
    public Text message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bus"))
        {
            bus = collision.GetComponent<Bus>();
            bus.speed = 0;
            bus.audioSourceEngine.Stop();
            SoundManager.instance.PlayMusicMenu();

            Invoke("ShowMessage", 1f);
            Invoke("EndGame", 8f);
        }
    }

    private void Update()
    {
        if (picture.gameObject.activeSelf && Input.anyKey)
        {
            Invoke("LoadCredits", 2f);
        }
    }

    private void EndGame()
    {
        followCamera.maxCameraPos.x = followCamera.transform.position.x;
        picture.transform.position = new Vector2(followCamera.transform.position.x, picture.transform.position.y);

        message.gameObject.SetActive(false);

        bus.speed = 60;
        Invoke("ShowPicture", 2f);
    }

    private void ShowMessage()
    {
        message.gameObject.SetActive(true);
    }

    private void ShowPicture()
    {
        picture.gameObject.SetActive(true);
    }

    private void LoadCredits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
}
