using System.Collections.Generic;
using UnityEngine;

public class AzeitonatorBall : Actor
{
    public List<string> tagTarget;
    public float velocity = 2;
    public Damage damage;

    private Vector2 direction = new Vector2(1, 0);
    private bool reversed = false;

    public override void Start()
    {
        base.Start();

        direction = new Vector2(velocity, 0);
        if (!facingRight && direction.x > 0)
        {
            direction.x *= -1;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Rigidbody2D.velocity = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tagTarget.Contains(collision.tag) && collision.isTrigger)
        {

            if (collision.CompareTag("Player") && collision.name.Equals("HitAttack"))
            {
                direction.x *= -1;
                reversed = true;
            }
            else
            {
                if (direction.x < 0)
                {
                    damage.AttackFromRight = true;
                }
                else
                {
                    damage.AttackFromRight = false;
                }

                if (reversed && collision.tag.Equals("Bullet"))
                {
                    collision.GetComponent<Actor>().Die();
                    Die();
                }
                else if (reversed || collision.tag.Equals("Player"))
                {
                    var actor = collision.GetComponent<Actor>();
                    if (actor != null)
                    {
                        actor.SetDamage(damage);
                        Die();
                    }
                }
            }
        }
    }

    public void SetSortingOrder(int sortingOrder)
    {
        spriteRenderer.sortingOrder = sortingOrder;
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Die();
    }

}
