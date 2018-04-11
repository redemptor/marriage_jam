using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    private bool reloading;

    void Start()
    {
        SoundManager.instance.PlayMusicEnvironment1();
        Debug.Log(GameManager.difficulty);
    }

    private void Update()
    {
        if (GameManager.instance.players.Any() && GameManager.instance.players.All(x => !x.Alive) && !reloading)
        {
            reloading = true;
            Invoke("ReloadLevel", 3f);
        }
    }

    public void ReloadLevel()
    {
        reloading = false;
        GameManager.instance.ReloadLevel();
    }
}
