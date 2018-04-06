using UnityEngine;

public class Enemy : ActionActor
{
    private float KNOCKOUT_TIME = 1.0f;
    private float timeKnockout;
    private bool attacking = false;
    private KnockoutActor _knockoutActor;
    private IaFollowActor _iaFollowActor;

    public float MaxDieDistanceX = 2f;
    public Vector3 DieDistancePower = new Vector3(0.1f, 0f, 0);

    public override void Start()
    {
        base.Start();
        _knockoutActor = new KnockoutActor(this);
        _iaFollowActor = new IaFollowActor(this);
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

        if (Alive && attacking)
        {
            Rigidbody2D.velocity = new Vector2(0f, 0f);
        }

        if (!IsKnockOut && !Stunned && !attacking && Alive)
        {
            _iaFollowActor.FixedUpdate();
        }
    }

    public override void SetAnimation()
    {
        base.SetAnimation();
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

    public override void StopAttack()
    {
        attacking = false;
        base.StopAttack();
    }

    public override void Attack()
    {
        attacking = true;
        base.Attack();
    }

}
