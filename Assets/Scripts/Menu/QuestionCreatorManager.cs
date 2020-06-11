using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionCreatorManager : MonoBehaviour
{
    
    public GameObject content;
    public GameObject panelQuestion;

    
    public TMP_InputField textNickname;
    public TMP_InputField textNewQuestion;

    TouchScreenKeyboard keyboard;


    void Start()
    {
        textNickname.text = SaveData.LoadNickname();
    }

    public int GetLastCustomId()
    {
        int lastId = 0;
        foreach(Question question in GlobalVariables.questionList)
            if(question.id < lastId) lastId = question.id;
        return lastId;
    }

    public void ShowExistingQuestions()
    {
        foreach(Question question in GlobalVariables.questionList)
        {
            if(question.id<0)
            {
                GameObject card = CreateQuestionCard();
                card.GetComponent<QuestionManager>().SetQuestionData(question);
            }
        }
    }

    public void CreateQuestion()
    {
        //add card to scene 
        GameObject card = CreateQuestionCard();

        //add question to local database
        Question question = new Question(GetLastCustomId()-1, textNewQuestion.text, 0, "custom", GlobalVariables.LANGUAGE);
        GlobalVariables.SetQuestion(question);

        //set created question attributes
        card.GetComponent<QuestionManager>().SetQuestionData(question);

        
        //set inputfield new question to nothing
        textNewQuestion.text = "";
    }

    public GameObject CreateQuestionCard()
    {
        GameObject card = Instantiate(panelQuestion) as GameObject; 
        card.transform.SetParent(content.transform, false);
        return card;
    }

    public void EditNickname()
    {
        GlobalVariables.SetNickname(textNickname.text);
    }
}
