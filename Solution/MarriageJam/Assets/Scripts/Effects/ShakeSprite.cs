using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeSprite
{
    private SpriteRenderer _spriteRenderer;
    private float _timeEffect;
    private int _timesToShake = 0;
    private bool _facingRight = false;
    private float _power = 0;
    private float _timeBack = 0;

    public ShakeSprite(SpriteRenderer spriteRenderer)
    {
        _spriteRenderer = spriteRenderer;
    }

    void Start()
    {

    }
    
    public void FixedUpdate()
    {
        if (_timesToShake > 0)
        {
            if (Time.time > _timeBack)
            {
                Vector3 currentPosition = _spriteRenderer.transform.position;

                _timeBack = Time.time + 0.1f;
                _timesToShake--;
                if (_facingRight)
                {
                    currentPosition.x -= _power;
                    _spriteRenderer.transform.position = currentPosition;
                }
                else
                {
                    currentPosition.x += _power;
                    _spriteRenderer.transform.position = currentPosition;
                }
                _facingRight = !_facingRight;
            }
        }
    }

    public void Shake(int times, bool facingRight, float power)
    {
        _timesToShake = times * 2;
        _facingRight = facingRight;
        _power = power;
    }
}
