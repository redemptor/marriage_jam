using UnityEngine;

public class Player : MonoBehaviour
{
    enum PLAYER_STATE { IDLE, WALK, JUMP, FALLING, DIE }
    enum PLAYER_ANIM_STATE { IDLE, WALK, JUMP, PUNCH01 }
    enum PLAYER_COLLIDERS { PlayerTop, PlayerBottom, PlayerLeft, PlayerRight }

    private bool _IsCollideTop = false;
    private bool _IsCollideBottom = false;
    private bool _IsCollideRight = false;
    private bool _IsCollideLeft = false;
    private Animator _Animator;
    private PLAYER_STATE _PlayerState = PLAYER_STATE.IDLE;

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

                if (Input.GetButton("Jump"))
                { _PlayerState = PLAYER_STATE.JUMP; }
                break;
            case PLAYER_STATE.WALK:
                if (move == Vector3.zero)
                { _PlayerState = PLAYER_STATE.IDLE; }

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
        }

        transform.Translate(move);
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

}
