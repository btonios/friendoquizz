using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public TMP_Text question;
    public TMP_Text answer;
    public TMP_Text textButtonNext;
    public GameObject content;
    public GameObject resultsContent;
    public GameObject playerCard;
    public GameObject playerCardResults;
    public GameObject panelRules;
    public GameObject panelResults;
    public GameObject panelGame;
    
    private List<Player> playerList;
    public bool rulesToggled;
     
    
    void Start()
    {
        GameSettings.gameStatus = "question";
        playerList = GameSettings.playerList;
        rulesToggled = false;

        //instantiate each car based on player list from game settings
        foreach(Player player in playerList)
        {
            GameObject card = Instantiate(playerCard) as GameObject;
            card.GetComponent<PlayerManager>().playerId = player.getPlayerId();
            card.GetComponent<PlayerManager>().playerName = player.getPlayerName();
            card.GetComponentInChildren<TMP_Text>().text = player.getPlayerName();
            content.GetComponent<RectTransform>().offsetMax += new Vector2(card.GetComponent<RectTransform>().rect.width + 60, 0);
            card.transform.SetParent(content.transform, false);
        }


        Next();
    }

    //when button Next clicked, decides what to do based on game status
    public void Next()
    {
        switch (GameSettings.gameStatus)
        {
            case "answer":
                GetAnswer();
                break;

            case "question":
                GetNewQuestion();
                break;

            case "results":
                EndGame();
                break;

            default:
                GetNewQuestion();
                break;
        }
    }

    //get new question 
    public void GetNewQuestion()
    {
        //adapt UI
        panelGame.transform.Find("panelAnswer").gameObject.SetActive(false);
        panelGame.transform.Find("panelQuestion").gameObject.SetActive(true);
        textButtonNext.text = "Verdict!";

        //set default choice for each player
        foreach(Transform card in content.transform) card.gameObject.GetComponent<PlayerManager>().SetDefaultChoice();
       
       
        //get all unused questions
        List<Question> unusedQuestions = new List<Question>();
        foreach(Question questionInList in GlobalVariables.questionList.ToList())
        {
            if(questionInList.used == false)
            {
                unusedQuestions.Add(questionInList);
            }
        }
           
        
        //if every questions has been used, reset list
        if(unusedQuestions.Count == 0)
        {
            foreach(Question questionInList in GlobalVariables.questionList)
            {
                questionInList.used = false;
                GlobalVariables.SetQuestion(questionInList);
            }
                
            unusedQuestions = GlobalVariables.questionList;
            
        }

        //pick random question in unused questions
        Question newQuestion = unusedQuestions[Random.Range(0, unusedQuestions.Count)];

        //set question to used
        foreach(Question questionInList in GlobalVariables.questionList.ToList())
        {
            if(questionInList.id == newQuestion.id)
            {
                questionInList.used = true;
                GlobalVariables.SetQuestion(questionInList);
            }
        }    

        question.text =  newQuestion.label;

        //update game settings
        GameSettings.gameStatus = "answer";
    }

    //get answer results
    public void GetAnswer()
    {
        string text = "";
        int yesVotes = 0;
        int noVotes = 0;
        bool yesWon = true;
        List<Player> minorityList = new List<Player>();

        //adapt UI
        panelGame.transform.Find("panelAnswer").gameObject.SetActive(true);
        panelGame.transform.Find("panelQuestion").gameObject.SetActive(false);
        textButtonNext.text = "Question suivante";

        //decide if yes or no is majority
        foreach (Player player in GameSettings.playerList)
        {
            if(player.playerAnswer == true) yesVotes++;
            else noVotes++;   
        }

        //give majority a point and assign to string minority's player's name
        if(yesVotes != noVotes)
        {
            if (yesVotes == playerList.Count() || noVotes == playerList.Count())
            {
                text = "Tout le monde a répondu la même chose, pénalité! Tout le monde perd!";
            }
            else
            {
                if(yesVotes < noVotes) yesWon = false;

                foreach (Player player in GameSettings.playerList)
                {
                    if(player.playerAnswer == yesWon) player.playerPoints++;
                        else minorityList.Add(player);
                }

                //construct sentences depending of minority number and position in list
                foreach(Player player in minorityList)
                {
                    Debug.Log(minorityList[minorityList.Count()-2].playerId);
                    if (player.playerId == minorityList.Last().playerId && minorityList.Count() == 1) text += player.playerName + " est le seul perdant! Deux gorgées!";
                    else if(player.playerId == minorityList.Last().playerId) text += "et " + player.playerName + " boivent une gorgée!";
                    else if (player.playerId == minorityList[minorityList.Count()-2].playerId) text += player.playerName + " ";
                    else text += player.playerName + ", ";
                }
            }
        }
        else
        {
            text = "Égalité, tout le monde a le droit à deux gorgées!";
        } 

        answer.text = text;

        //update game settings
        GameSettings.gameQuestionCount++;
        if(GameSettings.gameQuestionCount >= GameSettings.gameQuestionNumber) GameSettings.gameStatus = "results";
        else GameSettings.gameStatus = "question";
    }

    //stop game to go to menu
    public void GoToMenu()
    {
        GameSettings.SetDefaultSettings();
        SceneManager.LoadScene("Menu");
    }

    //toggle rules panel
    public void ToggleRules()
    {
        if(rulesToggled == true) panelRules.SetActive(true);
        else panelRules.SetActive(false);
        rulesToggled = !rulesToggled;
    }

    //show result screen
    public void EndGame()
    {
        //adapt UI

        panelResults.SetActive(true);
        panelGame.SetActive(false);

        //make player list ordered by points
        int rank = 1;
        List<Player> rankedList = GameSettings.playerList.OrderByDescending(o=>o.playerPoints).ToList();
        foreach(Player player in rankedList)
        {
            GameObject card = Instantiate(playerCardResults) as GameObject;
            card.GetComponentInChildren<TMP_Text>().text = player.getPlayerName();
            card.transform.Find("textRank").GetComponent<TMP_Text>().text = rank.ToString();
            card.transform.Find("textName").GetComponent<TMP_Text>().text = player.getPlayerName();
            card.transform.Find("textPoints").GetComponent<TMP_Text>().text = player.getPlayerPoints().ToString();
            
            //set color of podium
            switch (rank)
            {
                case 1:
                    card.transform.Find("textRank").GetComponent<TMP_Text>().color = new Color32(255, 215, 0, 255);
                    break;

                case 2:
                    card.transform.Find("textRank").GetComponent<TMP_Text>().color = new Color32(196, 202, 206, 255);
                    break;

                case 3:
                    card.transform.Find("textRank").GetComponent<TMP_Text>().color = new Color32(177, 86,15, 255);
                    break;

                default:
                    card.transform.Find("textRank").GetComponent<TMP_Text>().color = Color.white;
                    break;
            }

            resultsContent.GetComponent<RectTransform>().offsetMin -= new Vector2(0, card.GetComponent<RectTransform>().rect.height + 30);
            card.transform.SetParent(resultsContent.transform, false);
            rank++;
        }

    }
}
