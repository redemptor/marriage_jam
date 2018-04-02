using UnityEngine;

public class Player : Actor
{
    //enum PLAYER_STATE { IDLE, WALK, JUMP, FALLING, DIE, ATTACK }
    enum PLAYER_ANIM_STATE { IDLE, WALK, JUMP, PUNCH01, PUNCH02, PUNCH03 }

    //private int _TimeReleaseCombo = 0;
    //private bool _NeedUpdateAttackAnimation = true;

    //private PLAYER_STATE _PlayerState = PLAYER_STATE.IDLE;
    //private PLAYER_ANIM_STATE _PlayerAttackState = PLAYER_ANIM_STATE.IDLE;


    void Update()
    {
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

    void SetAnimation()
    {
        animator.SetBool(PLAYER_ANIM_STATE.WALK.ToString(), IsWalk());
        animator.SetInteger("ATTACK", comboHit);
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



    public bool IsWalk()
    {
        return rigidbody2D.velocity != Vector2.zero;
    }

    //public bool IsIddle()
    //{
    //    return _PlayerState == PLAYER_STATE.IDLE;
    //}

    //public bool IsPunch01()
    //{
    //    return (_PlayerState == PLAYER_STATE.ATTACK
    //             && _PlayerAttackState == PLAYER_ANIM_STATE.PUNCH01);
    //}

    //public bool IsPunch02()
    //{
    //    return (_PlayerState == PLAYER_STATE.ATTACK
    //             && _PlayerAttackState == PLAYER_ANIM_STATE.PUNCH02);
    //}

    //public bool IsPunch03()
    //{
    //    return (_PlayerState == PLAYER_STATE.ATTACK
    //             && _PlayerAttackState == PLAYER_ANIM_STATE.PUNCH03);
    //}
}
