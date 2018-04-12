using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Invoke("SetSpeed", 5f);
        }
    }

    private void SetSpeed()
    {
        followCamera.maxCameraPos.x = followCamera.transform.position.x;
        picture.transform.position = new Vector2(followCamera.transform.position.x, picture.transform.position.y);

        bus.speed = 60;
        Invoke("ShowPicture", 2f);
    }

    private void ShowMessage()
    {
        message.gameObject.SetActive(true);
    }

    private void ShowPicture()
    {
        message.gameObject.SetActive(false);

        picture.gameObject.SetActive(true);
    }
}
