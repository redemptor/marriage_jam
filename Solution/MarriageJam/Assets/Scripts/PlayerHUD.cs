using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Image avatar;
    public Image namePlayer;
    public Image health;
    public Image lostHealth;
    public Image[] score;
    public Sprite[] scoreNumbers;

    public Player player;
    //public Player CurrentPlayer
    //{
    //    get { return _player; }
    //    set
    //    {
    //        _player = value;
    //        UpdatePlayer();
    //    }
    //}

    private int currentHealth;
    private int currentScore;
    private float lostHealthWidth;

    void Start()
    {
        UpdatePlayer();
        lostHealthWidth = lostHealth.rectTransform.rect.width;
    }

    void Update()
    {
        UpdateHealt();
        UpdateScore();
    }

    public void SetPlayer(Player Player)
    {
        player = Player;

        UpdatePlayer();
    }

    void UpdatePlayer()
    {
        if(player != null)
        {
            avatar.sprite = player.NormalAvatar;
            namePlayer.sprite = player.NameActorSprite;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void UpdateHealt()
    {
        if (currentHealth != player.health)
        {
            if (currentHealth > player.health)
            {
                StartCoroutine(ShowHittedAvatar(1f));
            }

            var barWidth = ((player.maxHealth - player.health) / (float)player.maxHealth) * lostHealthWidth;
            if (barWidth > lostHealthWidth)
            {
                barWidth = lostHealthWidth;
            }

            lostHealth.rectTransform.sizeDelta = new Vector2(barWidth, lostHealth.rectTransform.rect.height);

            currentHealth = player.health;
        }
    }

    void UpdateScore()
    {
        if (currentScore != player.score)
        {
            var scoreList = player.score.ToString("00000").ToCharArray().Select(x => (int)char.GetNumericValue(x)).ToArray();

            for (int i = 0; i < score.Length; i++)
            {
                score[i].sprite = scoreNumbers[scoreList[i]];
            }

            currentScore = player.score;
        }
    }

    private IEnumerator ShowHittedAvatar(float waitTime)
    {
        avatar.sprite = player.HittedAvatar;
        yield return new WaitForSeconds(waitTime);
        avatar.sprite = player.NormalAvatar;
    }
}
