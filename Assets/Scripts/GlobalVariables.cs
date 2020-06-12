using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class GlobalVariables
{
    public static string MAC_ADDRESS;
    public static string NICKNAME;
    public static string APP_VERSION = "1.0";
    public static string LANGUAGE = "french";
    public static bool firstOpening = true;
    
    public static List<Question> questionList = new List<Question>();
    public static List<Question> nativeQuestionList = new List<Question>();
    public static List<string> userInfos = new List<string>();

    
    //gives to MAC_ADDRESS md5 hashed mac address
    public static void SetMACAddress()
    {
        MAC_ADDRESS = SystemInfo.deviceUniqueIdentifier;
    }

    public void SetDefaultSettings()
    {
        SetQuestionLists();
    }

    public static List<Question> GetQuestionLists()
    {
        List<Question> list = LoadNativeQuestions().Union<Question>(questionList).ToList<Question>();
        return list;
    }

    //get question from list with given index
    public static Question GetQuestion(int id)
    {   
        Question newQuestion = new Question();
        foreach(Question question in questionList)
            if(question.id == id)
                newQuestion = question;
        
        return newQuestion;
    }

    //set question list with native questions and user questions
    public static void SetQuestionLists()
    {
        questionList = new List<Question>();
        questionList = SaveData.LoadQuestions();

        nativeQuestionList = new List<Question>();
        nativeQuestionList = LoadNativeQuestions();
    }


    //add or update question in list
    public static void SetQuestion(Question question)
    {
        if(questionList.Any(q=>q.id == question.id))
        {
            foreach(Question questionInList in questionList)
            {
                if(questionInList.id == question.id) questionInList.setQuestion(question);
            }
        }
        else
        {
            questionList.Add(question);
        }
        SaveData.SaveQuestions();
    }


    //DELETE QUESTION
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

    public static void SetNickname(string nick)
    {
        NICKNAME = nick;
        SaveData.SaveNickname();
    }

    //DEBUG
    public static void setList()
    {
        questionList.Add(new Question(0,"Est-ce que c'est une question ?"));
        questionList.Add(new Question(0,"As-tu déjà été bourré ?"));
        questionList.Add(new Question(0,"Es-tu en couple ?"));
        questionList.Add(new Question(0,"Es-tu célibataire ?"));
        questionList.Add(new Question(0,"As-tu une teinture ?"));
        questionList.Add(new Question(0,"As-tu un tatouage ?"));
        questionList.Add(new Question(0,"As-tu déjà voyagé en dehors de ton pays ?"));
        questionList.Add(new Question(0,"As-tu déjà vomi à cause de l'alcool ?"));
        questionList.Add(new Question(0,"As-tu un Samsung ?"));
        questionList.Add(new Question(0,"As-tu un Iphone ?"));
        questionList.Add(new Question(0,"T'es-tu déjà rasé(e) la tête ?"));
        questionList.Add(new Question(0,"As-tu déjà contacté un marabout ?"));
        questionList.Add(new Question(0,"T'es-tu déjà endormi au cinéma ?"));
        questionList.Add(new Question(0,"As-tu déjà mangé dans un restaurant horrible ?"));
        questionList.Add(new Question(0,"As-tu les yeux marrons ?"));
        questionList.Add(new Question(0,"As-tu les cheveux bruns/chatains ?"));
        questionList.Add(new Question(0,"As-tu les cheveux blonds ?"));
        questionList.Add(new Question(0,"As-tu les cheveux roux ?"));
        questionList.Add(new Question(0,"As-tu une faiblesse pour le rhum ?"));
        questionList.Add(new Question(0,"Aimes-tu la Vodka ?"));
        questionList.Add(new Question(0,"Aimes-tu le vin blanc ?"));
        questionList.Add(new Question(0,"Aimes-tu le vin rouge ?"));
        questionList.Add(new Question(0,"Aimes-tu le champagne ?"));
        questionList.Add(new Question(0,"Es-tu végétarien(ne) ?"));
        questionList.Add(new Question(0,"T'es-tu déjà teint les cheveux ?"));
        questionList.Add(new Question(0,"Utilises-tu le terme Chocolatine ?"));
        questionList.Add(new Question(0,"Es-tu déjà tombé en public ?"));

        SaveData.SaveQuestions();
    }

    public static void debugQuestionList()
    {
        string t = "";
        foreach(Question question in questionList)
        {
            t += question.id + " | "+question.label+"\n";
        }
        Debug.Log(t);
        Debug.Log("Question number: " + questionList.Count);
    }

    public static void debugNativeQuestionList()
    {
        string t = "";
        foreach(Question question in nativeQuestionList)
        {
            t += question.id + " | "+question.label+"\n";
        }
        Debug.Log(t);
        Debug.Log("Question number: " + nativeQuestionList.Count);
    }

    public static void debugBothQuestionLists()
    {
        string t = "";
        foreach(Question question in GetQuestionLists())
        {
            t += question.id + " | "+question.label+"\n";
        }
        Debug.Log(t);
        Debug.Log("Question number: " + GetQuestionLists().Count);
    }


    //load native questions from questions.txt
    //0: id
    //1: label
    //2: language
    //3: isUsed
    public static List<Question> LoadNativeQuestions()
    {
        string path = "Assets/Resources/questions.txt";
     
        string[] lines = System.IO.File.ReadAllLines(path);
        
        int id = 0;
        string label = "";
        string language = "english";
        bool used = false;

        List<Question> nativeList = new List<Question>();
        
        foreach(string line in lines)
        {
            string[] questionInfos = line.Split(char.Parse("\\"));  
            id = int.Parse(questionInfos[0]);
            label = questionInfos[1];
            language = questionInfos[2];
            used = bool.Parse(questionInfos[3]);

            Question question = new Question(id, label, 0, "native", language, null, null, null, 0, false, used);
            nativeList.Add(question);
        }
        nativeQuestionList = nativeList;
        return nativeList;
        Debug.Log("Native list loaded");

    }

    public static void SetNativeQuestionUsed(Question question)
    {
        string path = "Assets/Resources/questions.txt";
        string[] lines = File.ReadAllLines(path);
        int count = 0;
        foreach(string line in lines)
        {
            string[] lineInfos = line.Split(char.Parse("\\"));
            if(lineInfos[0] == question.id.ToString())
            {
                lineInfos[3] = lineInfos[3].Replace("false", "true");
                lines[count] = lineInfos[0]+"\\"+lineInfos[1]+"\\"+lineInfos[2]+"\\"+lineInfos[3];
            }
            count++;
        }
        File.WriteAllLines(path, lines);

        Debug.Log("Question set to used");
    }

    public static void SetNativeQuestionsUnused()
    {
        string path = "Assets/Resources/questions.txt";
        string[] lines = File.ReadAllLines(path);
        int count = 0;
        foreach(string line in lines)
        {
            string[] lineInfos = line.Split(char.Parse("\\"));
            lineInfos[3] = lineInfos[3].Replace("true", "false");
            lines[count] = lineInfos[0]+"\\"+lineInfos[1]+"\\"+lineInfos[2]+"\\"+lineInfos[3];
            count++;
        }
        File.WriteAllLines(path, lines);
        Debug.Log("Native list set to unused");
    }
}
