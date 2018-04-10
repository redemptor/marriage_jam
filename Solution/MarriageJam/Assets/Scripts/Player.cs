using UnityEngine;

public class Player : ActionActor
{
    const string buttonNameFormat = "P{0}_{1}";

    public int score = 0;
    public int joystickNumber;

    public override void Update()
    {
        base.Update();
        SetAnimation();

        if (Time.time > timeNextHit)
        {
            SetHit(false);
            comboHit = 0;
        }

        if (Input.GetButtonDown(GetButtonName(GlobalFields.BUTTONS.Attack.ToString())) && Time.time > timeNextAttack)
        {
            Attack();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 move = Vector3.zero;

        if (comboHit == 0
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
