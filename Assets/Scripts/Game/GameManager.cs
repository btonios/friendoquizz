using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text question;
    public GameObject content;
    public GameObject playerCard;
    public GameObject panelRules;
    
    private List<Player> playerList;
    public bool rulesToggled;
     
    
    void Start()
    {
        playerList = GameSettings.playerList;
        rulesToggled = false;
        ToggleHelp();

        foreach(Player player in playerList)
        {
            GameObject card = Instantiate(playerCard) as GameObject;
            card.GetComponent<PlayerManager>().playerName = player.getPlayerName();
            card.GetComponentInChildren<TMP_Text>().text = player.getPlayerName();
            content.GetComponent<RectTransform>().offsetMax += new Vector2(card.GetComponent<RectTransform>().rect.width + 60, 0);
            card.transform.SetParent(content.transform, false);

        }
    }

    public void Next()
    {
        foreach(Transform card in content.transform)
        {
            card.GetComponent<Toggle>().isOn = false;
            card.GetComponent<Image>().color = new Color32(231, 47, 73, 250);;
        }

        GetNewQuestion();
    }

    public void GetNewQuestion()
    {
        int r = Random.Range(1, GameSettings.maxQuestionId);
        Question newQuestion = GameSettings.GetQuestion(r);
        question.text =  newQuestion.label;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ToggleHelp()
    {
        if(rulesToggled == true) panelRules.SetActive(true);
        else panelRules.SetActive(false);
        rulesToggled = !rulesToggled;
    }
}
