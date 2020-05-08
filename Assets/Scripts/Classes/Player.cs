using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Object
{
    public int playerId;
    public string playerName;
    public int playerPoints;

    public Player(int pId, string pName = "Player", int pPoints = 0)
    {
        this.playerId = pId;
        this.playerName = pName;
        this.playerPoints = pPoints;
    }

    public int getPlayerPoints()
    {
        return this.playerPoints;
    }

    public void setPlayerPoints(int playerPoints)
    {
        this.playerPoints = playerPoints;
    }

    public string getPlayerName()
    {
        return this.playerName;
    }

    public void setPlayerName(string playerName)
    {
        this.playerName = playerName;
    }



}
