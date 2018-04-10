using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class CustomSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private new List<Collider2D> collider2D;

    public bool isStatic;
    public bool IsVisible
    {
        get
        { return spriteRenderer.isVisible; }
    }

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
        var reference = spriteRenderer.bounds.min;

        spriteRenderer.sortingOrder = (int)(reference.y * -100);
    }


}