using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public List<string> tagTarget;

    private ActionActor actor;
    private BoxCollider2D trigger;

    private void Start()
    {
        actor = gameObject.GetComponentInParent<ActionActor>();
        trigger = gameObject.GetComponentInParent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
            collision.GetComponent<Actor>().SetDamage(actor.GetCurrentDamage());
            actor.SetHit(true);
        }
    }
}
