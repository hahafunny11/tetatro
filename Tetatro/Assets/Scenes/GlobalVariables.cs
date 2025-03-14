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
    public static int sisyphusFrames = 0;
    public static int finalCountdown = 3;

    //public static int timeShave = 0;
    public static double baseMult = 1;
    public static double singleMult = 1;
    public static double doubleMult = 1;
    public static double tripleMult = 1;
    public static double tetrisMult = 1;
    public static double chain = 1;
    public static double sChain = 1;

    public static HashSet<string> items = new HashSet<string>();
    public static List<string> availableShopItems = new List<string>();
    //public static List<string> stackables = new List<string>() {"Time", "Mult"};
    //public static List<string> lineStacks = new List<string>() {"All", "Single", "Double", "Triple", "Tetris"};
    public static string[] toRemove = new string[] {"null", "null", "null" };
    public static string[] currentShop = new string[] { "null", "null", "null" };
    public static int slot = 0;

    public static List<string> bossGeneric = new List<string>();
    public static List<string> bossFinal = new List<string>();

    public static string currentBoss = "";

    public static bool shopInfo = false;

    //these hashsets are for unionwiths
    public static HashSet<string> shopItems = new HashSet<string>() {"Nuke", "2Singles" ,"InARowBonus" ,"SingleChain" ,"TripleTriple" ,"MinMaxTet"};
    public static HashSet<string> nonFinalBosses = new HashSet<string>() {"Hermes", "Poseidon", "Aphrodite" };
    public static HashSet<string> finalBosses = new HashSet<string>() {"Sisyphus"};
}
