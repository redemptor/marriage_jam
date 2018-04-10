using System.Collections.Generic;
using UnityEngine;

public class AzeitonatorSafeZone : MonoBehaviour
{
    public List<string> tagTarget;

    private Azeitonator actor;

    private void Start()
    {
        actor = gameObject.GetComponentInParent<Azeitonator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
         //   actor.Escape();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {
    
        }
    }
}
