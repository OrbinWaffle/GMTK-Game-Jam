using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    [SerializeField] float time;
    [SerializeField] InputController IC;
    [SerializeField] FruitController FC;
    float timeLeft;

    void Awake()
    {
        main = this;
    }
    void Start()
    {
        timeLeft = time;
    }
    void Update()
    {
        if(timeLeft <= 0)
        {
            timeLeft = 0;
            GameOver();
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
        ScoreManager.instance.UpdateTimer((int)timeLeft);
    }
    void GameOver()
    {
        IC.OnGameOver();
        FC.OnGameOver();
        ScoreManager.instance.DisplayGameOver();
    }
}
