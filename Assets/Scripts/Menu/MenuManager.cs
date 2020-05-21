using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class MenuManager : MonoBehaviour
{
    public GameObject content;
    public GameObject playerCard;
    public GameObject panelRules;
    public GameObject panelSettings;
    public GameObject panelQuestionCreator;
    public GameObject panelMainMenu;
    public GameObject panelQuestionBrowser;
    public GameObject panelInfos;

    public TMP_Text textInfosVersion;
    public TMP_Text textQN;

    public List<Player> playerList;
    public bool rulesToggled;
    public bool infosToggled;
    public bool settingsToggled;
    public bool qcToggled;
    public bool qbToggled;
    public bool canChange;
    bool fading = false;


    
    void Start()
    {
        rulesToggled = true;
        infosToggled = true;
        settingsToggled = true;
        qcToggled = true;
        qbToggled = true;
        textInfosVersion.text = "Version: " + GlobalVariables.APP_VERSION;
    
        GameSettings.gameStatus = "menu";
        List<Player> playerList = new List<Player>(); 

        GlobalVariables.SetQuestionList();
    }

    //makes game start
    public void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }
    
    //add a player card on screen and focus on inputfield
    public void AddPlayer()
    {
        //add card to scene and add 130 to bottom to make scrolling work correctly
        GameObject card = Instantiate(playerCard) as GameObject; 

        card.transform.SetParent(content.transform, false);

        //open keyboard and to type player name
        card.GetComponentInChildren<TMP_InputField>().Select();   
    }

    //sets question list
    public void setListQuestionTest()
    {
        GlobalVariables.setList();
        SaveData.SaveQuestions();
    }

    public void SaveCurrentList()
    {
        SaveData.SaveQuestions();
    }

    public void ResetList()
    {
        SaveData.ResetQuestions();
    }

    //toggle function that either sets rules panel active or false
    public void ToggleRules()
    {
        if(rulesToggled == true) panelRules.SetActive(true);
        else panelRules.SetActive(false);
        rulesToggled = !rulesToggled;
    }

    //toggle function that either sets infos panel active or false
    public void ToggleInfos()
    {
        if(infosToggled == true) panelInfos.SetActive(true);
        else panelInfos.SetActive(false);
        infosToggled = !infosToggled;
    }


    //Fades from red to white text color
    IEnumerator FadeError(TMP_Text text)
    {
        float ElapsedTime = 0.0f;
        float TotalTime = 0.25f;
        while (ElapsedTime < TotalTime) 
        {
            ElapsedTime += Time.deltaTime;
            text.color = Color.Lerp(Color.red, Color.white, (ElapsedTime / TotalTime));
            fading = false;
            yield return null;
        }

    }

    //changes question number by adding or removing by given value
    //if can't, starts a coroutine to fade timer text color
    public void ChangeQuestionNumber(int value)
    {
       canChange = GameSettings.ChangeQuestionNumber(value);
       if (canChange == false && fading == false)
       {
           StartCoroutine(FadeError(textQN.GetComponent<TMP_Text>()));
           fading = true;
       } 
       textQN.text = GameSettings.gameQuestionNumber.ToString();
    }

    public void ToggleSettings()
    {
        if(settingsToggled == true) panelSettings.SetActive(true);
        else panelSettings.SetActive(false);
        settingsToggled = !settingsToggled;
    }

    public void ToggleQuestionCreator(Transform content)
    {
        if(qcToggled == true)
        {
            panelMainMenu.SetActive(false);
            panelQuestionCreator.SetActive(true);
            GetComponent<QuestionCreatorManager>().ShowExistingQuestions();
        } 
        else
        {
            panelMainMenu.SetActive(true);
            panelQuestionCreator.SetActive(false);

            foreach (Transform card in content) 
            {
                Destroy(card.gameObject);
            }  
        } 
        qcToggled = !qcToggled;
    }

    public void ToggleQuestionBrowser(Transform content)
    {
        if(qbToggled == true)
        {
            panelMainMenu.SetActive(false);
            panelQuestionBrowser.SetActive(true);
            GetComponent<QuestionBrowserManager>().SearchQuestions("every");
        } 
        else
        {
            panelMainMenu.SetActive(true);
            panelQuestionBrowser.SetActive(false);
            if(content.childCount > 0)
            {
                foreach (Transform card in content) 
                {
                    Destroy(card.gameObject);
                } 
            }
                 
        } 
        qbToggled = !qbToggled;
    }

    public void DebugList()
    {
        GlobalVariables.debuglist();
    }
}
