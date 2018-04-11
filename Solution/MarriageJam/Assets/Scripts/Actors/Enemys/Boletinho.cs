using UnityEngine;

public class Boletinho : Enemy
{
    private IaFollowActor _iaFollowActor;

    public override void Start()
    {
        base.Start();
        _iaFollowActor = new IaFollowActor(this);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!IsKnockOut && !Stunned && !attacking && !waiting && Alive)
        {
            _iaFollowActor.FixedUpdate();
        }
    }

    public override void Update()
    {
        base.Update();

        if (comboHit > 0 && Time.time > timeNextHit)
        {
            SetHit(false);
            comboHit = 0;
        }
    }

    public override void SetAnimation()
    {
        animator.SetInteger(ANIM_STATE.ATTACK.ToString(), comboHit);
        base.SetAnimation();
    }

    public override void Attack()
    {
        if (!Alive || IsKnockOut || Stunned) { return; }

        base.Attack();
        comboHit++;

        switch (comboHit)
        {
            case 1:
                CurrentDamage = DamageNormal;
                CurrentDamage.Combo = 1;
                CurrentDamage.SfxHit = SfxHit1;

                break;
            case 2:
                CurrentDamage.Combo = 2;
                CurrentDamage.SfxHit = SfxHit1;
                break;
        }

        if (comboHit == hitDurations.Length)
        {
            CurrentDamage = DamageStrong;
            _iaFollowActor.ForceRandomMove(true);
            CurrentDamage.Combo = 3;
            CurrentDamage.SfxHit = SfxHit1;
        }

        if (comboHit > hitDurations.Length)
        { comboHit = 1; }

        timeNextAttack = Time.time + hitDurations[comboHit - 1];
        timeNextHit = timeNextAttack;
    }

    public override void SetDamage(Damage damage)
    {
        base.SetDamage(damage);
    }

    public override void Die()
    {
        base.Die();

        PlaySoundsFX(SfxDie, false);
    }



}
