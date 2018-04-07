﻿using UnityEngine;

public class IaFollowActor
{
    private float MAX_TIME_RANDOM_MOVE = 4;

    private ActionActor _following;
    private ActionActor _follower;
    private bool randomMove = false;
    private float timeRandomMove;

    public IaFollowActor(ActionActor follower)
    {
        _follower = follower;
    }

    public void DoRandomMove()
    {
        if (!_follower.IsVisible)
        {
            randomMove = false;
        }

        if (timeRandomMove == 0)
        {
            timeRandomMove = Time.time + MAX_TIME_RANDOM_MOVE;
        }

        if (randomMove)
        {
            //turn off random move
            if (Time.time > timeRandomMove)
            {
                randomMove = false;
            }
        }
        else
        {
            //Try turn on
            if (Time.time > timeRandomMove)
            {
                randomMove = Random.Range(0, 2) == 1;
            }

            if (randomMove)
            {
                int randomX = Random.Range(0, 3);
                if (randomX == 2) { randomX = -1; }
                int randomY = Random.Range(0, 3);
                if (randomY == 2) { randomY = -1; }

                Vector3 direction = new Vector3(
                     randomX,
                     randomY,
                     0);
                _follower.Rigidbody2D.velocity = direction * _follower.moveVelocity * Time.deltaTime;

                timeRandomMove = 0;
            }
        }
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

        DoRandomMove();
        if (randomMove) { return; }

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
