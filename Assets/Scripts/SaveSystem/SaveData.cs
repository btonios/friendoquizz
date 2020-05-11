using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static string QUESTIONS_DATA_PATH = Application.persistentDataPath + "/questions.data";
    

    //Save question list in questions.data file 
    public static void SaveQuestions()
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
        
        //if no question found in questions.data, alert
        else 
        {
            Debug.LogError("ALERT: question.data FILE NOT FOUND");
        }

        return data;
    }

    public static void ResetQuestions()
    {
        File.Delete(QUESTIONS_DATA_PATH);
        GameSettings.questionList = new List<Question>();
    }

    public static void DebugData()
    {
        SaveData.ResetQuestions();
        GameSettings.setList();
        SaveData.SaveQuestions();
    }

    public static void debuglist()
    {
        string t = "";
        foreach(Question question in GameSettings.questionList)
        {
            t += question.id + ", ";
        }
        Debug.Log(t);
    }
}
