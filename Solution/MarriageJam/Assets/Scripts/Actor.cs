using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
    protected enum ANIM_STATE { IDLE, WALK, ATTACK, HIT, DIE, KNOCKOUT }

    protected bool stunned = false;
    protected ShakeSprite _shakeSprite;
    protected BlinkSprite _blinkSprite;

    protected AudioSource audioSource;
    protected AudioSource audioSourceCH02;
    protected SpriteRenderer spriteRenderer;
    protected new Rigidbody2D rigidbody2D;
    protected Animator animator;
    protected CustomSprite sprite;
    protected float timeCanDamage;
    protected float timeStunned;
    protected bool isKnockOut = false;

    public Rigidbody2D Rigidbody2D { get { return rigidbody2D; } }
    public AudioClip SfxDie;
    public float shakePower;
    public bool IsKnockOut { get { return isKnockOut; } }
    public bool facingRight = true;
    public bool IsVisible
    {
        get
        {
            return spriteRenderer.isVisible;
        }
    }

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

    public int health;
    public int maxHealth { get; private set; }

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

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<CustomSprite>();

        maxHealth = health;
    }

    public virtual void Start()
    {
        _shakeSprite = new ShakeSprite(spriteRenderer);
        _blinkSprite = new BlinkSprite(spriteRenderer);
        audioSourceCH02 = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySoundFXCH02(AudioClip audioClip, bool loop)
    {
        if (audioSourceCH02 != null && audioClip != null)
        {
            audioSourceCH02.clip = audioClip;
            audioSourceCH02.loop = loop;
            audioSourceCH02.Play();
        }
    }

    public void PlaySoundsFX(AudioClip audioClip, bool loop)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }

    public void StopSoundsFX(AudioClip audioClip)
    {
        if (audioSource != null)
        {
            audioSource.clip = audioClip;
            audioSource.Stop();
        }
    }

    public virtual void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public virtual void SetDamage(Damage damage)
    {
        if (Time.time > timeCanDamage)
        {
            health -= damage.Value;

            if (health < 0)
            {
                health = 0;
            }

            timeCanDamage = Time.time + 0.2f;

            if (!sprite.isStatic)
            {
                if (FacingRight && !damage.AttackFromRight)
                {
                    Flip();
                }
                else if (!FacingRight && damage.AttackFromRight)
                {
                    Flip();
                }
            }

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

            if (damage.SfxHit != null)
            { PlaySoundsFX(damage.SfxHit, false); }
        }
    }

    public void Shake()
    {
        _shakeSprite.Shake(2, facingRight, shakePower);
    }

    public void Blink(int times = 5)
    {
        _blinkSprite.Blink(times);
    }

    public bool IsWalk()
    {
        return rigidbody2D.velocity != Vector2.zero && !IsKnockOut && Alive;
    }

    public virtual void FixedUpdate()
    {
        _shakeSprite.FixedUpdate();
        _blinkSprite.FixedUpdate();
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