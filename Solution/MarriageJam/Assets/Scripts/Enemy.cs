using UnityEngine;

public class Enemy : ActionActor
{
    private float KNOCKOUT_TIME = 1.0f;

    private KnockoutActor _knockoutActor;
    private IaFollowActor _iaFollowActor;
    private IaAttackMeleeActor _iaAttackMeleeActor;
    private float timeKnockout;

    public float MaxDieDistanceX = 2f;
    public Vector3 DieDistancePower = new Vector3(0.1f, 0f, 0);

    public override void Start()
    {
        base.Start();
        _knockoutActor = new KnockoutActor(this);
        _iaFollowActor = new IaFollowActor(this);
        //   _iaAttackMeleeActor = new IaAttackMeleeActor();
    }

    public override void Update()
    {
        base.Update();

        _knockoutActor.Update();
        if (!Alive)
        {
            DieAnimation();
        }
        if (isKnockOut && !_knockoutActor.DoKnockOut)
        {
            if (timeKnockout == 0)
            { timeKnockout = Time.time + KNOCKOUT_TIME; }

            if (Time.time > timeKnockout)
            { isKnockOut = false; }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!IsKnockOut && !Stunned && Alive)
        {
            _iaFollowActor.FixedUpdate();
            // _iaAttackMeleeActor.FixedUpdate();
        }
    }

    public override void SetAnimation()
    {
        base.SetAnimation();
        animator.SetInteger(ANIM_STATE.ATTACK.ToString(), 0);
    }

    public override void SetDamage(Damage damage)
    {
        if (damage.Knockout)
        {
            isKnockOut = true;
            timeKnockout = 0;
            _knockoutActor.KnockOut(DieDistancePower, MaxDieDistanceX);
        }

        base.SetDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        _knockoutActor.KnockOut(DieDistancePower, MaxDieDistanceX);
    }

    private void DieAnimation()
    {
        if (!_knockoutActor.DoKnockOut)
        {
            if (FinishBlink)
            {
                Destroy(gameObject);
            }
            if (!DoBlink)
            {
                Blink();
            }
        }
    }
}
