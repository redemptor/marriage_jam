﻿using System.Collections.Generic;
using UnityEngine;

public class IaAttackMeleeActor : MonoBehaviour
{
    public List<string> tagTarget;
    public float TimeToAttack = 1;
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
            if (Time.time > timeToAttackTrigger)
            {
                actor.Attack();
                timeToAttackTrigger = Time.time + TimeToAttack;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
            timeToAttackTrigger = 0;
            actor.waiting = false;
            actor.StopAttack();
        }
    }
}
