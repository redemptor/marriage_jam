using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bus : MonoBehaviour
{
    const float offset = 0.1f;

    public float speed = 20f;
    public Transform busStop;
    public float timeStop = 2f;

    [SerializeField]
    private FollowCamera gameCamera;
    private new Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        gameCamera = (FollowCamera)FindObjectOfType(typeof(FollowCamera));
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
        if (timeStop <= 0 && !GameManager.instance.players[0].isActiveAndEnabled)
        {
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
        }

        if (transform.position.x < busStop.position.x || timeStop <= 0)
        {
            rigidbody2D.velocity = new Vector2(speed * Time.deltaTime, 0);
        }
        else
        {
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
}
