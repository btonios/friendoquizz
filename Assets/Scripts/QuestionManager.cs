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

    public TMP_InputField textQuestion;
    public GameObject editQuestion;


    public void SetQuestionData(Question data)
    {
        this.id = data.id;
        this.label = data.label;
        this.points = data.points;
        this.status = data.status;
        this.language = data.language;

        textQuestion.text = this.label;

        Question question = new Question(data.id, data.label, data.points, data.status, data.language, data.activated);
    }

    public void StartEditQuestion()
    {   
        editQuestion.SetActive(false);
        textQuestion.Select(); 
        textQuestion.ActivateInputField();
    }

    public void EndEditQuestion()
    {
        label = textQuestion.text;
        Question question = new Question(id, label, points, status, language, activated);
        GameSettings.SetQuestion(question);
        editQuestion.SetActive(true);
    }
    
    public void DeleteQuestion()
    {
        GameSettings.DeleteQuestion(this.id);
        Destroy(gameObject);
    }

    
    

}
