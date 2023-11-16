using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cl_GameData
{
    public static int lastScore { get; set; } = 0;
    public static int highestScore { get; set; } = 0;
    public static int coins { get; set; } = 0;
    public static int currentSkin = 0;
    public static List<int> skinsOwned { get; set; } = new List<int>();
    public static int currentBlueScore { get; set; } = 0;
    public static int currentRedScore { get; set; } = 0;
    public static int gamesPlayed {  get; set; } = 0;
    public static bool isLocalMulti { get; set; } = false;


    public static void resetData()
    {
        gamesPlayed = 0;
        currentBlueScore = 0;
        currentRedScore = 0;
    }
}
