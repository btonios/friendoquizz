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
    public GameObject sliderQuestion;
    public GameObject AdsManager;
    public GameObject textAddPlayers;
    public GameObject sliderQuestionNumber;
    public GameObject toggleRandomDrink;

    public TMP_Text textInfosVersion;
    public TMP_Text textQN;

    public List<Player> playerList;
    public bool rulesToggled;
    public bool infosToggled;
    public bool settingsToggled;
    public bool qcToggled;
    public bool qbToggled;
    public bool canChange;

    public bool useRandomDrink;
    

    
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

        GlobalVariables.SetQuestionLists();


        if(GlobalVariables.firstOpening == true)
        {
            
            GlobalVariables.firstOpening = false;
            Popup.Initialise("<color=\"red\">ATTENTION", "L'abus d'alcool est dangereux pour la santé. Consommez avec modération.\n\n En poursuivant, vous confirmez avoir au moins 18 ans et être responsable des conséquences que pourrait engendrer l'utilisation de FESTIS.", GameObject.Find("MenuCanvas"), false);
            Popup.Show();
        }

        AdsManager.GetComponent<AdsManager>().RequestBanner();
        GameSettings.SetGameSettings(sliderQuestionNumber, toggleRandomDrink);
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

    //changes question number by adding or removing by given value
    //if can't, starts a coroutine to fade timer text color
    public void ChangeQuestionNumber()
    {
        int number = (int)sliderQuestion.GetComponent<Slider>().value * 5;
        GameSettings.gameQuestionNumber = number;
        textQN.text = number.ToString();
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

    public void DebugList(string type)
    {
        switch(type)
        {
            case "native":
                GlobalVariables.debugNativeQuestionList();
            break;

            case "user":
                GlobalVariables.debugQuestionList();
            break;

            case "both":
                GlobalVariables.debugBothQuestionLists();
            break;
        }
        
    }
    

    public void ToggleRandomDrink()
    {
        GameSettings.ChangeRandomDrink();
    }

    public void ToggleTextAddPlayers(bool toggle)
    {
        textAddPlayers.SetActive(toggle);
    }

    public void SaveGameSettings()
    {
        SaveData.SaveGameSettings();
    }
}
