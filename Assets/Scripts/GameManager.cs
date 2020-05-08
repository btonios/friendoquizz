using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject content;
    public GameObject playerCard;
    private List<Player> playerList;
    
    void Start()
    {
        playerList = GameSettings.playerList;

        foreach(Player player in playerList)
        {
            GameObject card = Instantiate(playerCard) as GameObject;
            card.GetComponent<PlayerManager>().playerName = player.getPlayerName();
            card.GetComponentInChildren<TMP_Text>().text = player.getPlayerName();
            content.GetComponent<RectTransform>().offsetMax += new Vector2(card.GetComponent<RectTransform>().rect.width + 60, 0);
            card.transform.SetParent(content.transform, false);

        }
    }
}
