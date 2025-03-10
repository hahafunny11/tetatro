using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CashGet : MonoBehaviour
{
    public Text cashmoney;
    int finalCash = 0;
    int interest = 0;
    int intCheck = GlobalVariables.cash;
    int timeCash = 5;
    int timeModify = GlobalVariables.timeInFrames/1200;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        while (intCheck > 0)
        {
            intCheck -= 5;
            if (intCheck > 0 && interest != 10)
            {
                interest += 1;
            }
        }

        timeCash -= timeModify;
        switch (GlobalVariables.currentLevel % 3)
        {
            case 0:
                finalCash = 4;
                break;

            case 1:
                finalCash = 6;
                break;

            case 2:
                finalCash = 8;
                break;
        }
        finalCash += interest + timeCash;
        GlobalVariables.cash += finalCash;
        cashmoney.text = "Earned " + finalCash + "$";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
