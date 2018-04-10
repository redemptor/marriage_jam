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

        if (Time.time > timeNextHit)
        {
            SetHit(false);
            comboHit = 0;
        }
    }

    public override void SetAnimation()
    {
        base.SetAnimation();

        animator.SetInteger(ANIM_STATE.ATTACK.ToString(), comboHit);
    }

    public override void Attack()
    {
        if (!Alive || IsKnockOut || Stunned) { return; }
        base.Attack();

        comboHit++;

        if (comboHit == 1)
        { CurrentDamage = DamageNormal; }

        if (comboHit == hitDurations.Length)
        {
            CurrentDamage = DamageStrong;
            _iaFollowActor.ForceRandomMove(true);
        }

        if (comboHit > hitDurations.Length)
        { comboHit = 1; }

        timeNextAttack = Time.time + hitDurations[comboHit - 1];
        timeNextHit = timeNextAttack;
    }
}
