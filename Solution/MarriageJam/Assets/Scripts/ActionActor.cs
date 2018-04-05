using UnityEngine;

public class ActionActor : Actor
{
    public string Name;
    public Damage DamageNormal;
    public Damage DamageStrong;
    public Damage CurrentDamage;

    public int maxMoveVelocity;
    public int moveVelocity;
    public float[] hitDurations;

    protected float timeNextAttack;
    protected float timeNextHit;
    protected bool hit;
    protected int comboHit;

    public override void Start()
    {
        base.Start();
        if (DamageNormal != null)
        { CurrentDamage = DamageNormal; }
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

    public override void Update()
    {
        base.Update();

        if (Stunned && Time.time > timeStunned)
        {
            Stunned = false;
        }
    }

    public bool IsWalk()
    {
        return rigidbody2D.velocity != Vector2.zero;
    }

    public override void SetAnimation()
    {
        base.SetAnimation();

        animator.SetBool(ANIM_STATE.WALK.ToString(), IsWalk());
        animator.SetBool(ANIM_STATE.HIT.ToString(), Stunned);
        animator.SetBool(ANIM_STATE.DIE.ToString(), !Alive);
        animator.SetBool(ANIM_STATE.KNOCKOUT.ToString(), isKnockOut);
    }
}
