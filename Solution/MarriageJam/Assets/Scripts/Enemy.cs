using UnityEngine;

public class Enemy : Actor
{
    public float MaxDieDistanceX = 2f;
    public Vector3 DieDistancePower = new Vector3(0.1f, 0f, 0);
    private Vector3 diePosition;

    public override void Update()
    {
        base.Update();

        if (!Alive)
        {
            DieAnimation();
        }
    }

    public override void Die()
    {
        base.Die();
        animator.speed = 0;
        diePosition = transform.position;
        if (DieDistancePower.y > 0) { DieDistancePower.y *= -1; }
    }

    private void DieAnimation()
    {
        Vector3 currentPosition = transform.position;

        if ((Mathf.Abs(currentPosition.x)
            > (Mathf.Abs(diePosition.x) +MaxDieDistanceX / 2))
            && DieDistancePower.y < 0)
        {
            DieDistancePower.y *= -1;
        }

        if (Mathf.Abs(currentPosition.x) > (Mathf.Abs(diePosition.x) + MaxDieDistanceX))
        {
            animator.speed = 1;
        }

        if (animator.speed == 0)
        {
            if (FacingRight)
            { currentPosition -= DieDistancePower; }
            else
            { currentPosition += DieDistancePower; }

            transform.position = currentPosition;
        }
        else
        {
            //Blink enemy
            //Delete obj;
            //hide
        }
    }
}
