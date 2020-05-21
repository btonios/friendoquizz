using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestionBrowserManager : MonoBehaviour
{
    public Transform content;
    public GameObject panelQuestion;
    public List<Question> browserQuestionList = new List<Question>();

    NetworkManager NetworkManager;

    void Start()
    {
        NetworkManager = GetComponent<NetworkManager>();
    }
    
    public void SearchQuestions(string sort)
    {
        NetworkManager.GetBrowserQuestions(sort);
    }

    public void ShowQuestions()
    {
        DeleteQuestionCards();
        if(browserQuestionList.Count > 0)
        {
            if(GameObject.Find("textContentInfos"))
                GameObject.Find("textContentInfos").SetActive(false);

            foreach(Question question in browserQuestionList)
            {
                GameObject card = CreateQuestionCard();
                card.GetComponent<QuestionBrowser>().SetQuestionData(question);                              
            }
        }
        else
        {
            GameObject.Find("textContentInfos").SetActive(true);
        }
        
    }

    public GameObject CreateQuestionCard()
    {
        GameObject card = Instantiate(panelQuestion) as GameObject; 
        card.transform.SetParent(content, false);
        return card;
    }

    void DeleteQuestionCards()
    {
        if(content.childCount > 0)
        {
            foreach (Transform card in content) 
            {
                Destroy(card.gameObject);
            } 
        }
    }
}
