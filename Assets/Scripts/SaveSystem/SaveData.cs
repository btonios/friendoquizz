﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static string QUESTIONS_DATA_PATH = Application.persistentDataPath + "/questions.data";

    //Save question list in questions.data file 
    public static void SaveQuestions(List<Question> questions)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(QUESTIONS_DATA_PATH, FileMode.Create);

        List<Question> data = GameSettings.questionList;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Load question list from questions.data
    public static List<Question> LoadQuestions()
    {        
        List<Question> data = new List<Question>();

        if(File.Exists(QUESTIONS_DATA_PATH))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(QUESTIONS_DATA_PATH, FileMode.Open);

            data = formatter.Deserialize(stream) as List<Question>;
            stream.Close();
        }
        
        //if no question found in questions.data, one debug question is created in list
        else 
        {
            Debug.LogError("ALERT: question.data FILE NOT FOUND");
            Question errorQuestion = new Question(0, "NO QUESTION FOUND IN DATABASE");
            data.Add(errorQuestion);
        }

        return data;
    }

    public static void ResetQuestions()
    {
        File.Delete(QUESTIONS_DATA_PATH);
    }
}