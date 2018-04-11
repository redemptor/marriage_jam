using UnityEngine;

public class Player : ActionActor
{
    const string buttonNameFormat = "P{0}_{1}";

    private KnockoutActor _knockoutActor;
    private float KNOCKOUT_TIME = 1.0f;
    private float timeKnockout;

    public int score = 0;
    public int joystickNumber;

    public float MaxDieDistanceX = 0.5f;
    public Vector3 DieDistancePower = new Vector3(0.1f, 0f, 0);

    public AudioClip SfxHitAir;


    public override void Start()
    {
        base.Start();
        _knockoutActor = new KnockoutActor(this);
    }

    public override void Update()
    {
        base.Update();
        _knockoutActor.Update();

        if (Time.time > timeNextHit)
        {
            SetHit(false);
            comboHit = 0;
        }

        if (GameManager.instance.State == GameState.Play && Input.GetButtonDown(GetButtonName(BUTTONS.Attack.ToString())) && Time.time > timeNextAttack)
        {
            Attack();
        }

        if (isKnockOut && !_knockoutActor.DoKnockOut)
        {
            if (timeKnockout == 0)
            { timeKnockout = Time.time + KNOCKOUT_TIME; }

            if (Time.time > timeKnockout)
            { isKnockOut = false; }
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

        if (!_knockoutActor.DoKnockOut)
        {
            base.SetDamage(damage);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 move = Vector3.zero;

        if (GameManager.instance.State == GameState.Play
            && comboHit == 0
            && !IsKnockOut
            && !Stunned
            && Alive)
        {
            move.x = Input.GetAxis(GetButtonName("Horizontal")) * moveVelocity * Time.deltaTime;
            move.y = Input.GetAxis(GetButtonName("Vertical")) * moveVelocity * Time.deltaTime;
        }

        rigidbody2D.velocity = move;
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
        {
            CurrentDamage = DamageNormal;
            PlaySoundsFX(SfxHitAir, false);
        }

        if (comboHit == hitDurations.Length)
        {
            CurrentDamage = DamageStrong;
        }

        if (comboHit > hitDurations.Length)
        {
            comboHit = 1;
        }

        timeNextAttack = Time.time + hitDurations[comboHit - 1];
        timeNextHit = timeNextAttack;
    }

    private string GetButtonName(string button)
    {
        return string.Format(buttonNameFormat, joystickNumber, button);
    }
}
