using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static string QUESTIONS_DATA_PATH = Application.persistentDataPath + "/questions.data";
    public static string NICKNAME_DATA_PATH = Application.persistentDataPath + "/nickname.data";
    

    //Save question list in questions.data file 
    public static void SaveQuestions()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(QUESTIONS_DATA_PATH, FileMode.Create);

        List<Question> data = GlobalVariables.questionList;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Load question list from questions.data file
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

    //Save user nickname in nickname.data file 
    public static void SaveNickname()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(NICKNAME_DATA_PATH, FileMode.Create);

        string data = GlobalVariables.NICKNAME;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Load user nickname from nickname.data file
    public static string LoadNickname()
    {        
        string data = "anonymous";

        if(File.Exists(QUESTIONS_DATA_PATH))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(NICKNAME_DATA_PATH, FileMode.Open);

            data = formatter.Deserialize(stream).ToString();
            stream.Close();
        }
        
        //if no nickname found in nickname.data, alert
        else 
        {
            Debug.LogError("ALERT: nickname.data FILE NOT FOUND");
        }

        return data;
    }

    public static void ResetQuestions()
    {
        File.Delete(QUESTIONS_DATA_PATH);
        GlobalVariables.questionList = new List<Question>();
    }

    public static void DebugData()
    {
        SaveData.ResetQuestions();
        GlobalVariables.setList();
        SaveData.SaveQuestions();
    }

    
}
