using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class gotonextlevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //code for going between levels (will need to be updated
        {
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "title")
            {
                GlobalVariables.currentLevel = 0;
                GlobalVariables.clearedLines = 0;
                GlobalVariables.gamespeedMult = 0;
                GlobalVariables.baseScoreReq = 600;
                GlobalVariables.cash = 4;
                GlobalVariables.timeInFrames = 0;
                //GlobalVariables.timeShave = 0;
                GlobalVariables.bossCountdown = 3;
                GlobalVariables.baseMult = 1;
                GlobalVariables.singleMult = 1;
                GlobalVariables.doubleMult = 1;
                GlobalVariables.tripleMult = 1;
                GlobalVariables.tetrisMult = 1;
                GlobalVariables.chain = 1;
                GlobalVariables.sChain = 1;
                GlobalVariables.currentBoss = "";
                GlobalVariables.items.Clear();
                GlobalVariables.availableShopItems.Clear();
                GlobalVariables.bossGeneric.Clear();
                GlobalVariables.bossFinal.Clear();
                GlobalVariables.availableShopItems = GlobalVariables.shopItems.ToList();
                GlobalVariables.bossGeneric.Add("Hermes");
                GlobalVariables.toRemove[0] = "null";
                GlobalVariables.toRemove[1] = "null";
                GlobalVariables.toRemove[2] = "null";
                SceneManager.LoadSceneAsync("Tetris");
                

            }
            else if (scene.name == "1to2")
            {
                SceneManager.LoadSceneAsync("daShop");
            }
            else if (scene.name == "GameOver")
            {
                SceneManager.LoadSceneAsync("title");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "daShop")
            {
                GlobalVariables.chain = 1;
                GlobalVariables.sChain = 1;
                GlobalVariables.currentLevel += 1;
                GlobalVariables.timeInFrames = 0;
                if (GlobalVariables.toRemove[0] != "null")
                {
                    GlobalVariables.availableShopItems.Remove(GlobalVariables.toRemove[0]);
                }
                if (GlobalVariables.toRemove[1] != "null")
                {
                    GlobalVariables.availableShopItems.Remove(GlobalVariables.toRemove[1]);
                }
                if (GlobalVariables.toRemove[2] != "null")
                {
                    GlobalVariables.availableShopItems.Remove(GlobalVariables.toRemove[2]);
                }
                GlobalVariables.toRemove[0] = "null";
                GlobalVariables.toRemove[1] = "null";
                GlobalVariables.toRemove[2] = "null";
                if (GlobalVariables.currentLevel % 3 == 0 && GlobalVariables.currentLevel != 0)
                {
                    GlobalVariables.baseScoreReq += 1400 * (GlobalVariables.currentLevel - 2) * (((GlobalVariables.currentLevel - 3) / 12) + 1);
                    if (GlobalVariables.currentLevel > 8)
                    {
                        GlobalVariables.baseScoreReq *= (GlobalVariables.currentLevel / 3);
                    }
                }

                SceneManager.LoadSceneAsync("Tetris");
            }
        }
    }
}
