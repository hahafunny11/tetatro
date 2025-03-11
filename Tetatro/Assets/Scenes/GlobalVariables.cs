using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables
{
    public static int currentLevel = 0;
    public static int clearedLines = 0;
    public static float gamespeedMult = 0;
    public static int baseScoreReq = 600;
    public static int cash = 4;
    public static int timeInFrames = 0;
    public static int timeShave = 0;
    public static double baseMult = 1;
    public static double singleMult = 1;
    public static double doubleMult = 1;
    public static double tripleMult = 1;
    public static double tetrisMult = 1;

    public static HashSet<string> items = new HashSet<string>();
}
