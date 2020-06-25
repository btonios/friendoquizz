using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

                //GlobalVariables.AddQuestion but without saving data at the end (better performance)
                if(GlobalVariables.questionList.Any(q=>q.id == question.id))
                {
                    foreach(Question questionInList in GlobalVariables.questionList)
                    {
                        if(questionInList.id == question.id) questionInList.setQuestion(question);
                    }
                }
                else
                {
                    GlobalVariables.questionList.Add(question);
                }
            }
        }

        SaveData.SaveQuestions();
    }
}
