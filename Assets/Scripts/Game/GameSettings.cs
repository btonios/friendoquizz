using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public static class GameSettings
{
    public static int gameQuestionNumber = 10;
    public static int gameQuestionCount = 0;
    public static string gameStatus = "menu";
    public static int playerNumber = 0;
    public static int lastPlayerId = 0;
    public static List<Player> playerList = new List<Player>();

    //mods
    public static bool randomDrink = false;

    //changes value if sum is < to max and > to min values
    //returns bool: false -> couldn't change value
    //             true  -> value has been changed
   /* public static bool ChangeQuestionNumber(int value)
    {
        bool canChange = false;
        if((gameQuestionNumber+value) <= 100 && (gameQuestionNumber+value) >= 1)
        {
            gameQuestionNumber += value; 
            canChange = true; 
        }

        return canChange;
    }*/

    public static void ChangeRandomDrink()
    {
        randomDrink = !randomDrink;
    }

    public static Dictionary<string, dynamic> GetActualSettings()
    {
        Dictionary<string, dynamic> gameSettings = new Dictionary<string, dynamic>(); 
        gameSettings.Add("QuestionNumber",gameQuestionNumber);
        gameSettings.Add("PlayerList", playerList);
        gameSettings.Add("RandomDrink", randomDrink);
        gameSettings.Add("LastPlayerId", lastPlayerId);
        gameSettings.Add("PlayerNumber", playerNumber);
        return gameSettings;
    }

    public static void SetGameSettings(GameObject sliderQuestionNumber, GameObject toggleRandomDrink)
    {
        Dictionary<string, dynamic> gameSettings = SaveData.LoadGameSettings();

        if(gameSettings == null)
        {
            gameQuestionNumber = 10;
            playerNumber = 0;
            lastPlayerId = 0;
            randomDrink = false;
            playerList = new List<Player>();
        }
        else
        {
            gameQuestionNumber = gameSettings["QuestionNumber"];   
            playerNumber = gameSettings["PlayerNumber"];         
            lastPlayerId = gameSettings["LastPlayerId"];
            randomDrink = gameSettings["RandomDrink"];
            playerList = gameSettings["PlayerList"];

            //create player object
            foreach(Player player in playerList)
                GameObject.Find("MenuManager").GetComponent<MenuManager>().CreatePlayerCardWithObject(player);

            //toggle random drink if needed
            if(randomDrink == true)
                toggleRandomDrink.GetComponent<Toggle>().isOn = true;
            else
                toggleRandomDrink.GetComponent<Toggle>().isOn = false;
        }

        

        //set non saved default values
        gameStatus = "menu";
        gameQuestionCount = 0;

        //set question number slider 
        sliderQuestionNumber.GetComponent<Slider>().value = gameQuestionNumber/5;
    }
}
