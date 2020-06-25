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
    public GameObject panelLanguages;
    public GameObject panelWarning;
    public GameObject sliderQuestion;
    public GameObject AdsManager;
    public GameObject textAddPlayers;
    public GameObject sliderQuestionNumber;
    public GameObject toggleRandomDrink;
    
    public TMP_Dropdown ddSort;
    public TMP_Text textInfosVersion;
    public TMP_Text textQN;

    public List<Player> playerList;
    public bool rulesToggled;
    public bool infosToggled;
    public bool settingsToggled;
    public bool languagesToggled;
    public bool warningToggled;
    public bool qcToggled;
    public bool qbToggled;
    public bool canChange;

    public bool useRandomDrink;
    

    
    void Start()
    {
        rulesToggled = true;
        infosToggled = true;
        settingsToggled = true;
        languagesToggled = true;
        warningToggled = true;
        qcToggled = true;
        qbToggled = true;

        textInfosVersion.text = "Version: " + GlobalVariables.APP_VERSION;
    
        GameSettings.gameStatus = "menu";
        List<Player> playerList = new List<Player>(); 

        GlobalVariables.SetQuestionLists();

        if(GlobalVariables.firstOpeningLanguage == true)
        {
            GlobalVariables.firstOpeningLanguage = false;
            panelLanguages.SetActive(true);
        }
        else
        {
            if(GlobalVariables.firstOpening == true)
            {
                GlobalVariables.firstOpening = false;
                panelWarning.SetActive(true);
            }
        }

        
        AdsManager.GetComponent<AdsManager>().RequestBanner();
        GameSettings.SetGameSettings(sliderQuestionNumber, toggleRandomDrink);

        ddSort.options[0].text = GameLanguage.GetTraduction("QB_SORT_RECENT");
        ddSort.options[1].text = GameLanguage.GetTraduction("QB_SORT_BEST");
        ddSort.options[2].text = GameLanguage.GetTraduction("QB_SORT_DEVICE");
    }

    //makes game start
    public void OnPlay()
    {
        if(GameSettings.playerNumber > 1)
            SceneManager.LoadScene("Game");
        else
            ToggleTextAddPlayers(true);  
    }
    
    //add a player card on screen and focus on inputfield
    public void AddPlayer()
    {
        //add card to scene 
        GameObject card = Instantiate(playerCard) as GameObject;         
        card.transform.SetParent(content.transform, false);

        //open keyboard and to type player name
        card.GetComponentInChildren<TMP_InputField>().Select();   
    }

    public void CreatePlayerCardWithObject(Player player)
    {
        GameObject card = Instantiate(playerCard) as GameObject; 
        card.GetComponent<PlayerManager>().SetProperties(player);
        card.transform.Find("inputField").GetComponent<TMP_InputField>().text = player.playerName;
        card.transform.SetParent(content.transform, false);
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

    //changes question number by adding or removing by given value
    //if can't, starts a coroutine to fade timer text color
    public void ChangeQuestionNumber()
    {
        int number = (int)sliderQuestion.GetComponent<Slider>().value * 5;
        GameSettings.gameQuestionNumber = number;
        textQN.text = number.ToString();
    }

    //Toggle Language Panel
    public void ToggleLanguages()
    {
        if(languagesToggled == true) panelLanguages.SetActive(true);
        else panelLanguages.SetActive(false);
        languagesToggled = !languagesToggled;
    }

    //Toggle Warning Panel
    public void ToggleWarning()
    {
        if(warningToggled == true) panelWarning.SetActive(true);
        else panelWarning.SetActive(false);
        warningToggled = !warningToggled;
    }

    //Toggle Settings Panel
    public void ToggleSettings()
    {
        if(settingsToggled == true) panelSettings.SetActive(true);
        else panelSettings.SetActive(false);
        settingsToggled = !settingsToggled;
    }

    //Toggle Question Creation Panel
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

    //Toggle Question Browser Panel
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

    public void ToggleRandomDrink()
    {
        GameSettings.ChangeRandomDrink();
    }

    //Toggle text to add players berlow play button if < 2 players in settings
    public void ToggleTextAddPlayers(bool toggle)
    {
        textAddPlayers.SetActive(toggle);
    }

    public void SaveGameSettings()
    {
        SaveData.SaveGameSettings();
    }

    public void DebugQuestion()
    {
        GlobalVariables.debugQuestionList();
    }

    
}