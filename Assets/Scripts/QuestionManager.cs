using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class QuestionManager : MonoBehaviour
{
    public int id;
    public string label;
    public int points;
    public string status;
    public string language;
    public string publisherMAC;
    public string nickname;
    public string date;
    public int voteY_N;
    public bool used;
    public bool downloaded;

    public GameObject imageLoading;
    public GameObject imageUpload;
    public TMP_InputField textQuestion;
    public TMP_Text textInfos;
    public GameObject editQuestion;
    public Button buttonUpload;

    public void Start()
    {
        buttonUpload.onClick.AddListener(() => UploadQuestion());
    }

    public Question GetQuestionData()
    {
        return new Question(this.id, this.label, this.points , this.status,  this.language , this.publisherMAC, this.nickname, this.date, this.voteY_N, this.used , this.downloaded);
    }


    public void SetQuestionData(Question data)
    {
        this.id = data.id;
        this.label = data.label;
        this.points = data.points;
        this.status = data.status;
        this.language = data.language;
        this.publisherMAC = data.publisherMAC;
        this.nickname = data.nickname;
        this.date = data.date;
        this.used = data.used;
        this.downloaded = data.downloaded;

        textQuestion.text = this.label;
        
    }

    public void StartEditQuestion()
    {   
        editQuestion.SetActive(false);
        textQuestion.Select(); 
        textQuestion.ActivateInputField();
    }

    public void EndEditQuestion()
    {
        this.label = textQuestion.text;
        GlobalVariables.SetQuestion(GetQuestionData());
        editQuestion.SetActive(true);
    }
    
    public void DeleteQuestion()
    {
        GlobalVariables.DeleteQuestion(GetQuestionData());
        Destroy(gameObject);
    }

    public void UploadQuestion()
    {
        if(CheckQuestionFormat() == true)
        {
            buttonUpload.interactable = false;
            gameObject.GetComponent<Image>().color = new Color32(50, 50, 50, 200);
            imageLoading.SetActive(true);
            imageUpload.SetActive(false);
            GameObject.Find("MenuManager").GetComponent<NetworkManager>().UploadQuestion(gameObject);
        }
        else
        {
            textInfos.GetComponent<TMP_Text>().text = "Le format de question n'est pas respecté.";
            textInfos.GetComponent<TMP_Text>().color = new Color32(179,58,58, 255);
        }
    }
    
    public void SetTextInfo(bool sent)
    {
        if(sent == true)
        {
            textInfos.GetComponent<TMP_Text>().text = "La question a été envoyée.";
            textInfos.GetComponent<TMP_Text>().color = new Color32(119, 173, 127, 255);
        }
        else
        {
            textInfos.GetComponent<TMP_Text>().text = "Erreur lors de l'envoi de la question.";
            textInfos.GetComponent<TMP_Text>().color = new Color32(179,58,58, 255);
        }
        
        imageLoading.SetActive(false);
        imageUpload.SetActive(true);
    }


    bool CheckQuestionFormat()
    {
        int countRiskyWords = 0;
        bool ret = true;
        string questionLabel = label.ToLower();
        string[] riskyWords = new string[] {"fuck", "putain", "merde", "connard", "con", "pd", "pute", "couille", "fucking", "salope", "enculé", "enculer", "merdé", "enculée", };
        string[] badWords = new string[] {"", "", ""};
        string[] labelWords = questionLabel.Split(' ');

        foreach(string word in labelWords)

        //check if word >10 or <99 characters long
        if(label.Length < 10 || label.Length > 99)
            ret = false;

        foreach (var word in labelWords)
        {
            //check if word is a badword
            foreach(string badWord in badWords)
            {
                if(word == badWord)
                    return false;
            }

            //allow only 2 risky words
            foreach(string riskyWord in riskyWords)
            {
                if(word == riskyWord)
                    countRiskyWords++;
            }
            
        }
        if(countRiskyWords > 2)
                ret = false;

        //check for risky words in short questions
        if(labelWords.Length <= 2)
        {
            foreach (string word in labelWords)
            {
                //check if word is a badword
                foreach(string riskyWord in riskyWords)
                {
                    if(word == riskyWord)
                        return false;
                }
            }
        }

        if(labelWords.Length < 3)
            ret = false;

        return ret;
    }



}
