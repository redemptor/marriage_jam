using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bus : MonoBehaviour
{
    public float speed = 50f;
    public Transform busStop;
    public float timeStop = 2f;

    public AudioSource audioSourceEngine;
    public AudioSource audioSourceFX;
    public AudioClip sfxDoorOpen;
    public AudioClip sfxDoorClose;

    public GameObject[] DestroyObjects;

    private FollowCamera gameCamera;
    private new Rigidbody2D rigidbody2D;

    private bool doorClosed;
    private bool spawnPlayer;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        gameCamera = Camera.main.GetComponent<FollowCamera>();
        gameCamera.targets = new[] { transform };

        //foreach (var player in GameManager.instance.players)
        //{
        //    if (player != null)
        //    {
        //        player.gameObject.SetActive(false);
        //    }
        //}

        if (GameManager.instance.State != GameState.Win)
        {
            GameManager.instance.State = GameState.Wait;
        }
        else
        {

        }
    }

    private void FixedUpdate()
    {
        if (timeStop <= sfxDoorClose.length && !doorClosed)
        {
            PlaySoundFX(sfxDoorClose);
            doorClosed = true;
        }

        if (timeStop <= 0 && !spawnPlayer)
        {
            gameCamera.minCameraPos.x = busStop.position.x;

            if (GameManager.instance.State != GameState.Win)
            {
                GameManager.instance.SpawnPlayers(busStop.position);
                gameCamera.targets = GameManager.instance.players.Select(x => x.transform).ToArray();
            }

            GameManager.instance.ShowHUD(false);

            spawnPlayer = true;
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

                foreach (var obj in DestroyObjects)
                {
                    Destroy(obj);
                }
            }

            rigidbody2D.velocity = Vector2.zero;
            timeStop -= Time.deltaTime;
        }
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x > busStop.position.x)
        {
            if (GameManager.instance.State != GameState.Win)
            {
                GameManager.instance.State = GameState.Play;
                GameManager.instance.ShowHUD(true);
                SoundManager.instance.PlayMusicLevel1();
            }

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
