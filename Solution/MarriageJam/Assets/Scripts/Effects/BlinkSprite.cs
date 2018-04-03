using UnityEngine;

public class BlinkSprite
{
    private SpriteRenderer _spriteRenderer;
    private float _times = 0;
    private float _timeBlink = 0;
    public bool FinishBlink;
    public bool DoBlink
    {
        get { return _times > 0; }
    }

    public BlinkSprite(SpriteRenderer spriteRenderer)
    {
        _spriteRenderer = spriteRenderer;
    }

    public void FixedUpdate()
    {
        if (DoBlink)
        {
            if (Time.time > _timeBlink)
            {
                Vector3 currentPosition = _spriteRenderer.transform.position;

                _timeBlink = Time.time + 0.1f;
                _times--;

                _spriteRenderer.enabled = !_spriteRenderer.enabled;

                if (_times == 0) { FinishBlink = true;  }
            }
        }
    }

    public void Blink(int times)
    {
        _times = times * 2;
        FinishBlink = false;
    }
}
