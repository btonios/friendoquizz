using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestionBrowserManager : MonoBehaviour
{
    public GameObject content;
    public GameObject panelQuestion;
    public List<Question> browserQuestionList = new List<Question>();

    NetworkManager NetworkManager;

    void Start()
    {
        NetworkManager = GetComponent<NetworkManager>();
    }
    
    public void SearchQuestions()
    {
        NetworkManager.GetBrowserQuestions();
    }

    public void ShowQuestions()
    {
        foreach(Question question in browserQuestionList)
        {
            GameObject card = CreateQuestionCard();
        
            if(GlobalVariables.questionList.Any(q=>q.id == question.id))
            {
                card.GetComponent<QuestionBrowser>().downloaded = true;

                card.GetComponent<Image>().color = new Color32(30, 30, 30, 150);
                card.transform.Find("buttonDownload").Find("imageDownload").gameObject.SetActive(false);
                card.transform.Find("buttonDownload").Find("imageRemove").gameObject.SetActive(true);
            }
            else
            {
                
                card.GetComponent<QuestionBrowser>().downloaded = false;
                card.transform.Find("buttonDownload").Find("imageRemove").gameObject.SetActive(false);
            }

            card.GetComponent<QuestionBrowser>().SetQuestionData(question);
        }
    }

    public GameObject CreateQuestionCard()
    {
        GameObject card = Instantiate(panelQuestion) as GameObject; 
        card.transform.SetParent(content.transform, false);
        return card;
    }
    
}
