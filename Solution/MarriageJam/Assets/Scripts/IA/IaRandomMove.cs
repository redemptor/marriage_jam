using UnityEngine;

public class IaRandomMove
{
    private float MAX_TIME_RANDOM_MOVE = 3;

    private ActionActor _following;
    private ActionActor _follower;
    private bool doRandomMove = false;
    private float timeRandomMove;

    public bool DoRandomMove
    {
        get { return doRandomMove; }
    }
    public bool RandomMoveComplete
    {
        get { return timeRandomMove == 0; }
    }

    public IaRandomMove(ActionActor follower)
    {
        _follower = follower;
    }

    public void SetFollow(ActionActor actor)
    {
        _following = actor;
    }

    public void Start()
    {
        if (DoRandomMove) { return; }
        doRandomMove = true;

        int randomX = Random.Range(0, 3);
        if (randomX == 2) { randomX = -1; }
        int randomY = Random.Range(0, 3);
        if (randomY == 2) { randomY = -1; }

        Vector3 direction = new Vector3(
             randomX,
             randomY,
             0);
        _follower.Rigidbody2D.velocity = direction * _follower.moveVelocity * Time.deltaTime;

        timeRandomMove = Time.time + MAX_TIME_RANDOM_MOVE;
        Debug.Log("NOVO START ");
    }

    public void FixedUpdate()
    {
        if (!_follower.Alive
            || _follower.IsKnockOut
            || _follower.Stunned)
        { return; }


        if (!_follower.IsVisible)
        {
            // randomMove = false;
            // RandomMoveComplete = true;
            // timeRandomMove = 0;
        }

        if (doRandomMove)
        {
            //turn off random move
            if (Time.time > timeRandomMove)
            {
                doRandomMove = false;
                timeRandomMove = 0;
                _follower.Rigidbody2D.velocity = new Vector2(0, 0);
            }
        }
    }
}
