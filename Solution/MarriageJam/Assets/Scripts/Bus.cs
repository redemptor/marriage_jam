using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bus : MonoBehaviour
{
    public Player player;

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
        gameCamera.target = transform;
    }

    private void FixedUpdate()
    {
        if (transform.position.x < busStop.position.x)
        {
            rigidbody2D.velocity = new Vector2(speed * Time.deltaTime, 0);
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}
