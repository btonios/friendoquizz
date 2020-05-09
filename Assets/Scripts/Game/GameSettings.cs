using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static int playerNumber = 0;
    public static int lastPlayerId = 0;
    public static int maxQuestionId = -1;
    public static List<Player> playerList = new List<Player>();
    public static List<Question> questionList = SaveData.LoadQuestions();


    //get question from list with IDs
    public static Question GetQuestion(int questionId)
    {
        Question question = new Question();
        foreach(Question questionInList in questionList)
        {
            if(questionInList.id == questionId)
            {
                question = questionInList;
            }
        }
        return question;
    }

    //set maxQuestionId
    public static void SetMaxQuestionId()
    {
        foreach(Question questionInList in questionList)
        {
            if(questionInList.id > maxQuestionId)
            {
                maxQuestionId = questionInList.id;
            }
        }
    }

    public static void LoadQuestions()
    {
        GameSettings.questionList = SaveData.LoadQuestions();
        SetMaxQuestionId();
    }

    public static void setList()
    {
        questionList.Add(new Question(0, "A"));
        questionList.Add(new Question(1, "BB"));
        questionList.Add(new Question(2, "DDD"));
        questionList.Add(new Question(3, "EEEE"));
        questionList.Add(new Question(4, "FFFFF"));
        questionList.Add(new Question(5, "GGGGGG"));
        questionList.Add(new Question(6, "HHHHHHH"));
        questionList.Add(new Question(7, "IIIIIIII"));
        questionList.Add(new Question(8, "JJJJJJJJJ"));    
    }
}
