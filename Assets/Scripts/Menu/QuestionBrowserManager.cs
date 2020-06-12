using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestionBrowserManager : MonoBehaviour
{
    public Transform content;
    public GameObject panelQuestion;
    public GameObject panelFilterButtons;
    public GameObject buttonBlockFilters;
    public GameObject contentInfos;
    public Image imgButtonSortMyQuestions;
    public Image imgButtonSortRecent;
    public Image imgButtonSortBest;
    public Sprite spritePressed;
    public Sprite spriteNone;
    
    public List<Question> browserQuestionList = new List<Question>();
    public List<int> deleteListId;
    NetworkManager NetworkManager;

    bool filtersShowing;

    void Start()
    {
        filtersShowing = false;
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
            if(contentInfos != null)
            contentInfos.SetActive(false);

            foreach(Question question in browserQuestionList)
            {
                GameObject card = CreateQuestionCard();
                card.GetComponent<QuestionBrowser>().SetQuestionData(question);                              
            }
        }
        else
        {
            contentInfos.SetActive(true);
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

    void ShowDownloadedQuestions()
    {
        if(content.childCount > 0)
            foreach (Transform card in content) 
                if(card.GetComponent<QuestionBrowser>().downloaded == false)       
                    Destroy(card.gameObject);
    }

    void ShowNotDownloadedQuestions()
    {
        if(content.childCount > 0)
            foreach (Transform card in content) 
                if(card.GetComponent<QuestionBrowser>().downloaded == true)       
                    Destroy(card.gameObject);
    }


    /** sort questions based on int of dropdown
    *   0 - recent
    *   1 - best
    *   2 - device
    */
    public void DropdownSort(int selected)
    {
        switch(selected)
        {
            //sort by recent
            case 0:
                SearchQuestions("recent");
            break;

            //sort by best
            case 1:
                SearchQuestions("best");
            break;

            //sort by device
            case 2:
                SearchQuestions("device");
            break;


        }
    }

    /** filter questions based on int of dropdown
    *   0 - downloaded
    *   1 - not downloaded
    */
    public void DropdownFilter(int selected)
    {
        switch(selected)
        {
            //filter by downloaded
            case 0:
                ShowDownloadedQuestions();
            break;

            //filter non downloaded
            case 1:
                ShowNotDownloadedQuestions();
            break;
        }
        ToggleFilterButtons();
    }

    public void ToggleFilterButtons()
    {
        if(filtersShowing == true)
        {
            panelFilterButtons.SetActive(false);
            buttonBlockFilters.SetActive(false);      
        }
        else
        {
            panelFilterButtons.SetActive(true);
            buttonBlockFilters.SetActive(true);
        }
        
        filtersShowing = !filtersShowing;
    }
}
