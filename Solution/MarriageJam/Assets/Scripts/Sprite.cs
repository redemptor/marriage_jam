using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class Sprite : MonoBehaviour
{
    public bool isStatic;

    private SpriteRenderer spriteRenderer;
    private new List<Collider2D> collider2D;
    private ShakeSprite _shakeSprite;
    public bool facingRight = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponents<Collider2D>().Where(x => !x.isTrigger).ToList();
        collider2D.AddRange(GetComponentsInChildren<Collider2D>().Where(x => !x.isTrigger).ToList());

        _shakeSprite = new ShakeSprite(spriteRenderer);

        SortingOrder();
    }

    void LateUpdate()
    {
        if (!isStatic)
        {
            SortingOrder();
        }
    }

    void FixedUpdate()
    {
        _shakeSprite.FixedUpdate();
    }

    void SortingOrder()
    {
        var reference = spriteRenderer.bounds.min;

        spriteRenderer.sortingOrder = (int)(reference.y * -100);
    }

    public void Shake()
    {
        _shakeSprite.Shake(2, facingRight, 0.2f);
    }
}
