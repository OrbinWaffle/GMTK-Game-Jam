using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    // To update the UI whenever a point is added call
    // ScoreManager.instance.addPoint();
    public static ScoreManager instance;
    public Text scoreText;
    public Text highscoreText;

    private void Awake()
    {
        instance = this;
    }
    int score = 0;
    int highScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        scoreText.text = score.ToString() + " Points";
        highscoreText.text = "High Score: " + highScore;
    }

    public void addPoint()
    {
        // This function increases points by 1
        score += 1;
        scoreText.text = score.ToString() + " Points";
        if (highScore < score)
            PlayerPrefs.SetInt("highScore", score);
    }
}
