using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlobalVariables
{
    public static string MAC_ADDRESS;
    public static string NICKNAME;
    
    public static List<Question> questionList = new List<Question>();

    public static string Md5(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);
    
        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);
    
        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";
    
        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }
    
        return hashString.PadLeft(32, '0');
    }

    public static void SetMACAddress()
    {
        MAC_ADDRESS = Md5(SystemInfo.deviceUniqueIdentifier);
    }

    public void SetDefaultSettings()
    {
        SetQuestionList();
    }


    //get question from list with given index
    public static Question GetQuestion(int index)
    {
        Question newQuestion = questionList[index];
        return newQuestion;
    }

    public static void SetQuestionList()
    {
        questionList = new List<Question>();
        questionList = SaveData.LoadQuestions();
    }

    public static void SetQuestion(Question question)
    {
        if(questionList.Any(q=>q.id == question.id))
        {
            foreach(Question questionInList in questionList)
            {
                if(questionInList.id == question.id) questionInList.label = question.label;
            }
        }
        else
        {
            questionList.Add(question);
        }
        SaveData.SaveQuestions();
    }

    public static bool DeleteQuestion(Question question)
    {
        bool isDeleted = false;
        for(int i = questionList.Count - 1; i > -1; i--)
        {
            if(questionList[i].id == question.id)
            {
                questionList.RemoveAt(i);
                isDeleted = true;
            }
        }
        SaveData.SaveQuestions();
        return isDeleted;
    }

    //DEBUG
    public static void setList()
    {
        questionList.Add(new Question(0, "As-tu déjà fumé?"));
        questionList.Add(new Question(1, "Aimes-tu lire des livres?"));
        questionList.Add(new Question(2, "As-tu déjà embarqué dans un avion?"));
        questionList.Add(new Question(3, "As-tu un prénom composé?"));
        questionList.Add(new Question(4, "T'es-tu déjà fait piquer par une guêpe?"));
        questionList.Add(new Question(5, "As-tu déjà été bourré?"));

        SaveData.SaveQuestions();
    }
}
