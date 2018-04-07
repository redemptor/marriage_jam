using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player1;
    public Player player2;

    public Text healthPlayer1;
    public Text scorePlayer1;

    public Text healthPlayer2;
    public Text scorePlayer2;

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

    }

    void Update()
    {
        if (player1 != null)
        {
            healthPlayer1.text = string.Format("Health: {0}", player1.health);
            scorePlayer1.text = string.Format("Score: {0}", player1.score);
        }

        if (player2 != null)
        {
            healthPlayer2.text = string.Format("Health: {0}", player2.health);
            scorePlayer2.text = string.Format("Score: {0}", player2.score);
        }
    }
}
