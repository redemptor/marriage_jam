using UnityEngine;

public class IaRangedFollowActor
{
    private float MAX_TIME_RANDOM_MOVE = 4;

    private ActionActor _following;
    private ActionActor _follower;
    private Vector3 _distance;

    public IaRangedFollowActor(ActionActor follower, Vector3 distance)
    {
        _follower = follower;
        _distance = distance;
    }

    public void SetFollow(ActionActor actor)
    {
        _following = actor;
    }

    public void FixedUpdate()
    {
        if (!_follower.Alive
            || _follower.IsKnockOut
            || _follower.Stunned
            || _follower.waiting)
        { return; }

        if (_following == null || !_following.Alive)
        {
            _following = LevelManager.GetRandomPlayerAtScene();
        }
        else
        {
            Vector3 direction = (_following.transform.position - _follower.transform.position).normalized;

            if (direction.x < 0) { direction = (_following.transform.position - _follower.transform.position + _distance).normalized; }
            else if (direction.x > 0) { direction = (_following.transform.position - _follower.transform.position - _distance).normalized; }
            _follower.Rigidbody2D.velocity = direction * _follower.moveVelocity * Time.deltaTime;
        }
    }
}
