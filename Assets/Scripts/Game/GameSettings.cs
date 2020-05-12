using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GameSettings
{
    public static int gameQuestionNumber = 10;
    public static int gameQuestionCount = 0;
    public static string gameStatus = "menu";
    public static bool useTimer = false;
    public static float timer = 30;
    public static int playerNumber = 0;
    public static int lastPlayerId = 0;
    public static List<Player> playerList = new List<Player>();

    public static void SetDefaultSettings()
    {
        gameQuestionNumber = 10;
        gameQuestionCount = 0;
        gameStatus = "menu";
        playerNumber = 0;
        lastPlayerId = 0;
        playerList = new List<Player>();
    }

    

    

    //changes value if sum is < to max and > to min values
    //returns bool: false -> couldn't change value
    //              true  -> value has been changed
    public static bool ChangeTimerValue(float value)
    {
        bool canChange = false;
        if((timer+value) <= 60 && (timer+value) >= 10)
        {
            timer += value; 
            canChange = true; 
        }

        return canChange;
    }

    //changes value if sum is < to max and > to min values
    //returns bool: false -> couldn't change value
    //              true  -> value has been changed
    public static bool ChangeQuestionNumber(int value)
    {
        bool canChange = false;
        if((gameQuestionNumber+value) <= 100 && (gameQuestionNumber+value) >= 1)
        {
            gameQuestionNumber += value; 
            canChange = true; 
        }

        return canChange;
    }

    


   

    
}
