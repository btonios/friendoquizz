using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Json;
using System.Linq;
using TMPro;


public class NetworkManager : MonoBehaviour
{
    public const string URL = "ynotdevelopments.com/requests/";

    void Start()
    {
        GlobalVariables.SetMACAddress();
        GlobalVariables.NICKNAME = SaveData.LoadNickname();
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
        form.AddField("language", GlobalVariables.LANGUAGE);
        Debug.Log(GlobalVariables.LANGUAGE);

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

    



    public void UploadQuestion(GameObject questionObject)
    {
        StartCoroutine(PostBrowserQuestion(PHPFile("uploadQuestion"), questionObject));
    }

    IEnumerator PostBrowserQuestion(string url, GameObject questionObject)
    {
        Question question = questionObject.GetComponent<QuestionManager>().GetQuestionData();
        WWWForm form = new WWWForm();
        form.AddField("label", question.label);
        form.AddField("userMacAddress", GlobalVariables.MAC_ADDRESS);
        form.AddField("nickname", GlobalVariables.NICKNAME);
        form.AddField("language", GlobalVariables.LANGUAGE);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                questionObject.GetComponent<QuestionManager>().SetTextInfo(false);
                Debug.LogError("Question couldn't be sent. See error: " + webRequest.error);
            }
            else
            {
                int id;
                if(int.TryParse(webRequest.downloadHandler.text, out id))
                {
                    foreach(Question qst in GlobalVariables.questionList)
                    {
                        if(qst.id == question.id)
                        {
                            qst.setId(id);
                            questionObject.GetComponent<QuestionManager>().SetTextInfo(true);
                            SaveData.SaveQuestions();
                        }
                    }
                }
                else
                {
                    questionObject.GetComponent<QuestionManager>().SetTextInfo(false);
                    questionObject.transform.Find("textInfo").GetComponent<TMP_Text>().text = "Erreur lors de l'envoi de la question.";
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