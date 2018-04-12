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
        //SoundManager.instance.PlayMusicLevel1();
        SoundManager.instance.PlayMusicEnvironment1();
        Debug.Log(GameManager.difficulty);
    }

    private void Update()
    {

        if (GameManager.instance.players.Any())
        {
            if (GameManager.instance.players.All(x => !x.Alive) && !reloading)
            {
                if (GameManager.difficulty == Difficulty.Hard)
                {
                    Invoke("ContinueScene", 3f);
                }
                else
                {
                    Invoke("ReloadLevel", 3f);
                }

                reloading = true;
            }
            else if (GameManager.instance.players.Any(x => !x.Alive) && GameManager.instance.players.Any(x => x.Alive))
            {
                Invoke("RevivePlayers", 3f);
            }
        }
    }

    public void RevivePlayers()
    {
        if (GameManager.instance.players.Any(x => !x.Alive))
        {
            foreach (var playerDead in GameManager.instance.players.Where(x => !x.Alive))
            {
                playerDead.Revive();
            }
        }
    }

    public void ReloadLevel()
    {
        reloading = false;
        GameManager.instance.ReloadLevel();
    }

    public void ContinueScene()
    {
        SceneManager.LoadScene("Continue Screen", LoadSceneMode.Single);
    }
}
