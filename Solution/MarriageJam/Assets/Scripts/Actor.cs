using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
    protected enum ANIM_STATE { IDLE, WALK, ATTACK, HIT, DIE, KNOCKOUT }

    private bool stunned = false;
    private ShakeSprite _shakeSprite;
    private BlinkSprite _blinkSprite;
    private Vector3 _lastPosition;

    protected AudioSource audioSource;
    protected SpriteRenderer spriteRenderer;
    protected new Rigidbody2D rigidbody2D;
    protected Animator animator;
    protected Sprite sprite;
    protected float timeCanDamage;
    protected float timeStunned;
    protected bool isKnockOut = false;

    public Rigidbody2D Rigidbody2D { get { return rigidbody2D; } }
    public float shakePower;
    public bool IsKnockOut { get { return isKnockOut; } }
    public bool facingRight = true;
    public Animator Animator
    {
        get { return animator; }
    }

    public bool FinishBlink
    {
        get { return _blinkSprite.FinishBlink; }
    }

    public bool DoBlink
    {
        get { return _blinkSprite.DoBlink; }
    }
    public bool FacingRight
    {
        get { return facingRight; }
    }
    public bool Alive
    {
        get { return health > 0; }
    }
    public float health;

    public bool Stunned
    {
        get
        { return stunned; }
        set
        {
            stunned = value;
            timeStunned = Time.time + 1f;
        }
    }

    public virtual void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<Sprite>();

        _shakeSprite = new ShakeSprite(spriteRenderer);
        _blinkSprite = new BlinkSprite(spriteRenderer);
        _lastPosition = transform.position;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void PlaySounfFX(AudioClip audioClip)
    {
        if (audioSource != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public virtual void SetDamage(Damage damage)
    {
        if (Time.time > timeCanDamage)
        {
            health -= damage.Value;
            timeCanDamage = Time.time + 0.2f;
            if (Rigidbody2D != null)
            { Rigidbody2D.velocity = new Vector2(0, 0); }

            if (Alive)
            {
                if (isKnockOut)
                {
                    Stunned = false;
                }
                else
                {
                    Stunned = true;
                    Shake();
                }
            }
            else
            {
                Stunned = false;
                isKnockOut = false;
                Die();
            }
        }
    }

    public void Shake()
    {
        _shakeSprite.Shake(2, facingRight, shakePower);
    }

    public void Blink()
    {
        _blinkSprite.Blink(5);
    }

    public bool IsWalk()
    {
        return rigidbody2D.velocity != Vector2.zero;
    }

    public virtual void FixedUpdate()
    {
        _shakeSprite.FixedUpdate();
        _blinkSprite.FixedUpdate();

        if (
            Alive
            && !stunned
            && !IsKnockOut
            && (Decimal.Round((decimal)_lastPosition.x, 2) - Decimal.Round((decimal)transform.position.x, 2) != 0)
            && (_lastPosition.x - transform.position.x > 0 && facingRight
            || _lastPosition.x - transform.position.x < 0 && !facingRight))
        {
            Flip();
        }
        _lastPosition = transform.position;
    }

    public virtual void Update()
    {
        SetAnimation();
    }

    public virtual void SetAnimation() { }

    public virtual void Die()
    {
        Rigidbody2D.velocity = new Vector2(0, 0);
    }
}