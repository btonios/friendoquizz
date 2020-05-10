using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{

    public int playerId;
    public string playerName;
    public int playerPoints = 0;

    public Toggle toggle;
    public Image toggleImg;


    void Start()
    {

    }

    public void RemovePlayer()
    {
        for(int i=GameSettings.playerList.Count - 1; i > -1; i--)
        {
            if(GameSettings.playerList[i].playerId==this.playerId)
            {
                GameObject.Find("Content").GetComponent<RectTransform>().offsetMax -= new Vector2(GameObject.Find("Content").GetComponent<RectTransform>().rect.width + 60, 0);
                GameSettings.playerNumber--;
                GameSettings.playerList.RemoveAt(i);
            }
        }
        Destroy(gameObject);
    }

    public void AddPlayerToList(GameObject card)
    {
        //apply data to created player card
        this.playerId = GameSettings.lastPlayerId + 1;
        this.playerName = card.GetComponentInChildren<TMP_InputField>().text;

        Player newPlayer = new Player(this.playerId, this.playerName);
        
        //change global variables
        GameSettings.lastPlayerId = this.playerId;
        GameSettings.playerNumber++;        
        GameSettings.playerList.Add(newPlayer);
    }

    public void SetDefaultChoice()
    {
        toggle.isOn = false;
        ChangePlayerAnswer();
    }

    public void ChangePlayerAnswer()
    {
        if(toggle.isOn)
        {
            toggleImg.color = new Color32(104, 213, 81, 250);

            foreach (Player player in GameSettings.playerList) 
                if(player.playerId == playerId) player.playerAnswer = true;
        }
        else
        {
            toggleImg.color = new Color32(231, 47, 73, 250);

            foreach (Player player in GameSettings.playerList) 
                if(player.playerId == playerId) player.playerAnswer = false; 
        }
    }
}
