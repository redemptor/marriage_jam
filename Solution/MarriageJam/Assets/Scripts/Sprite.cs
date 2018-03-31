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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponents<Collider2D>().Where(x => !x.isTrigger).ToList();
        collider2D.AddRange(GetComponentsInChildren<Collider2D>().Where(x => !x.isTrigger).ToList());

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
        var reference = (isStatic && collider2D != null)
            ? collider2D.Select(x => x.bounds.max.y).Max()
            : (collider2D != null
                ? collider2D.Select(x => x.bounds.min.y).Min()
                : spriteRenderer.bounds.min.y);

        spriteRenderer.sortingOrder = (int)(reference * -100);
    }
}
