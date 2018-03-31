using UnityEngine;

public class Player : MonoBehaviour
{
    enum PLAYER_STATE { IDLE, WALK, JUMP, FALLING, DIE, ATTACK }
    enum PLAYER_ANIM_STATE { IDLE, WALK, JUMP, PUNCH01, PUNCH02, PUNCH03 }
    enum PLAYER_COLLIDERS { PlayerTop, PlayerBottom, PlayerLeft, PlayerRight }

    private int _TimeReleaseCombo = 0;
    private bool _IsCollideTop = false;
    private bool _IsCollideBottom = false;
    private bool _IsCollideRight = false;
    private bool _IsCollideLeft = false;
    private bool _NeedUpdateAttackAnimation = true;
    private Animator _Animator;
    private PLAYER_STATE _PlayerState = PLAYER_STATE.IDLE;
    private PLAYER_ANIM_STATE _PlayerAttackState = PLAYER_ANIM_STATE.IDLE;

    public int MaxJumpPower = -2;
    public int MaxMoveVelocity;
    public int MoveVelocity;
    public int JumpVelocity;

    void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    void Update()
    {
        _Animator.SetBool(PLAYER_ANIM_STATE.IDLE.ToString(), IsIddle());
        _Animator.SetBool(PLAYER_ANIM_STATE.WALK.ToString(), IsWalk());
        _Animator.SetBool(PLAYER_ANIM_STATE.JUMP.ToString(), IsFalling());
        _Animator.SetBool(PLAYER_ANIM_STATE.JUMP.ToString(), IsJump());
        _Animator.SetBool(PLAYER_ANIM_STATE.PUNCH01.ToString(), IsPunch01());
        _Animator.SetBool(PLAYER_ANIM_STATE.PUNCH02.ToString(), IsPunch02());
        _Animator.SetBool(PLAYER_ANIM_STATE.PUNCH03.ToString(), IsPunch03());
     
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        move.x = (Input.GetAxis("Horizontal") * MoveVelocity) * Time.deltaTime;
        move.y = (Input.GetAxis("Vertical") * JumpVelocity) * Time.deltaTime;

        switch (_PlayerState)
        {
            case PLAYER_STATE.IDLE:
                if (move != Vector3.zero)
                { _PlayerState = PLAYER_STATE.WALK; }

                if (Input.GetButtonDown(GlobalFields.BUTTONS.ACTION.ToString()))
                { _PlayerState = PLAYER_STATE.ATTACK; }

                if (Input.GetButton(GlobalFields.BUTTONS.JUMP.ToString()))
                { _PlayerState = PLAYER_STATE.JUMP; }
                break;
            case PLAYER_STATE.WALK:
                if (move == Vector3.zero)
                { _PlayerState = PLAYER_STATE.IDLE; }

                if (Input.GetButtonDown(GlobalFields.BUTTONS.ACTION.ToString()))
                { _PlayerState = PLAYER_STATE.ATTACK; return; }

                if (Input.GetButton("Jump"))
                { _PlayerState = PLAYER_STATE.JUMP; return; }

                if (move != Vector3.zero)
                { _PlayerState = PLAYER_STATE.WALK; }

                if (_IsCollideTop && move.y > 0) { move.y = 0; }
                if (_IsCollideBottom && move.y < 0) { move.y = 0; }
                if (_IsCollideRight && move.x > 0) { move.x = 0; }
                if (_IsCollideLeft && move.x < 0) { move.x = 0; }
                break;
            case PLAYER_STATE.JUMP:
                move.y = 0;

                if (transform.position.z <= MaxJumpPower)
                { _PlayerState = PLAYER_STATE.FALLING; return; }

                move.y = (move.y + JumpVelocity) * Time.deltaTime;
                move.z = (move.z - JumpVelocity) * Time.deltaTime;
                break;
            case PLAYER_STATE.FALLING:
                if (transform.position.z < 0)
                {
                    move.y = (move.y - JumpVelocity) * Time.deltaTime;
                    move.z = (move.z + JumpVelocity) * Time.deltaTime;
                }
                else
                {
                    _PlayerState = PLAYER_STATE.IDLE;
                    move.z = 0;
                }
                break;
            case PLAYER_STATE.ATTACK:

                if (_NeedUpdateAttackAnimation)
                {
                    switch (_PlayerAttackState)
                    {
                        case PLAYER_ANIM_STATE.IDLE:
                            _PlayerAttackState = PLAYER_ANIM_STATE.PUNCH01;
                            _TimeReleaseCombo = 0;
                            break;
                        case PLAYER_ANIM_STATE.PUNCH01:
                            _PlayerAttackState = PLAYER_ANIM_STATE.PUNCH02;
                            _TimeReleaseCombo = 0;
                            break;
                        case PLAYER_ANIM_STATE.PUNCH02:
                            _PlayerAttackState = PLAYER_ANIM_STATE.PUNCH03;
                            _TimeReleaseCombo = 20;
                            break;
                    }
                    _NeedUpdateAttackAnimation = false;
                }
                else
                {
                    if (_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 
                        && !_Animator.IsInTransition(0))
                    {
                        _NeedUpdateAttackAnimation = true;
                        _PlayerState = PLAYER_STATE.IDLE;
                    }
                }

                break;
        }

        transform.Translate(move);
        ControlAttackCombo();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals(GlobalFields.MAP_TYPE.WALL.ToString()))
        {
            if (collision.otherCollider.gameObject.name.Equals(PLAYER_COLLIDERS.PlayerTop.ToString()))
            { _IsCollideTop = true; }
            if (collision.otherCollider.gameObject.name.Equals(PLAYER_COLLIDERS.PlayerBottom.ToString()))
            { _IsCollideBottom = true; }
            if (collision.otherCollider.gameObject.name.Equals(PLAYER_COLLIDERS.PlayerRight.ToString()))
            { _IsCollideRight = true; }
            if (collision.otherCollider.gameObject.name.Equals(PLAYER_COLLIDERS.PlayerLeft.ToString()))
            { _IsCollideLeft = true; }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals(GlobalFields.MAP_TYPE.WALL.ToString()))
        {
            if (collision.otherCollider.gameObject.name.Equals(PLAYER_COLLIDERS.PlayerTop.ToString()))
            { _IsCollideTop = false; }
            if (collision.otherCollider.gameObject.name.Equals(PLAYER_COLLIDERS.PlayerBottom.ToString()))
            { _IsCollideBottom = false; }
            if (collision.otherCollider.gameObject.name.Equals(PLAYER_COLLIDERS.PlayerRight.ToString()))
            { _IsCollideRight = false; }
            if (collision.otherCollider.gameObject.name.Equals(PLAYER_COLLIDERS.PlayerLeft.ToString()))
            { _IsCollideLeft = false; }
        }
    }

    private void ControlAttackCombo()
    {
        if (_PlayerState == PLAYER_STATE.IDLE
            && _PlayerAttackState != PLAYER_ANIM_STATE.IDLE)
        {
            _TimeReleaseCombo++;
            if(_TimeReleaseCombo >= 20)
            {
                _NeedUpdateAttackAnimation = true;
                _PlayerState = PLAYER_STATE.IDLE;
                _PlayerAttackState = PLAYER_ANIM_STATE.IDLE;
            }
        }
    }

    public bool IsJump()
    {
        return _PlayerState == PLAYER_STATE.JUMP;
    }
    public bool IsWalk()
    {
        return _PlayerState == PLAYER_STATE.WALK;
    }
    public bool IsIddle()
    {
        return _PlayerState == PLAYER_STATE.IDLE;
    }
    public bool IsFalling()
    {
        return _PlayerState == PLAYER_STATE.FALLING;
    }
    public bool IsPunch01()
    {
        return (_PlayerState == PLAYER_STATE.ATTACK
                 && _PlayerAttackState == PLAYER_ANIM_STATE.PUNCH01);
    }
    public bool IsPunch02()
    {
        return (_PlayerState == PLAYER_STATE.ATTACK
                 && _PlayerAttackState == PLAYER_ANIM_STATE.PUNCH02);
    }
    public bool IsPunch03()
    {
        return (_PlayerState == PLAYER_STATE.ATTACK
                 && _PlayerAttackState == PLAYER_ANIM_STATE.PUNCH03);
    }
}
