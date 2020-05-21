﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Json;
using System.Linq;

public class NetworkManager : MonoBehaviour
{
    public const string URL = "127.0.0.1/festty/requests/";

    void Start()
    {
        GlobalVariables.SetMACAddress();
        GlobalVariables.NICKNAME = "anonymous";
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    public string PHPFile(string file)
    {
        string entireURL = URL+file+".php";
        return entireURL;
    }

    public void GetBrowserQuestions(string sort)
    {
        StartCoroutine(GetBrowserQuestions(PHPFile("browseQuestions"), sort));
    } 

    IEnumerator GetBrowserQuestions(string url, string sort)
    {   
        WWWForm form = new WWWForm();
        form.AddField("userMacAddress", GlobalVariables.MAC_ADDRESS);
        form.AddField("sort", sort);

        using (UnityWebRequest webRequest  = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                //convert from json to list<question>
                string jsonResponse = fixJson(webRequest.downloadHandler.text);
                Question[] questions = JsonHelper.FromJson<Question>(jsonResponse);

                //refresh browser question list
                GetComponent<QuestionBrowserManager>().browserQuestionList = new List<Question>();
                foreach(Question question in questions)
                {
                    GetComponent<QuestionBrowserManager>().browserQuestionList.Add(question);
                }
                GetComponent<QuestionBrowserManager>().ShowQuestions();
            }
        }
    }

    



    public void UploadQuestion(Question question)
    {
        StartCoroutine(PostBrowserQuestion(PHPFile("uploadQuestion"), question));
    }

    IEnumerator PostBrowserQuestion(string url, Question question)
    {
        WWWForm form = new WWWForm();
        form.AddField("label", question.label);
        form.AddField("userMacAddress", GlobalVariables.MAC_ADDRESS);
        form.AddField("nickname", GlobalVariables.NICKNAME);

        using (UnityWebRequest webRequest  = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Question couldn't be sent. See error: " + webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                int id;
                if(int.TryParse(webRequest.downloadHandler.text, out id))
                {
                    Debug.Log("id: " + webRequest.downloadHandler.text);
                    foreach(Question qst in GlobalVariables.questionList)
                    {
                        if(qst.id == question.id)
                        {
                            qst.setId(id);
                        }
                    }
                }
                else
                {
                    Debug.LogError("ID returned is wrong: See error: " + webRequest.downloadHandler.text);
                    GlobalVariables.DeleteQuestion(question);

                }
                
            }
        }
    }




    public void ChangeQuestionPoints(Question question)
    {
        StartCoroutine(PostChangeQuestionPoints(PHPFile("changeQuestionPoints"), question));
    }

    IEnumerator PostChangeQuestionPoints(string url, Question question)
    {
        WWWForm form = new WWWForm();
        form.AddField("questionId", question.id);
        form.AddField("userMacAddress", GlobalVariables.MAC_ADDRESS);
        form.AddField("vote", question.voteY_N);

        using (UnityWebRequest webRequest  = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Question couldn't be sent. See error: " + webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }

    
    public void DoReport(Question question)
    {
        StartCoroutine(PostDoReport(PHPFile("doReport"), question));
    }

    IEnumerator PostDoReport(string url, Question question)
    {
        WWWForm form = new WWWForm();
        form.AddField("questionId", question.id);
        form.AddField("userMacAddress", GlobalVariables.MAC_ADDRESS);
        form.AddField("publisherMAC", question.publisherMAC);

        using (UnityWebRequest webRequest  = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Couldn't report. See error: " + webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);

            }
        }
    }


}