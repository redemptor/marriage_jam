using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankSlip : Enemy
{

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
        base.Attack();

        comboHit++;

        if (comboHit == 1)
        { CurrentDamage = DamageNormal; }

        if (comboHit == hitDurations.Length)
        { CurrentDamage = DamageStrong; }

        if (comboHit > hitDurations.Length)
        { comboHit = 1; }

        timeNextAttack = Time.time + hitDurations[comboHit - 1];
        timeNextHit = timeNextAttack;
    }
}
