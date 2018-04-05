using System.Collections;
using System.Collections.Generic;
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

    public Actor GetRandomPlayerAtScene()
    {
        //LevelManager.getCurrentScene;
        //Find at scene tag player
        return null;
    }

    public void Update()
    {
        if (_following == null)
        {
            _following = GetRandomPlayerAtScene();
        }
        else
        {
            Vector3 currentPosition = _follower.transform.position;
            currentPosition.x++;
            _follower.transform.position = currentPosition;
        }
    }
}
