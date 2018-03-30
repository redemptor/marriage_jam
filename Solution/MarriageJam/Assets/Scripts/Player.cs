using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _IsCollideTop = false;
    private bool _IsCollideRight = false;
    private bool _IsJump = false;
    private bool _IsFalling = false;
    private Animator _Animator;

    public int MaxJumpPower = -2;
    public int MaxMoveVelocity;
    public int MoveVelocity;
    public int JumpVelocity;

    //TODO : Remover os boleanos de estado e usar o enum
    enum PLAYER_STATE { IDLE, MOVE, JUMP, FALLING, DIE }

    void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    void Update()
    {
        _Animator.SetBool("jump", _IsJump);
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        move.x = (Input.GetAxis("Horizontal") * MoveVelocity) * Time.deltaTime;

        if (!_IsJump)
        {
            move.y = (Input.GetAxis("Vertical") * JumpVelocity) * Time.deltaTime;
        }
        else
        {
            if (transform.position.z <= MaxJumpPower)
            {
                _IsFalling = true;
            }

            if (_IsFalling)
            {
                if (transform.position.z < 0)
                {
                    move.y = (move.y - JumpVelocity) * Time.deltaTime;
                    move.z = (move.z + JumpVelocity) * Time.deltaTime;
                }
                else
                {
                    _IsFalling = false;
                    _IsJump = false;
                    move.z = 0;
                }
            }
            else
            {
                move.y = (move.y + JumpVelocity) * Time.deltaTime;
                move.z = (move.z - JumpVelocity) * Time.deltaTime;
            }
        }

        if (Input.GetButton("Jump") && !_IsJump)
        {
            _IsJump = true;
        }

        if (!_IsJump)
        {
            if (_IsCollideTop && move.y > 0)
                move.y = 0;


            if (_IsCollideRight && move.x > 0)
                move.x = 0;
        }

        transform.Translate(move);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "wall")
        {
            if (collision.otherCollider.gameObject.name.Equals("PlayerTop"))
            {
                _IsCollideTop = true;
            }
            if (collision.otherCollider.gameObject.name.Equals("PlayerRight"))
            {
                _IsCollideRight = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "wall")
        {
            if (collision.otherCollider.gameObject.name.Equals("PlayerTop"))
            {
                _IsCollideTop = false;
            }
            if (collision.otherCollider.gameObject.name.Equals("PlayerRight"))
            {
                _IsCollideRight = false;
            }
        }
    }
}
