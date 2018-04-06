using System.Collections.Generic;
using UnityEngine;

public class IaAttackMeleeActor : MonoBehaviour
{
    public List<string> tagTarget;

    private ActionActor actor;

    private void Start()
    {
        actor = gameObject.GetComponentInParent<ActionActor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
            actor.Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
            actor.StopAttack();
        }
    }
}
