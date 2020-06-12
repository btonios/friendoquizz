using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static string QUESTIONS_DATA_PATH = Path.Combine(Application.persistentDataPath,"questions.data");
    public static string NICKNAME_DATA_PATH = Path.Combine(Application.persistentDataPath,"nickname.data");
    public static string GAMESETTINGS_DATA_PATH = Path.Combine(Application.persistentDataPath,"gamesettings.data");
    

    //Save question list in questions.data file 
    public static void SaveQuestions()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(QUESTIONS_DATA_PATH, FileMode.Create);

        List<Question> data = GlobalVariables.questionList;

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.LogWarning("Questions saved");
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
            Debug.LogWarning("<color=yellow><b>ALERT: question.data FILE NOT FOUND</b></color>");
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

        if(File.Exists(NICKNAME_DATA_PATH))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(NICKNAME_DATA_PATH, FileMode.Open);

            if(stream.Length != 0)
                data = formatter.Deserialize(stream).ToString();
                
            stream.Close();
        }
        
        //if no nickname found in nickname.data, alert
        else 
        {
            Debug.LogWarning("<color=yellow><b>ALERT: nickname.data FILE NOT FOUND</b></color>");
        }

        return data;
    }





    //Save game settings in gamesettings.data file
    public static void SaveGameSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(GAMESETTINGS_DATA_PATH, FileMode.Create);

        Dictionary<string, dynamic> data = GameSettings.GetActualSettings();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //Load user nickname from gamesettings.data file
    public static Dictionary<string, dynamic> LoadGameSettings()
    {        
        Dictionary<string, dynamic> data = null;

        if(File.Exists(GAMESETTINGS_DATA_PATH))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(GAMESETTINGS_DATA_PATH, FileMode.Open);

            if(stream.Length != 0)
                data = formatter.Deserialize(stream) as Dictionary<string, dynamic>;
                
            stream.Close();
        }
        
        //if no nickname found in nickname.data, alert
        else 
        {
            Debug.LogWarning("<color=yellow><b>ALERT: gamesettings.data FILE NOT FOUND</b></color>");
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

    //DELETE EVERY .data FILES!
    public static void DeleteSaves()
    {
        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath, "*.data", SearchOption.AllDirectories);
        foreach( string filepath in files)
        {
            System.IO.File.Delete(filepath);
        }
    }
}
