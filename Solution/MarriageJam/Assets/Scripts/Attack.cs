using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
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
            collision.GetComponent<Actor>().SetDamage(actor.CurrentDamage);
            actor.SetHit(true);
        }
    }
}
