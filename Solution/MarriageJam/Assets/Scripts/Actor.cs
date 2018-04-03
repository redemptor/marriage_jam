using UnityEngine;

public class Actor : MonoBehaviour
{
    protected enum ANIM_STATE { IDLE, WALK, ATTACK, HIT, DIE }

    private bool stunned = false;
    
    protected AudioSource audioSource;
    protected new Rigidbody2D rigidbody2D;
    protected Animator animator;
    protected float timeNextAttack;
    protected float timeNextHit;
    protected float timeCanDamage;
    protected float timeStunned;
    protected bool hit;
    protected int comboHit;
    protected Sprite sprite;
            
    public string Name;
    public bool Stunned { get { return stunned; } set { stunned = value; timeStunned = Time.time + 1f; } }
    public bool FacingRight { get { return sprite.facingRight; } }
    public bool Alive { get { return health > 0; } }
    public int maxMoveVelocity;
    public int moveVelocity;
    public float[] hitDurations;
    public float health;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<Sprite>();
    }

    public void Flip()
    {
        sprite.facingRight = !sprite.facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void PlaySounfFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
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
                sprite.Shake();
            }
            else
            {
                Die();
            }
        }
    }

    public virtual void SetHit(bool isHit)
    {
        hit = isHit;

        if (isHit && comboHit < hitDurations.Length)
        {
            timeNextHit += 0.2f;
        }
        else
        {
            timeNextHit = timeNextAttack;
        }
    }

    public virtual void Update()
    {
        SetAnimation();

        if (Stunned && Time.time > timeStunned)
        { Stunned = false; }
    }

    public virtual void SetAnimation()
    {
        animator.SetBool(ANIM_STATE.WALK.ToString(), IsWalk());
        animator.SetBool(ANIM_STATE.HIT.ToString(), Stunned);
        animator.SetBool(ANIM_STATE.DIE.ToString(), !Alive);
    }

    public virtual void Die()
    {

    }

    public bool IsWalk()
    {
        return rigidbody2D.velocity != Vector2.zero;
    }

}
