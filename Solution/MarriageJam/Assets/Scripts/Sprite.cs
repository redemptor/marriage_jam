using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class Sprite : MonoBehaviour
{
    public bool isStatic;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        SortingOrder();
    }

    void LateUpdate()
    {
        if (!isStatic)
        {
            SortingOrder();
        }
    }

    void SortingOrder()
    {
        var reference = (isStatic && collider != null)
            ? collider.bounds.max
            : (collider != null
                ? collider.bounds.min
                : spriteRenderer.bounds.min);

        //spriteRenderer.sortingOrder = (int)(Camera.main.ViewportToWorldPoint(reference).y * -10);
        spriteRenderer.sortingOrder = (int)(reference.y * -100);
    }
}
