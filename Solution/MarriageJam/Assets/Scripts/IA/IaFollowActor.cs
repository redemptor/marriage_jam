using UnityEngine;

public class IaFollowActor
{
    private ActionActor _following;
    private ActionActor _follower;

    public IaFollowActor(ActionActor follower)
    {
        _follower = follower;
    }

    public void SetFollow(ActionActor actor)
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
            _follower.Rigidbody2D.velocity = direction * _follower.moveVelocity * Time.deltaTime;
        }
    }
}
