using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static Scoring instance;

    public Text reqscoreText;
    public Text scoreText;

    int reqscore = 600;
    int score = 0;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reqscoreText.text = "Target Score: " + reqscore.ToString();
        scoreText.text = "Current Score: " + score.ToString();
    }

    // Update is called once per frame
    public void AddPoint(int pointValue)

    {

        score = score + pointValue;

        scoreText.text = "Current Score: " + score.ToString();

    }
}
