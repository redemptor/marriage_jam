using System.Linq;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Vector3 currentPosition;
    private SpriteRenderer item;
    private SpriteRenderer shadow;

    public AudioClip SfxGet;
    public AnimationCurve curve;

    private void Start()
    {
        var sprites = GetComponentsInChildren<SpriteRenderer>();
        item = sprites.FirstOrDefault(x => x.name.Equals("Item"));
        shadow = sprites.FirstOrDefault(x => x.name.Equals("Shadow"));

        if (item == null)
        {
            item = sprites[0];
        }

        curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.8f, 0.05f))
        {
            preWrapMode = WrapMode.PingPong,
            postWrapMode = WrapMode.PingPong
        };

        currentPosition = item.transform.localPosition;
    }

    private void Update()
    {
        item.transform.localPosition = new Vector3(
            currentPosition.x,
            currentPosition.y + curve.Evaluate(Time.time),
            currentPosition.z);

        var shadowScale = 1 - curve.Evaluate(Time.time) * 4f;
        shadow.transform.localScale = new Vector3(shadowScale, shadowScale, shadow.transform.localScale.z);
    }
}