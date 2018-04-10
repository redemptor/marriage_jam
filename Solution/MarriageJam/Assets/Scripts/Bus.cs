using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bus : MonoBehaviour
{
    const float offset = 0.1f;

    public float speed = 20f;
    public Transform busStop;
    public float timeStop = 2f;

    public AudioSource audioSourceFX;
    public AudioClip sfxDoorOpen;
    public AudioClip sfxDoorClose;

    private FollowCamera gameCamera;
    private new Rigidbody2D rigidbody2D;

    private bool doorClosed;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        gameCamera = Camera.main.GetComponent<FollowCamera>();
        gameCamera.targets = new[] { transform };

        foreach (var player in GameManager.instance.players)
        {
            if (player != null)
            {
                player.gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (timeStop <= sfxDoorClose.length && !doorClosed)
        {
            PlaySoundFX(sfxDoorClose);
            doorClosed = true;
        }

        if (timeStop <= 0 && !GameManager.instance.players[0].isActiveAndEnabled)
        {
            gameCamera.minCameraPos.x = busStop.position.x;

            for (int i = 0; i < GameManager.instance.players.Length; i++)
            {
                if (GameManager.instance.players[i] != null)
                {
                    GameManager.instance.players[i].transform.position = new Vector2(busStop.position.x - offset + (i * offset * 2), busStop.position.y);
                    GameManager.instance.players[i].gameObject.SetActive(true);
                    GameManager.instance.huds[i].SetPlayer(GameManager.instance.players[i]);
                }
            }

            gameCamera.targets = GameManager.instance.players.Select(x => x.transform).ToArray();

            SoundManager.instance.PlayMusicLevel1();
        }

        if (transform.position.x < busStop.position.x || timeStop <= 0)
        {
            rigidbody2D.velocity = new Vector2(speed * Time.deltaTime, 0);
        }
        else
        {
            if (rigidbody2D.velocity != Vector2.zero)
            {
                PlaySoundFX(sfxDoorOpen);
            }

            rigidbody2D.velocity = Vector2.zero;
            timeStop -= Time.deltaTime;
        }
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x > busStop.position.x)
        {
            Debug.Log("Start Game");
            Destroy(gameObject);
        }
    }

    public void PlaySoundFX(AudioClip audioClip)
    {
        if (audioSourceFX != null)
        {
            audioSourceFX.clip = audioClip;
            audioSourceFX.Play();
        }
    }
}
