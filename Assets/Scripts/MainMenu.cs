using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    void Start()
    {
        UpdateScore();
    }
    public void ResetScore()
    {
        PlayerPrefs.SetInt("lowScore", 0);
        UpdateScore();
    }
    public void UpdateScore()
    {
        scoreText.text = "Lowest Score: " + PlayerPrefs.GetInt("lowScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame(){
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
