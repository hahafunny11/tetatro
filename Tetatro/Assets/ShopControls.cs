using UnityEngine;
using System;
using System.Collections;
using Unity.Mathematics;
using Random = System.Random;
using System.Collections.Generic;
public class ShopControls : MonoBehaviour
{
    Random random = new Random();
    public int currentSlot = 0;
    public string[] slotsIdx = new string[] { "null", "null", "null" };
    public int[] shopCosts = new int[] { 0, 0, 0, 0 };
    public int[] magiChest = new int[] { -1, -1, -1 };
    public int tempint = 0;
    public bool inChestMenu = false;
    //public static List<string> lorb = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GlobalVariables.availableShopItems.Count > 0)
        {
            while (slotsIdx[0] == "null")
            {
                slotsIdx[0] = GlobalVariables.availableShopItems[random.Next(0, GlobalVariables.availableShopItems.Count)];
            }
        }
        switch (slotsIdx[tempint])
        {
            case "Nuke":
                shopCosts[tempint] = 100;
                break;

            case "2Singles":
                shopCosts[tempint] = 10;
                break;

            case "InARowBonus":
                shopCosts[tempint] = 15;
                break;

            case "SingleChain":
                shopCosts[tempint] = 20;
                break;

            case "TripleTriple":
                shopCosts[tempint] = 33;
                break;

            case "MinMaxTet":
                shopCosts[tempint] = 50;
                break;

            default:
                shopCosts[tempint] = 0;
                break;
        }
        tempint++;

        if (GlobalVariables.availableShopItems.Count > 1)
        {
            while (slotsIdx[1] == "null" && slotsIdx[1] != slotsIdx[0])
            {
                slotsIdx[1] = GlobalVariables.availableShopItems[random.Next(0, GlobalVariables.availableShopItems.Count)];
            }
        }
        switch (slotsIdx[tempint])
        {
            case "Nuke":
                shopCosts[tempint] = 100;
                break;

            case "2Singles":
                shopCosts[tempint] = 10;
                break;

            case "InARowBonus":
                shopCosts[tempint] = 15;
                break;

            case "SingleChain":
                shopCosts[tempint] = 20;
                break;

            case "TripleTriple":
                shopCosts[tempint] = 33;
                break;

            case "MinMaxTet":
                shopCosts[tempint] = 50;
                break;

            default:
                shopCosts[tempint] = 0;
                break;
        }
        tempint++;
        if (GlobalVariables.availableShopItems.Count > 2)
        {
            while (slotsIdx[2] == "null" && slotsIdx[2] != slotsIdx[0] && slotsIdx[2] != slotsIdx[1])
            {
                slotsIdx[2] = GlobalVariables.availableShopItems[random.Next(0, GlobalVariables.availableShopItems.Count)];
            }
        }
        switch (slotsIdx[tempint])
        {
            case "Nuke":
                shopCosts[tempint] = 100;
                break;

            case "2Singles":
                shopCosts[tempint] = 10;
                break;

            case "InARowBonus":
                shopCosts[tempint] = 15;
                break;

            case "SingleChain":
                shopCosts[tempint] = 20;
                break;

            case "TripleTriple":
                shopCosts[tempint] = 33;
                break;

            case "MinMaxTet":
                shopCosts[tempint] = 50;
                break;

            default:
                shopCosts[tempint] = 0;
                break;
        }
        tempint++;
        shopCosts[tempint] = 5;
    }

    // Update is called once per frame
    void Update()
    {
        GlobalVariables.currentShop[0] = slotsIdx[0];
        GlobalVariables.currentShop[1] = slotsIdx[1];
        GlobalVariables.currentShop[2] = slotsIdx[2];
        /*Debug.Log(GlobalVariables.gamespeedMult);
        if (GlobalVariables.cash >= shopCosts[currentSlot] && currentSlot != 3)
        {
            Debug.Log(slotsIdx[currentSlot] + ", " + currentSlot);
        }*/
        if (Input.GetKeyDown(KeyCode.W))
        {
            GlobalVariables.shopInfo = !GlobalVariables.shopInfo;
        }
            if (Input.GetKeyDown(KeyCode.A))
        {
            currentSlot -= 1;
            if (currentSlot < 0)
            {
                currentSlot = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentSlot += 1;
            if (currentSlot > 3)
            {
                currentSlot = 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shopCosts[currentSlot] != 0)
            {
                if (currentSlot != 3)
                {
                    if (GlobalVariables.cash >= shopCosts[currentSlot])
                    {
                        GlobalVariables.cash -= shopCosts[currentSlot];
                        shopCosts[currentSlot] = 0;
                        GlobalVariables.items.Add(slotsIdx[currentSlot]);
                        GlobalVariables.toRemove[currentSlot] = slotsIdx[currentSlot];
                        slotsIdx[currentSlot] = "null";
                    }
                }
                else
                {
                    if (GlobalVariables.cash >= shopCosts[currentSlot])
                    {
                        if (GlobalVariables.gamespeedMult == 0)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                magiChest[i] = random.Next(1, 6);
                                if (i == 1)
                                {
                                    if (magiChest[i] == magiChest[0])
                                    {
                                        while (magiChest[i] == magiChest[0])
                                        {
                                            magiChest[i] = random.Next(1, 6);
                                        }
                                    }
                                }
                                else if (i == 2)
                                {
                                    if (magiChest[i] == magiChest[0] || magiChest[i] == magiChest[1])
                                    {
                                        while (magiChest[i] == magiChest[0] || magiChest[i] == magiChest[1])
                                        {
                                            magiChest[i] = random.Next(1, 6);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (magiChest[0] != 0 && magiChest[1] != 0 && magiChest[2] != 0)
                                {
                                    magiChest[i] = random.Next(0, 2);
                                    if (magiChest[i] == 1)
                                    {
                                        magiChest[i] = random.Next(1, 6);
                                        if (i == 1)
                                        {
                                            if (magiChest[i] == magiChest[0])
                                            {
                                                while (magiChest[i] == magiChest[0])
                                                {
                                                    magiChest[i] = random.Next(1, 6);
                                                }
                                            }
                                        }
                                        else if (i == 2)
                                        {
                                            if (magiChest[i] == magiChest[0] || magiChest[i] == magiChest[1])
                                            {
                                                while (magiChest[i] == magiChest[0] || magiChest[i] == magiChest[1])
                                                {
                                                    magiChest[i] = random.Next(1, 6);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    magiChest[i] = random.Next(1, 6);
                                    if (i == 1)
                                    {
                                        if (magiChest[i] == magiChest[0])
                                        {
                                            while (magiChest[i] == magiChest[0])
                                            {
                                                magiChest[i] = random.Next(1, 6);
                                            }
                                        }
                                    }
                                    else if (i == 2)
                                    {
                                        if (magiChest[i] == magiChest[0] || magiChest[i] == magiChest[1])
                                        {
                                            while (magiChest[i] == magiChest[0] || magiChest[i] == magiChest[1])
                                            {
                                                magiChest[i] = random.Next(1, 6);
                                            }
                                        }
                                    }
                                }
                                
                            }
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            switch (magiChest[i])
                            {
                                case 0:
                                    if (GlobalVariables.gamespeedMult > .65f)
                                    {
                                        GlobalVariables.gamespeedMult -= .05f;
                                    }
                                    else if (GlobalVariables.gamespeedMult > .5f)
                                    {
                                        GlobalVariables.gamespeedMult -= .075f;
                                    }
                                    else if (GlobalVariables.gamespeedMult > 0f)
                                    {
                                        GlobalVariables.gamespeedMult -= .1f;
                                    }
                                    GlobalVariables.gamespeedMult *= 100;
                                    Math.Round(GlobalVariables.gamespeedMult);
                                    GlobalVariables.gamespeedMult /= 100;
                                    if (GlobalVariables.gamespeedMult < .1f)
                                    {
                                        GlobalVariables.gamespeedMult = 0;
                                    }
                                    break;

                                case 1:
                                    GlobalVariables.baseMult += .2;
                                    break;

                                case 2:
                                    GlobalVariables.singleMult += .5;
                                    break;

                                case 3:
                                    GlobalVariables.doubleMult += .7;
                                    break;

                                case 4:
                                    GlobalVariables.tripleMult += 1;
                                    break;

                                case 5:
                                    GlobalVariables.tetrisMult += 1.5;
                                    break;
                            }
                        }
                        
                    }
                }

            }
        }
    }
}
