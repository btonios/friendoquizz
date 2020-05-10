﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Object
{
    public int playerId;
    public string playerName;
    public bool playerAnswer;
    public int playerPoints;

    public Player(int pId, string pName = "Player", bool playerAnswer = false, int pPoints = 0)
    {
        this.playerId = pId;
        this.playerName = pName;
        this.playerAnswer = playerAnswer;
        this.playerPoints = pPoints;
    }

    public int getPlayerId()
    {
        return this.playerId;
    }

    public void setPlayerId(int playerId)
    {
        this.playerId = playerId;
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

     public bool getPlayerAnswer()
    {
        return this.playerAnswer;
    }

    public void setPlayerAnswer(bool playerAnswer)
    {
        this.playerAnswer = playerAnswer;
    }



}
