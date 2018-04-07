using UnityEngine;

public class SceneObject : Actor
{
    public GameObject[] DropItens;

    public override void Die()
    {
        if (DropItens.Length > 0)
        {
            var item = Random.Range(0, DropItens.Length);

            Instantiate(DropItens[item], transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}

