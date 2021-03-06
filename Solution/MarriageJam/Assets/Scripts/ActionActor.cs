﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class ActionActor : Actor
{
    public Sprite NormalAvatar;
    public Sprite HittedAvatar;
    public Sprite NameActorSprite;

    public Damage DamageNormal;
    public Damage DamageStrong;
    public Damage CurrentDamage;

    public AudioClip SfxHit1;
    public AudioClip SfxHit2;
    public AudioClip SfxHit3;
    public AudioClip SfxKnockout;

    public bool waiting;
    public int ComboHit
    {
        get { return comboHit; }
    }
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

    public override void SetDamage(Damage damage)
    {
        if (Alive)
        {
            if (health - damage.Value <= 0)
            {
                PlaySoundFXCH02(SfxDie, false);
            }
            else if (damage.Knockout)
            {
                PlaySoundFXCH02(SfxKnockout, false);
            }
        }

        base.SetDamage(damage);
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

        if (waiting && !isKnockOut && Alive)
        {
            Rigidbody2D.velocity = new Vector2(0, 0);
        }

        if (Stunned && Time.time > timeStunned)
        {
            Stunned = false;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (
            Alive
            && !Stunned
            && !IsKnockOut
            && (rigidbody2D.velocity.x < 0 && facingRight
            || rigidbody2D.velocity.x > 0 && !facingRight))
        {
            Flip();
        }
    }

    public override void SetAnimation()
    {
        base.SetAnimation();

        animator.SetBool(ANIM_STATE.WALK.ToString(), IsWalk());
        animator.SetBool(ANIM_STATE.HIT.ToString(), Stunned);
        animator.SetBool(ANIM_STATE.DIE.ToString(), !Alive);
        animator.SetBool(ANIM_STATE.KNOCKOUT.ToString(), isKnockOut);
    }

    public override void Die()
    {
        base.Die();
        comboHit = 0;
    }

    public virtual void Attack() { }
    public virtual void StopAttack() { }

    public override void Flip()
    {
        base.Flip();
        DamageNormal.AttackFromRight = !facingRight;
        DamageStrong.AttackFromRight = !facingRight;
    }
}
