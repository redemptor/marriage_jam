using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public List<string> tagTarget;

    private Actor actor;
    private BoxCollider2D trigger;

    private void Start()
    {
        actor = gameObject.GetComponentInParent<Actor>();
        trigger = gameObject.GetComponentInParent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag))
        {
            collision.GetComponent<Enemy>().SetDamage(damage);
            actor.SetHit(true);
            //trigger.gameObject.SetActive(false);
        }
    }
}
