using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public int id;
    public string label;
    public int points;
    public string status;
    public string language;
    public bool activated;
    public bool downloaded;


    public TMP_InputField textQuestion;
    public GameObject editQuestion;

     public Question Getthis()
    {
        return new Question(this.id, this.label, this.points , this.status,  this.language , this.activated , this.downloaded);
    }

    public void SetQuestionData(Question data)
    {
        this.id = data.id;
        this.label = data.label;
        this.points = data.points;
        this.status = data.status;
        this.language = data.language;
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
        GameSettings.SetQuestion(Getthis());
        editQuestion.SetActive(true);
    }
    
    public void DeleteQuestion()
    {
        GameSettings.DeleteQuestion(Getthis());
        Destroy(gameObject);
    }

    
    

}
