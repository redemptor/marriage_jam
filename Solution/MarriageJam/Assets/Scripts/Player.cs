using UnityEngine;

public class Player : ActionActor
{
    public override void Update()
    {
        base.Update();
        SetAnimation();

        if (Time.time > timeNextHit)
        {
            SetHit(false);
            comboHit = 0;
        }

        if (Input.GetButtonDown(GlobalFields.BUTTONS.ACTION.ToString()) && Time.time > timeNextAttack)
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;

        if (comboHit == 0)
        {
            move.x = Input.GetAxis("Horizontal") * moveVelocity * Time.deltaTime;
            move.y = Input.GetAxis("Vertical") * moveVelocity * Time.deltaTime;
        }

        if (move.x < 0 && facingRight || move.x > 0 && !facingRight)
        {
            Flip();
        }

        rigidbody2D.velocity = move;
    }

    public override void SetAnimation()
    {
        base.SetAnimation();
        animator.SetInteger(ANIM_STATE.ATTACK.ToString(), comboHit);
    }

    private void Attack()
    {
        comboHit++;

        if (comboHit > hitDurations.Length)
        {
            comboHit = 1;
        }

        timeNextAttack = Time.time + hitDurations[comboHit - 1];
        timeNextHit = timeNextAttack;
    }
}
