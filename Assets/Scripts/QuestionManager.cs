using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public bool activated;
    public bool downloaded;


    public TMP_InputField textQuestion;
    public GameObject editQuestion;
    public Button buttonUpload;

    public void Start()
    {
        buttonUpload.onClick.AddListener(() => GameObject.Find("MenuManager").GetComponent<NetworkManager>().UploadQuestion(GetQuestionData()));
    }

    public Question GetQuestionData()
    {
        return new Question(this.id, this.label, this.points , this.status,  this.language , this.publisherMAC, this.nickname, this.date, this.activated , this.downloaded);
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
        this.activated = data.activated;
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
    

}
