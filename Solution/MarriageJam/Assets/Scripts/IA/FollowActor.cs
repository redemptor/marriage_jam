using UnityEngine;

public class FollowActor
{
    private Actor _following;
    private Actor _follower;

    public FollowActor(Actor follower)
    {
        _follower = follower;
    }

    public void SetFollow(Actor actor)
    {
        _following = actor;
    }

    public void FixedUpdate()
    {
        if (!_follower.Alive
            || _follower.IsKnockOut
            || _follower.Stunned)
        { return; }

        if (_following == null)
        {
            _following = LevelManager.GetRandomPlayerAtScene();
        }
        else
        {
            Vector3 direction = (_following.transform.position - _follower.transform.position).normalized;

            _follower.Rigidbody2D.velocity = new Vector2(0.0001f, 0.0001f);
            _follower.Rigidbody2D.MovePosition(
                   _follower.transform.position
                   + direction
                   * ((ActionActor)_follower).moveVelocity
                   * Time.deltaTime);
        }
    }
}
