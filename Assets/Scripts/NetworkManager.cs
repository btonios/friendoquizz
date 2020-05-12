using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Json;
using System.Linq;

public class NetworkManager : MonoBehaviour
{
    public const string URL = "127.0.0.1/friendoquizz/";


    public string PHPFile(string file, string args="")
    {
        string entireURL = URL+file+".php"+args;
        return entireURL;
    }

    IEnumerator GetBrowserQuestions(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(PHPFile("browseQuestions")))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                string jsonResponse = fixJson(webRequest.downloadHandler.text);
                Question[] questions = JsonHelper.FromJson<Question>(jsonResponse);

                //convert from Array to List
                List<Question> retQuestions = new List<Question>();
                foreach(Question question in questions)
                {
                    GetComponent<QuestionBrowserManager>().browserQuestionList.Add(question);
                }
                GetComponent<QuestionBrowserManager>().ShowQuestions();
            }
        }
    }

    public void GetBrowserQuestions()
    {
        StartCoroutine(GetBrowserQuestions(PHPFile("getQuestions")));
    } 

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

}