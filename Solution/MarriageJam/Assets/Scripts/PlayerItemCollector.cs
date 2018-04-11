using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private Player player;
    protected AudioSource audioSource;

    void Start()
    {
        player = GetComponentInParent<Player>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            var itemHealth = collision.GetComponent<CollectableHealth>();

            if (itemHealth != null)
            {
                player.health += itemHealth.health;

                if (player.health > player.maxHealth)
                {
                    player.health = player.maxHealth;
                }

                player.PlaySoundFXCH02(itemHealth.SfxGet, false);
            }

            var itemScore = collision.GetComponent<CollectableScore>();

            if (itemScore != null)
            {
                player.score += itemScore.score;
                player.PlaySoundFXCH02(itemScore.SfxGet, false);
            }

            Destroy(collision.gameObject);
        }
    }
}
