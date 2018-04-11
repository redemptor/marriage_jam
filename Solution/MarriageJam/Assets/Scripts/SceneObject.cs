using System.Threading;
using UnityEngine;

public class SceneObject : Actor
{
    public GameObject[] DropItens;

    public override void Die()
    {
        if (DropItens.Length > 0 && spriteRenderer.enabled)
        {
            var item = Random.Range(0, DropItens.Length);
            Instantiate(DropItens[item], transform.position, Quaternion.identity);
        }

        PlaySoundFXCH02(SfxDie, false);
        spriteRenderer.enabled = false;

        //Todo Delay time tem que ser conforme o tamanho do som
        Destroy(gameObject, 2);
    }
}

