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
    public Text bonusTime;
    public Text bonusCash;
    public Text currentCash;

    int reqscore = GlobalVariables.baseScoreReq;
    int score = 0;
    int time = 40;
    int bonus = 5;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
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
        currentCash.text = "Cash:$" + GlobalVariables.cash.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GlobalVariables.chain);
        if (GlobalVariables.timeInFrames < 12000) {
            GlobalVariables.timeInFrames += 1;
            //Debug.Log(GlobalVariables.timeInFrames);
        }

            if (score >= reqscore)
        {
            /*GlobalVariables.totalLevel += 1;

            if (GlobalVariables.currentLevel == 3)
                {
                    GlobalVariables.currentLevel = 0;
                }*/
            SceneManager.LoadSceneAsync("1to2");
            score = 0; //this is done because otherwise the above line is called twice, messing with the players cash.
                
        }
        if (GlobalVariables.timeInFrames % 60 == 0 && bonus != 0)
        {
            time -= 1;
            if (time == 0)
            {
                bonus -= 1;
                if (bonus != 0)
                {
                    time = 40;
                }
            }
            switch (time)
            {
                default:
                    bonusTime.text = time.ToString();
                    break;

                case 0:
                    bonusTime.text = "  NO BONUS";
                    break;
            }
            
            switch (bonus)
            {
                case 5:
                    bonusCash.text = "$$$$$";
                    break;

                case 4:
                    bonusCash.text = " $$$$";
                    break;

                case 3:
                    bonusCash.text = "  $$$";
                    break;

                case 2:
                    bonusCash.text = "   $$";
                    break;

                case 1:
                    bonusCash.text = "    $";
                    break;

                case 0:
                    bonusCash.text = "     ";
                    break;
            }
        }
        //Debug.Log(GlobalVariables.gamespeedMult);
        if (Input.GetKeyDown(KeyCode.L)) //debug tool so I don't have to play tetris for 20+ minutes to test a single variable.
        {
            score = reqscore;
        }
        if (Input.GetKeyDown(KeyCode.C)) //debug tool so I don't have to play tetris for 20+ minutes to test a single variable.
        {
            GlobalVariables.cash += 1000;
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
