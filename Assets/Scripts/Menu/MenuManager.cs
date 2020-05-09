using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    public GameObject content;
    public GameObject playerCard;
    public GameObject panelRules;

    public List<Player> playerList;
    public string tempString;
    public bool rulesToggled;

    TouchScreenKeyboard keyboard;

    
    void Start()
    {
        rulesToggled = false;
        ToggleRules();
    }

    public void OnPlay()
    {
        GameSettings.LoadQuestions();
        SceneManager.LoadScene("Game");
    }
    
    public void AddPlayer()
    {
        //add card to scene and add 130 to bottom to make scrolling work correctly
        GameObject card = Instantiate(playerCard) as GameObject; 
        content.GetComponent<RectTransform>().offsetMax += new Vector2(card.GetComponent<RectTransform>().rect.width + 60, 0);

        card.transform.SetParent(content.transform, false);

        //open keyboard and type name
        card.GetComponentInChildren<TMP_InputField>().Select();
        
        
    }

    public void setListQuestionTest()
    {
        GameSettings.setList();
        SaveData.SaveQuestions(GameSettings.questionList);
    }

    public void ToggleRules()
    {
        if(rulesToggled == true) panelRules.SetActive(true);
        else panelRules.SetActive(false);
        rulesToggled = !rulesToggled;
    }
}
