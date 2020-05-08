using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    public int playerId;
    public string playerName;
    public int playerPoints = 0;

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
        //create player object and make global variables changes
        this.playerId = GameSettings.lastPlayerId + 1;
        this.playerName = card.GetComponentInChildren<TMP_InputField>().text;

        Player newPlayer = new Player(this.playerId, this.playerName);
        
        GameSettings.lastPlayerId = this.playerId;
        GameSettings.playerNumber++;        
        GameSettings.playerList.Add(newPlayer);

        Debug.Log("Global Settings: LastId: " + GameSettings.lastPlayerId + " PlayerNumber: "+GameSettings.playerNumber);
    }
}
