using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Tet2")
        {
            reqscore *= 2;
        }
        else if (scene.name == "Tetboss")
        {
            reqscore *= 3;
        }
        reqscoreText.text = "Target Score: " + reqscore.ToString();
        scoreText.text = "Current Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= reqscore)
        {
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Tetris")
            {
                SceneManager.LoadSceneAsync("1to2");
            }
            else if (scene.name == "Tet2")
            {
                SceneManager.LoadSceneAsync("2to3");
            }
            else if (scene.name == "Tetboss")
            {
                SceneManager.LoadSceneAsync("3to1");
            }
        }
    }
    public void AddPoint(int pointValue)

    {

        score = score + pointValue;

        scoreText.text = "Current Score: " + score.ToString();

    }
}
