using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AnimationCurve curve;

    private Vector3 currentPosition;
    private new Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();

        curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.8f, 0.05f))
        {
            preWrapMode = WrapMode.PingPong,
            postWrapMode = WrapMode.PingPong
        };

        currentPosition = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(
            currentPosition.x,
            currentPosition.y + curve.Evaluate(Time.time),
            currentPosition.z);

        collider.offset = new Vector2(0f, -curve.Evaluate(Time.time));
    }
}