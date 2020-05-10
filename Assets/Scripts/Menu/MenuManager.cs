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
    public GameObject panelDisabler;
    public TMP_Text textTimer;

    public List<Player> playerList;
    public bool rulesToggled;
    public bool canChange;
    bool fading= false;

    TouchScreenKeyboard keyboard;

    
    void Start()
    {
        GameSettings.gameStatus = "menu";
        List<Player> playerList = new List<Player>();

        rulesToggled = false;
        ToggleRules();

        GameSettings.useTimer = false;
        ToggleTimer();
        

        GameSettings.setList();
    }

    //makes game start
    public void OnPlay()
    {
        GameSettings.LoadQuestions();
        SceneManager.LoadScene("Game");
    }
    
    //add a player card on screen and focus on inputfield
    public void AddPlayer()
    {
        //add card to scene and add 130 to bottom to make scrolling work correctly
        GameObject card = Instantiate(playerCard) as GameObject; 
        content.GetComponent<RectTransform>().offsetMax += new Vector2(card.GetComponent<RectTransform>().rect.width + 60, 0);

        card.transform.SetParent(content.transform, false);

        //open keyboard and to type player name
        card.GetComponentInChildren<TMP_InputField>().Select();   
    }

    //sets question list
    public void setListQuestionTest()
    {
        GameSettings.setList();
        SaveData.SaveQuestions(GameSettings.questionList);
    }

    //toggle function that either sets rules panel active or false
    public void ToggleRules()
    {
        if(rulesToggled == true) panelRules.SetActive(true);
        else panelRules.SetActive(false);
        rulesToggled = !rulesToggled;
    }

    //changes timer values by given value
    //if can't, starts a coroutine to fade timer text color
    public void ChangeTimerValue(float value)
    {
       canChange = GameSettings.ChangeTimerValue(value);
       if (canChange == false && fading == false)
       {
           StartCoroutine(FadeError(textTimer.GetComponent<TMP_Text>()));
           fading = true;
       } 
       textTimer.text = GameSettings.timer.ToString() + " secondes";
    }

    //toggles panelDisabler and global variable userTimer
    public void ToggleTimer()
    { 
        if(GameSettings.useTimer == true) panelDisabler.SetActive(false);
        else panelDisabler.SetActive(true);
        GameSettings.useTimer = !GameSettings.useTimer; 
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


}
