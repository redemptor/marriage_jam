using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player[] players;
    public PlayerHUD[] huds;

    private bool reloading;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //if (SoundManager.instance != null)
        //{
        //    SoundManager.instance.StopMusic();
        //}
    }

    void Update()
    {
        if (players.All(x => !x.Alive) && !reloading)
        {
            reloading = true;
            Invoke("ReloadLevel", 3f);
        }
    }

    public void ReloadLevel()
    {
        reloading = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
