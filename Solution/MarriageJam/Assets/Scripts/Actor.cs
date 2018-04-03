using UnityEngine;

public class Actor : MonoBehaviour
{
    protected enum ANIM_STATE { IDLE, WALK, ATTACK, HIT, DIE }

    protected AudioSource audioSource;
    protected SpriteRenderer spriteRenderer;
    protected new Rigidbody2D rigidbody2D;
    protected Animator animator;
    protected Sprite sprite;

    protected float timeCanDamage;

    public float shakePower;

    public bool FacingRight
    {
        get
        {
            return sprite.facingRight;
        }
    }

    public bool Alive
    {
        get
        {
            return health > 0;
        }
    }

    public float health;

    private bool stunned = false;
    protected float timeStunned;

    private ShakeSprite _shakeSprite;


    public bool Stunned
    {
        get
        {
            return stunned;
        }
        set
        {
            stunned = value; timeStunned = Time.time + 1f;
        }
    }

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<Sprite>();

        _shakeSprite = new ShakeSprite(spriteRenderer);

    }

    public void Flip()
    {
        sprite.facingRight = !sprite.facingRight;
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

    public virtual void SetDamage(float damage)
    {
        if (Time.time > timeCanDamage)
        {
            health -= damage;
            timeCanDamage = Time.time + 0.2f;

            if (Alive)
            {
                Stunned = true;
                Shake();
            }
            else
            {
                Die();
            }
        }
    }

    public void Shake()
    {
        _shakeSprite.Shake(2, sprite.facingRight, shakePower);
    }

    public virtual void Update()
    {
        SetAnimation();
    }

    private void FixedUpdate()
    {
        _shakeSprite.FixedUpdate();
    }

    public virtual void SetAnimation() { }

    public virtual void Die() { }
}
