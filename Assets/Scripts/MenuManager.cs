using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    public GameObject content;
    public List<Player> playerList;
    public GameObject playerCard;

    public string tempString;
    TouchScreenKeyboard keyboard;
    
    public void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }

    
    
    public void AddPlayer()
    {
        //add card to scene and add 130 to bottom to make scrolling work correctly
        content.GetComponent<RectTransform>().offsetMin -= new Vector2(0, 120);
        GameObject card = Instantiate(playerCard) as GameObject; 
        
        card.transform.SetParent(content.transform, false);

        //open keyboard and type name
        card.GetComponentInChildren<TMP_InputField>().Select();
        
        
    }




}
