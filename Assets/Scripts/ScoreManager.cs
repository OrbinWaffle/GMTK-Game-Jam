using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour{
    // To update the UI whenever a point is added call
    // ScoreManager.instance.addPoint();
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI timerText;
    public GameObject gameOverObj;
    public TextMeshProUGUI finalMessage;

    private void Awake(){
        instance = this;
    }

    int ninjaScore = 0;
    int lowScore = 0;

    // Start is called before the first frame update
    void Start(){
        lowScore = PlayerPrefs.GetInt("lowScore", 0);
        scoreText.text = ninjaScore.ToString() + " Points";
        highscoreText.text = "Lowest Score: " + lowScore;
    }
    public void UpdateTimer(int timeLeft)
    {
        timerText.text = "Time Left: " + timeLeft;
    }

    public void AddPoints(int points){
        // This function increases points by 1

        ninjaScore += points;
        scoreText.text = ninjaScore.ToString() + " Points";

        if (lowScore > ninjaScore){
            lowScore = ninjaScore;
            highscoreText.text = "Lowest Score: " + lowScore;
            PlayerPrefs.SetInt("lowScore", lowScore);
        }
    }
    public void DisplayGameOver()
    {
        gameOverObj.SetActive(true);
        finalMessage.text = "The Ninja got " + ninjaScore + " points.";
    }
}
