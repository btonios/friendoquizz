using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    public int playerId;
    public string playerName;
    public int playerPoints = 0;
    public bool status;


    public void RemovePlayer()
    {
        string bPl = "before: ";
        foreach(Player player in GameSettings.playerList)
        {
            bPl += "["+player.playerId+"]"+player.playerName + ", ";
        }
        Debug.Log(bPl);

        for(int i=GameSettings.playerList.Count - 1; i > -1; i--)
        {
            if(GameSettings.playerList[i].playerId==this.playerId)
            {
                GameObject.Find("Content").GetComponent<RectTransform>().offsetMin += new Vector2(0, 120);
                GameSettings.playerNumber--;
                GameSettings.playerList.RemoveAt(i);
            }
        }
        string aPl = "after: ";
        foreach(Player player in GameSettings.playerList)
        {
            aPl += "["+player.playerId+"]"+player.playerName + ", ";
        }
        Debug.Log(aPl);
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

    public void OnToggleValueChanged()
    {
        Toggle toggle = GetComponent<Toggle>();
        Image toggleImg = GetComponent<Image>();

        Color32 noColor = new Color32(231, 47, 73, 250);
        Color32 yesColor = new Color32(104, 213, 81, 250);

        if(toggle.isOn)
        {
            status = true;
            toggleImg.color = yesColor;
        }
        else
        {
            status = false;
            toggleImg.color = noColor;

        }
    }
}
