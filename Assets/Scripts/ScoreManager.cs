using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text highScoreText;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString() + " Points";
        highScoreText.text = "Highscore: " + highscore.ToString();
    }

    public void AddPoint()
    {
        this.score += 10;
        scoreText.text = score.ToString() + " Points";
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void AddExtraPoint()
    {
        this.score += 50;
        scoreText.text = score.ToString() + " Points";
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
}
