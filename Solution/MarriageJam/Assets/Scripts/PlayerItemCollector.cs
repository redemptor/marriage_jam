using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if (collision.CompareTag("Collectable"))
        {
            var itemHealth = collision.GetComponent<CollectableHealth>();

            if (itemHealth != null)
            {
                player.health += itemHealth.health;
            }

            var itemScore = collision.GetComponent<CollectableScore>();

            if (itemScore != null)
            {
                player.score += itemScore.score;
            }

            Destroy(collision.gameObject);
        }
    }
}
