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
    public Text speed;

    int reqscore = GlobalVariables.baseScoreReq;
    int score = 0;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        if (GlobalVariables.currentLevel % 3 == 1)
        {
            reqscore *= 2;
        }
        else if (GlobalVariables.currentLevel % 3 == 2)
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
            /*GlobalVariables.totalLevel += 1;

            if (GlobalVariables.currentLevel == 3)
                {
                    GlobalVariables.currentLevel = 0;
                }*/
                SceneManager.LoadSceneAsync("1to2");
            }
        //Debug.Log(GlobalVariables.gamespeedMult);
        if (Input.GetKeyDown(KeyCode.L)) //debug tool so I don't have to play tetris for 20+ minutes to test a single variable.
        {
            score = reqscore;
        }
        switch (GlobalVariables.gamespeedMult)
        {
            case 0f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">";
                break;

            case 0.1f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">>";
                break;

            case 0.2f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">>>";
                break;

            case 0.3f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">>>>";
                break;

            case 0.4f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">>>>>";
                break;

            case 0.5f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">>>>>>";
                break;

            case 0.575f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">>>>>>>";
                break;

            case 0.65f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">>>>>>>>";
                break;

            case 0.7f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">>>>>>>>>";
                break;

            case 0.75f:
                speed.text = "Game Speed" + System.Environment.NewLine + ">>>>>>>>>>";
                break;


            /*default:
                speed.text = "Game Speed" + System.Environment.NewLine + "ERROR: OOPS";
                break;*/
        }
    }
    public void AddPoint(int pointValue)

    {

        score = score + pointValue;

        scoreText.text = "Current Score: " + score.ToString();

    }
}
