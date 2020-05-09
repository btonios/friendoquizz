using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class Question 
{ 	
    public int id;
    public string label;
    public int points;
    public string status;
    public string language;

    public Question (int id = 0, string label = null, int points = 0, string status = null, string language = null)
    {
        this.id = id;
        this.label = label;
        this.points = points;
        this.status = status;
        this.language = language;
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
