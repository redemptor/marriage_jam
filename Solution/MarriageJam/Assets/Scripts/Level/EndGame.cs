using System.Linq;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    const float safeDistance = 0.1f;
    const float playersDistance = 0.2f;
    public Bus bus;
    public Transform busStartPosition;
    public GameObject camLimits;

    private Player[] players;
    private bool callBus;
    private FollowCamera followCamera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!callBus && GameManager.instance.State == GameState.Win && players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                var targetPosition = transform.position;
                targetPosition.x += playersDistance * i;

                var distance = Vector3.Distance(targetPosition, players[i].transform.position);

                if (distance >= safeDistance)
                {
                    var direction = (targetPosition - players[i].transform.position).normalized;
                    players[i].Rigidbody2D.velocity = direction * players[i].moveVelocity * Time.deltaTime;
                }
                else
                {
                    players[i].Rigidbody2D.velocity = Vector2.zero;
                }
            }

            if (!callBus && players.All(x => x.Rigidbody2D.velocity == Vector2.zero))
            {
                SoundManager.instance.StopMusic();

                callBus = true;
                camLimits.SetActive(false);

                bus.DestroyObjects = players.Select(x => x.gameObject).ToArray();
                bus.busStop = transform;
                followCamera.minCameraPos.x = followCamera.transform.position.x;

                Instantiate(bus, busStartPosition.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        followCamera = Camera.main.GetComponent<FollowCamera>();

        if (GameManager.instance.State == GameState.Play && collision.CompareTag("Player"))
        {
            GameManager.instance.State = GameState.Win;
            followCamera.maxCameraPos.x += 10f;

            players = FindObjectsOfType<Player>();
        }
    }
}
