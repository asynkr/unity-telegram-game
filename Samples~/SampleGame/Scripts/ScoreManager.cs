using Asynkrone.UnityTelegramGame.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private ConnexionManager telegramConnexionManager;

    [SerializeField] private int score;

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void OnClickAddToScore()
    {
        score++;
    }
    public void OnClickShareScore()
    {
        telegramConnexionManager.SendScore(score);
    }
}
