using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{ 	
    public int id;
    public string label;
    public int points;
    public string status;
    public string language;
    public string publisherMAC;
    public string nickname;
    public string date;
    public int voteY_N;
    public bool reported;

    public bool used;
    public bool downloaded;


    public Question (int id = 0, string label = null, int points = 0, string status = null, 
                    string language = null, string publisherMAC = null, string nickname = null, 
                    string date = null, int voteY_N = 0, bool reported = false, bool used = false, bool downloaded = false)
    {
        this.id = id;
        this.label = label;
        this.points = points;
        this.status = status;
        this.language = language;
        this.publisherMAC = publisherMAC;
        this.nickname = nickname;
        this.date = date;
        this.voteY_N = voteY_N;
        this.reported = reported;

        this.used = used;
        this.downloaded = downloaded;

    }


    public void setQuestion(Question newQuestion)
    {
        this.label = newQuestion.label;
        this.points = newQuestion.points;
        this.status = newQuestion.status;
        this.language = newQuestion.language;
        this.publisherMAC = newQuestion.publisherMAC;
        this.nickname = newQuestion.nickname;
        this.date = newQuestion.date;
        this.voteY_N = newQuestion.voteY_N;
        this.used = newQuestion.used;
        this.downloaded = newQuestion.downloaded;
    }
    public int getVoteY_N()
    {
        return this.voteY_N;
    }

    public void setVoteY_N(int voteY_N)
    {
        this.voteY_N = voteY_N;
    }

    public int getId()
    {
        return this.id;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public string getLabel()
    {
        return this.label;
    }

    public void setLabel(string label)
    {
        this.label = label;
    }

    public int getPoints()
    {
        return this.points;
    }

    public void setPoints(int points)
    {
        this.points = points;
    }

    public string getStatus()
    {
        return this.status;
    }

    public void setStatus(string status)
    {
        this.status = status;
    }

    public string getLanguage()
    {
        return this.language;
    }

    public void setLanguage(string language)
    {
        this.language = language;
    }


}
