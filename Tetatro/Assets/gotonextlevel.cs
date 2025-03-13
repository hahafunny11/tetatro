using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using Random = System.Random;

public class gotonextlevel : MonoBehaviour
{
    Random random = new Random();
    public int listLen = 0;
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
                GlobalVariables.sisyphusFrames = 0;
                //GlobalVariables.timeShave = 0;
                GlobalVariables.finalCountdown = 3;
                GlobalVariables.baseMult = 1;
                GlobalVariables.singleMult = 1;
                GlobalVariables.doubleMult = 1;
                GlobalVariables.tripleMult = 1;
                GlobalVariables.tetrisMult = 1;
                GlobalVariables.chain = 1;
                GlobalVariables.sChain = 1;
                GlobalVariables.currentBoss = "Hermes";
                GlobalVariables.items.Clear();
                GlobalVariables.availableShopItems.Clear();
                GlobalVariables.bossGeneric.Clear();
                GlobalVariables.bossFinal.Clear();
                GlobalVariables.availableShopItems = GlobalVariables.shopItems.ToList();
                GlobalVariables.bossGeneric.Add("Poseidon, Aphrodite");
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
                Debug.Log(GlobalVariables.currentLevel + 1);
                GlobalVariables.timeInFrames = 0;
                GlobalVariables.sisyphusFrames = 0;
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
                if (GlobalVariables.currentLevel % 3 == 0)
                {
                    if (GlobalVariables.currentBoss == "Hermes")
                    {
                        GlobalVariables.currentBoss = "Poseidon";
                    }
                    else if (GlobalVariables.currentBoss == "Poseidon")
                    {
                        GlobalVariables.currentBoss = "Aphrodite";
                    }
                    else if (GlobalVariables.currentBoss == "Aphrodite")
                    {
                        GlobalVariables.currentBoss = "Sisyphus";
                    }
                    else if (GlobalVariables.currentBoss == "Sisyphus")
                    {
                        GlobalVariables.currentBoss = "Hermes";
                    }
                }
                    SceneManager.LoadSceneAsync("Tetris");
            }
        }
    }
}
