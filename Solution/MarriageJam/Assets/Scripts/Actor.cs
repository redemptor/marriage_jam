using UnityEngine;

public class Actor : MonoBehaviour
{
    public float health;

    protected AudioSource audioSource;
    protected new Rigidbody2D rigidbody2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected bool facingRight = true;

    public int maxMoveVelocity;
    public int moveVelocity;

    protected bool hit;
    public int maxHitCombo;
    protected int comboHit;

    protected float timeNextAttack;
    protected float timeNextHit;
    protected float timeCanDamage;

    public float[] hitDurations;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void PlaySounfFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void SetDamage(float damage)
    {
        if (Time.time > timeCanDamage)
        {
            health -= damage;
            timeCanDamage = Time.time + 0.1f;
        }
    }

    public void SetHit(bool isHit)
    {
        hit = isHit;

        if (isHit && comboHit < maxHitCombo)
        {
            timeNextHit += 0.2f;
        }
        else
        {
            timeNextHit = timeNextAttack;
        }
    }

    private void FixedUpdate()
    {

    }
}
