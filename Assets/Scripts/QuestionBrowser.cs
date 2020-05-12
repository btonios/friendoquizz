using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionBrowser : MonoBehaviour
{
    public int id;
    public string label;
    public int points;
    public string status;
    public string language;
    public bool activated;
    public bool downloaded;


    public int test;
    public TMP_Text textQuestion;
    public TMP_Text textInfos;
    public GameObject imageDownload;
    public GameObject imageRemove;

    public Question GetQuestionData()
    {
        return new Question(this.id, this.label, this.points , this.status,  this.language , this.activated , this.downloaded);
    }

    public void SetQuestionData(Question data)
    {
        test = data.id;
        this.id = data.id;
        this.label = data.label;
        this.points = data.points;
        this.status = data.status;
        this.language = data.language;
        this.activated = data.activated;
        this.downloaded = data.downloaded;

        textQuestion.text = this.label;

        if(this.points <= -10)
            textInfos.text = "Will be deleted soon.";
        else
            textInfos.text = this.points + " points";

    }

    public void ChangePoints(int point)
    {   
        this.points += point;
        if(this.points > -10)
            textInfos.text = this.points + " points";
        else
            textInfos.text = textInfos.text = "Will be deleted soon.";
    }

    public void ToggleDownloaded()
    {
        if(this.downloaded == false)
        {
            GameSettings.SetQuestion(GetQuestionData());
            GetComponent<Image>().color = new Color32(30, 30, 30, 150);
            imageRemove.SetActive(true);
            imageDownload.SetActive(false);
        }
        else
        {
            GameSettings.DeleteQuestion(GetQuestionData());
            GetComponent<Image>().color = new Color32(0, 0, 0, 150);
            imageRemove.SetActive(false);
            imageDownload.SetActive(true);
        }

        this.downloaded = !this.downloaded;

    }

    
    

}
