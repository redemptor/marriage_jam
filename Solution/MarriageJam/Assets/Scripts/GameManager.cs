using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    const float offset = 0.1f;

    public GameObject MainCamera;
    public static GameManager instance;
    public static Difficulty difficulty = Difficulty.Normal;
    public static int NumPlayers = 1;

    public Player[] playersPrefabs;

    public Player[] players;
    public PlayerHUD[] huds;
    public GameState State;

    private CameraShake _cameraShake;

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

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _cameraShake = MainCamera.GetComponent<CameraShake>();

        //if (SoundManager.instance != null)
        //{
        //    SoundManager.instance.StopMusic();
        //}
    }

    public void SpawnPlayers(Vector2 position)
    {
        players = new Player[NumPlayers];

        for (int i = 0; i < NumPlayers; i++)
        {
            var player = playersPrefabs[i];
            player.joystickNumber = i + 1;

            var playerPosition = new Vector2(position.x - offset + (i * offset * 2), position.y);
            players[i] = Instantiate(player, playerPosition, Quaternion.identity);
            huds[i].SetPlayer(players[i]);
        }
    }

    public void ShowHUD(bool show)
    {
        foreach (var hud in huds)
        {
            if (hud.player != null)
            {
                hud.gameObject.SetActive(show);

            }
            else
            {
                hud.gameObject.SetActive(false);

            }

        }
    }

    public void ReloadLevel()
    {
        //players = new Player[0];
        //State = GameState.Wait;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ShakeScreen()
    {
        _cameraShake.shouldShake = true;
    }
}
