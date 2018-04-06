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
            Debug.Log("OI" + actor.name + ">" + collision.GetComponent<Actor>().name);
            actor.Attack();
           // collision.GetComponent<Actor>().SetDamage(actor.CurrentDamage);
           // actor.SetHit(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
            //WIP
            actor.StopAttack();
            // collision.GetComponent<Actor>().SetDamage(actor.CurrentDamage);
            // actor.SetHit(true);
        }
    }
}
