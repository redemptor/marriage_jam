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


        //switch (_PlayerState)
        //{
        //    case PLAYER_STATE.IDLE:
        //        if (move != Vector3.zero)
        //        {
        //            _PlayerState = PLAYER_STATE.WALK;
        //        }

        //        break;

        //    case PLAYER_STATE.WALK:
        //        if (move == Vector3.zero)
        //        {
        //            _PlayerState = PLAYER_STATE.IDLE;
        //        }

        //        if (Input.GetButtonDown(GlobalFields.BUTTONS.ACTION.ToString()))
        //        {
        //            _PlayerState = PLAYER_STATE.ATTACK;
        //            return;
        //        }


        //        if (move != Vector3.zero)
        //        {
        //            _PlayerState = PLAYER_STATE.WALK;
        //        }

        //        break;

        //    case PLAYER_STATE.ATTACK:



        //        //if (_NeedUpdateAttackAnimation)
        //        //{
        //        //    switch (_PlayerAttackState)
        //        //    {
        //        //        case PLAYER_ANIM_STATE.IDLE:
        //        //            _PlayerAttackState = PLAYER_ANIM_STATE.PUNCH01;
        //        //            _TimeReleaseCombo = 0;
        //        //            break;
        //        //        case PLAYER_ANIM_STATE.PUNCH01:
        //        //            _PlayerAttackState = PLAYER_ANIM_STATE.PUNCH02;
        //        //            _TimeReleaseCombo = 0;
        //        //            break;
        //        //        case PLAYER_ANIM_STATE.PUNCH02:
        //        //            _PlayerAttackState = PLAYER_ANIM_STATE.PUNCH03;
        //        //            _TimeReleaseCombo = 20;
        //        //            break;
        //        //    }
        //        //    _NeedUpdateAttackAnimation = false;
        //        //}
        //        //else
        //        //{
        //        //    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1
        //        //        && !animator.IsInTransition(0))
        //        //    {
        //        //        _NeedUpdateAttackAnimation = true;
        //        //        _PlayerState = PLAYER_STATE.IDLE;
        //        //    }
        //        //}

        //        break;
        //}



        rigidbody2D.velocity = move;

        //ControlAttackCombo();
    }

    void SetAnimation()
    {
        //animator.SetBool(PLAYER_ANIM_STATE.IDLE.ToString(), IsIddle());
        animator.SetBool(PLAYER_ANIM_STATE.WALK.ToString(), IsWalk());
        animator.SetInteger("ATTACK", comboHit);

        //animator.SetBool(PLAYER_ANIM_STATE.PUNCH01.ToString(), IsPunch01());
        //animator.SetBool(PLAYER_ANIM_STATE.PUNCH02.ToString(), IsPunch02());
        //animator.SetBool(PLAYER_ANIM_STATE.PUNCH03.ToString(), IsPunch03());
    }

    private void Attack()
    {
        //_PlayerState = PLAYER_STATE.ATTACK;
        comboHit++;

        if (comboHit > maxHitCombo)
        {
            comboHit = 1;
        }

        timeNextAttack = Time.time + hitDurations[comboHit - 1];
        timeNextHit = timeNextAttack;
    }

    private void ControlAttackCombo()
    {
        //if (_PlayerState == PLAYER_STATE.IDLE
        //    && _PlayerAttackState != PLAYER_ANIM_STATE.IDLE)
        //{
        //    _TimeReleaseCombo++;
        //    if (_TimeReleaseCombo >= 20)
        //    {
        //        _NeedUpdateAttackAnimation = true;
        //        _PlayerState = PLAYER_STATE.IDLE;
        //        _PlayerAttackState = PLAYER_ANIM_STATE.IDLE;
        //    }
        //}
    }


    public bool IsWalk()
    {
        return rigidbody2D.velocity != Vector2.zero;
        //return _PlayerState == PLAYER_STATE.WALK;
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
