﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour
{
    public TMP_Text question;
    public TMP_Text answer;
    public GameObject content;
    public GameObject resultsContent;
    public GameObject playerCard;
    public GameObject playerCardResults;
    public GameObject panelRules;
    public GameObject panelResults;
    public GameObject panelGame;

    private AdsManager AdsManager;
    
    private List<Player> playerList;
    
    private List<Question> questionList;
    private List<Question> unusedQuestionsList;

    public bool rulesToggled;

    private string textOnlyLoser;
    private string textEquals;
    private string textAnd;
    private string textDrinkOnce;
     
    
    void Start()
    {
        //Load and show Ad on game start
        questionList = GlobalVariables.GetQuestionList();
        AdsManager = GameObject.FindGameObjectWithTag("AdsManager").GetComponent<AdsManager>();

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
            card.transform.SetParent(content.transform, false);
        }

        textEquals = GameLanguage.GetTraduction("GAME_ANSWER_EQUAL");
        textOnlyLoser = GameLanguage.GetTraduction("GAME_ANSWER_ONLYLOSER");
        textAnd = GameLanguage.GetTraduction("GAME_ANSWER_AND");
        textDrinkOnce = GameLanguage.GetTraduction("GAME_ANSWER_DRINKONCE");
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
        Debug.Log(GameSettings.gameQuestionCount);
        if (GameSettings.gameQuestionCount % 5 == 0)
            AdsManager.RequestInterstitial();

        //adapt UI
        panelGame.transform.Find("panelAnswer").gameObject.SetActive(false);
        panelGame.transform.Find("panelQuestion").gameObject.SetActive(true);

        //set default choice for each player
        foreach(Transform card in content.transform) card.gameObject.GetComponent<PlayerManager>().SetDefaultChoice();
        
        
        unusedQuestionsList = new List<Question>();
        
        //to keep track of used questions across games 
        //used bool is used and checked here
        bool allUsed = true;
        foreach(Question question in questionList.ToList())
        {
            if(question.used == false)
            {
                unusedQuestionsList.Add(question);
                allUsed = false;
            }
        }

        //if every question have been used, reset every question used bool to false
        if(allUsed == true)
        {
            foreach(Question question in GlobalVariables.questionList)
            {
                if(GlobalVariables.questionList.Any(q=>q.id == question.id))
                {
                    question.used = false;
                    GlobalVariables.SetQuestion(question);
                }           
            }

            questionList = GlobalVariables.GetQuestionList();
            
            //get new unused questions in unused question list
            foreach(Question question in questionList.ToList())
                if(question.used == false)
                    unusedQuestionsList.Add(question);
        }

        //pick random question in unused questions
        Question newQuestion = unusedQuestionsList[Random.Range(0, unusedQuestionsList.Count)];
        
        //set used bool to true
        newQuestion.used = true;
        
        foreach(Question question in GlobalVariables.questionList)
            if(question.id == newQuestion.id)
                GlobalVariables.SetQuestion(newQuestion);
            

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
                text = textEquals;
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
                    if (player.playerId == minorityList.Last().playerId && minorityList.Count() == 1) text += player.playerName + textOnlyLoser;
                    else if(player.playerId == minorityList.Last().playerId) text += textAnd + player.playerName + textDrinkOnce;
                    else if (player.playerId == minorityList[minorityList.Count()-2].playerId) text += player.playerName + " ";
                    else text += player.playerName + ", ";
                }
            }
        }
        else
        {
            text = textEquals;
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
        //Load Ad
        AdsManager.RequestInterstitial();


        //adapt UI
        panelResults.SetActive(true);
        panelGame.SetActive(false);

        //make player list ordered by points
        int rank = 1;
        List<Player> rankedList = GameSettings.playerList.OrderByDescending(o=>o.playerPoints).ToList();
        int lastPlayerPoints = rankedList[rankedList.Count-1].playerPoints;

        foreach(Player player in rankedList)
        {
            GameObject card = Instantiate(playerCardResults) as GameObject;
            card.GetComponentInChildren<TMP_Text>().text = player.getPlayerName();
            card.transform.Find("textRank").GetComponent<TMP_Text>().text = rank.ToString();
            card.transform.Find("textName").GetComponent<TMP_Text>().text = player.getPlayerName();
            card.transform.Find("textPoints").GetComponent<TMP_Text>().text = player.getPlayerPoints().ToString();

            //random drink
            if(GameSettings.randomDrink == true)
            {
                int rnd = Random.Range(0, 100);
                if(rnd>50)
                    card.transform.Find("imageBeer").gameObject.SetActive(false);
            }
            else
            {
                if(player.playerPoints != lastPlayerPoints)
                    card.transform.Find("imageBeer").gameObject.SetActive(false);
            }
            
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

            //set color of last player(s)
            if(player.playerPoints == lastPlayerPoints)
                card.transform.Find("textRank").GetComponent<TMP_Text>().color = new Color32(255, 0, 0, 255);
                    

            card.transform.SetParent(resultsContent.transform, false);
            rank++;
        }

    }
}
