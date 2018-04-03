using UnityEngine;

public class KnockoutActor
{
    private float _maxDieDistanceX = 2f;
    private Vector3 _dieDistancePower = new Vector3(0.1f, 0f, 0);
    private Vector3 diePosition;
    private Actor _actor;

    public bool DoKnockOut
    {
        get { return _actor.Animator.speed == 0; }
    }

    public KnockoutActor(Actor actor)
    {
        _actor = actor;
    }

    public void KnockOut(Vector3 dieDistancePower, float maxDieDistanceX)
    {
        _dieDistancePower = dieDistancePower;
        _maxDieDistanceX = maxDieDistanceX;

        _actor.Animator.speed = 0;
        diePosition = _actor.transform.position;
        if (_dieDistancePower.y > 0) { _dieDistancePower.y *= -1; }
    }

    public void Update()
    {
        if (_actor.Animator.speed == 0)
        {
            Vector3 currentPosition = _actor.transform.position;

            if ((Mathf.Abs(currentPosition.x) > (Mathf.Abs(diePosition.x) + _maxDieDistanceX / 2))
                && _dieDistancePower.y < 0)
            { _dieDistancePower.y *= -1; }

            //Verify actor is finish the knockout. When finish, play animator again
            if (Mathf.Abs(currentPosition.x) > (Mathf.Abs(diePosition.x) + _maxDieDistanceX))
            { _actor.Animator.speed = 1; }

            if (_actor.Animator.speed == 0)
            {
                if (_actor.FacingRight)
                { currentPosition -= _dieDistancePower; }
                else
                { currentPosition += _dieDistancePower; }

                _actor.transform.position = currentPosition;
            }
        }
    }
}
