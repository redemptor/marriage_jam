using UnityEngine;

public class Enemy : ActionActor
{
    private float KNOCKOUT_TIME = 1.0f;
    private float timeKnockout;

    protected KnockoutActor _knockoutActor;
    protected bool attacking = false;

    public float MaxDieDistanceX = 0.5f;
    public Vector3 DieDistancePower = new Vector3(0.1f, 0f, 0);

    public override void Start()
    {
        base.Start();
        _knockoutActor = new KnockoutActor(this);
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

        if (Alive && (attacking || waiting) && !isKnockOut)
        {
            Rigidbody2D.velocity = new Vector2(0f, 0f);
        }
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

    public virtual void DieAnimation()
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