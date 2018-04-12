using System.Collections.Generic;
using UnityEngine;

public class IaAttackRangedActor : MonoBehaviour
{
    public List<string> tagTarget;
    public float TimeToAttack = 0.5f;
    public float TimeWaitingAfterCombo = 1;
    private float timeToAttackTrigger = 0;

    private ActionActor actor;

    private void Start()
    {
        actor = gameObject.GetComponentInParent<ActionActor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
            actor.waiting = true;
            if (timeToAttackTrigger == 0)
            { timeToAttackTrigger = Time.time + TimeToAttack; }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
            var actorTarget = collision.GetComponent<Actor>();

            if (Time.time > timeToAttackTrigger && actorTarget != null && actorTarget.Alive)
            {
                actor.Attack();
                timeToAttackTrigger = Time.time + TimeToAttack;

                if (actor.hitDurations.Length > 0 && actor.ComboHit > 0)
                {
                    timeToAttackTrigger += actor.hitDurations[0];
                    if (actor.hitDurations.Length == actor.ComboHit)
                    { timeToAttackTrigger += TimeWaitingAfterCombo; }
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
            timeToAttackTrigger = 0;
            actor.waiting = false;
        }
    }
}
