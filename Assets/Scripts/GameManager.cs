using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerCard;
    public List<Player> playerList;
    void Start()
    {
        playerList = GameSettings.playerList;

        foreach(Player player in playerList)
        {
            GameObject card = Instantiate(playerCard) as GameObject;
            card.GetComponentInChildren<Text>().text = player.getPlayerName();
        }
    }
}
