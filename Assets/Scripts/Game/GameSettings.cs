using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static List<Question> questionList = SaveData.LoadQuestions();

    public static void SetDefaultSettings()
    {
        gameQuestionNumber = 2;
        gameQuestionCount = 0;
        gameStatus = "menu";
        playerNumber = 0;
        lastPlayerId = 0;
        playerList = new List<Player>();
        questionList = SaveData.LoadQuestions();
    }

    //get question from list with given index
    public static Question GetQuestion(int index)
    {
        Question newQuestion = questionList[index];
        return newQuestion;
    }

    public static void LoadQuestions()
    {
        questionList = new List<Question>();
        questionList = SaveData.LoadQuestions();
    }

    public static void setList()
    {
        questionList.Add(new Question(0, "As-tu déjà fumé?"));
        questionList.Add(new Question(1, "Aimes-tu lire des livres?"));
        questionList.Add(new Question(2, "As-tu déjà embarqué dans un avion?"));
        questionList.Add(new Question(3, "As-tu un prénom composé?"));
        questionList.Add(new Question(4, "T'es-tu déjà fait piquer par une guêpe?"));
        questionList.Add(new Question(5, "As-tu déjà été bourré?"));

        SaveData.SaveQuestions(questionList);
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
}
