using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class QuestionBrowser : MonoBehaviour
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
    public bool reported;

    public bool used;
    public bool downloaded;

    public TMP_Text textQuestion;
    public TMP_Text textInfos;
    public GameObject imageDownload;
    public GameObject imageRemove;
    public Button buttonUpvote;
    public Button buttonDownvote;
    public Button buttonReport;



    public Question GetQuestionData()
    {
        return new Question(this.id, this.label, this.points , this.status,  this.language , this.publisherMAC, this.nickname, this.date, this.voteY_N, this.reported, this.used , this.downloaded);
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
        this.voteY_N = data.voteY_N;
        this.reported = data.reported;

        this.used = data.used;
        this.downloaded = data.downloaded;

        string textNickname = this.nickname;
        string textPoints = this.points + " points";
        string textDate = System.DateTime.ParseExact(this.date, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");


        textQuestion.text = this.label;
        
        SetInfosText();

        if(this.voteY_N == 1)
        {
            buttonUpvote.interactable = false;
            buttonDownvote.interactable = true;
        }
        else if(this.voteY_N == -1)
        {
            buttonUpvote.interactable = true;
            buttonDownvote.interactable = false;
        }

        if(this.reported == true)
            buttonReport.interactable = false;


        //check if question is already downloaded
        if(GlobalVariables.questionList.Any(q => q.id == id))
            SetIsDownloaded(true);
        else
            SetIsDownloaded(false); 

    }

    public void ChangePoints(int point)
    {      
        

        int pointsValue = point;
        if(this.voteY_N != 0) pointsValue = point*2;
        if(point == 1)
        {
            this.voteY_N = 1;
            this.points += pointsValue;
            buttonUpvote.interactable = false;
            buttonDownvote.interactable = true;
            ChangePointsInServer();
        }
        else if (point == -1)
        {
            this.voteY_N = -1;
            this.points += pointsValue;
            buttonUpvote.interactable = true;
            buttonDownvote.interactable = false;
            ChangePointsInServer();
        }
        
        SetInfosText();
    }

    public void DoReport()
    {
        buttonReport.interactable = false;
        GameObject.Find("MenuManager").GetComponent<NetworkManager>().DoReport(GetQuestionData());
    }

    void SetInfosText()
    {
        if(this.points > -10)
        {
            string textNickname = this.nickname;
            string textPoints = this.points + " points";
            string textDate = System.DateTime.ParseExact(this.date, "yyyy-MM-dd", null).ToString("dd/MM/yyyy");
            textInfos.text = textNickname+" • "+textPoints+" • le "+textDate;
        }
        else
        {
            textInfos.text = textInfos.text = "Cette question va bientôt être supprimée car elle a un score trop négatif.";
        }
    }
    public void ChangePointsInServer()
    {
        GameObject.Find("MenuManager").GetComponent<NetworkManager>().ChangeQuestionPoints(GetQuestionData());
    }

    public void ToggleDownloaded()
    {
        if(this.downloaded == true)
        {
            GlobalVariables.SetQuestion(GetQuestionData());
            SetIsDownloaded(false);
        }
        else
        {            
            GlobalVariables.DeleteQuestion(GetQuestionData());
            SetIsDownloaded(true);
        }
    }

    public void SetIsDownloaded(bool dl)
    {
        if(dl == true)
        {
            GetComponent<Image>().color = new Color32(30, 30, 30, 150);
            imageRemove.SetActive(true);
            imageDownload.SetActive(false); 
        }
        else
        {
            GetComponent<Image>().color = new Color32(0, 0, 0, 150);
            imageRemove.SetActive(false);
            imageDownload.SetActive(true);
        }
        
        this.downloaded = dl;
    }

    
    

}
