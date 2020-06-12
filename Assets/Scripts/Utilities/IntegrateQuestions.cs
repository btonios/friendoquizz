using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegrateQuestions : MonoBehaviour
{

    public TextAsset file;
    public void Start()
    {
        if(GlobalVariables.firstOpening == true)
            Integrate();
    }

    public void Integrate()
    {
        string[] lines = file.text.Split(char.Parse("\n"));
        
        foreach(string line in lines)
        {
            if(line != "")
            {
                string[] infos = line.Split(char.Parse("\\"));
                Question question = new Question(int.Parse(infos[0]), infos[1], 0, "added", infos[2]);
                GlobalVariables.SetQuestion(question);
            }
        }
    }
}
