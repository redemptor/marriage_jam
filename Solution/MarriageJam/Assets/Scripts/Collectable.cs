using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AnimationCurve curve;
    private Vector3 currentPosition;

    private void Start()
    {
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
            currentPosition.y - curve.Evaluate(Time.time),
            currentPosition.z);
    }
}