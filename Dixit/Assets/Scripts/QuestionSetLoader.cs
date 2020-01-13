/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



public class QuestionSetLoader : MonoBehaviour
{



    [SerializeField]
    public string questionPath;
    
    public string qp;

    void ShuffleList(QuestionSet questSet)
    {
        for (int i = 0; i < questSet.QuestionList.Count; i++)
        {
            Question temp = questSet.QuestionList[i];
            int index = UnityEngine.Random.Range(i, questSet.QuestionList.Count);
            questSet.QuestionList[i] = questSet.QuestionList[index];
            questSet.QuestionList[index] = temp;
        }
    }

    //Method eget QuestionSet-> liest Json ein-> zu QuestionSet
    //zu implementieren

    //remove Questionfromset

    //

    public void RemoveQuestionFromSet(QuestionSet questSet, int questionID)
    {
        questSet.QuestionList.RemoveAt(questionID);
    }


    public void PrintOutQuestions(QuestionSet questionSet)
    {
        string output;

        output = JsonConvert.SerializeObject(questionSet);
        Debug.Log(" " + questionSet + " :" + output);

    }



    //Methode verwendet momentan ein pfad zur json datei, wenn die Datei später über
    QuestionSet JsonToQuestionSet(string path)
    {
        using (StreamReader reader = File.OpenText(@"" + path))
        {

            QuestionSet questionSet = new QuestionSet();
            JObject on = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            string json = on.ToString();
            questionSet = JsonConvert.DeserializeObject<QuestionSet>(json);
            return questionSet;

        }
    }


    public QuestionSet LoadQuestionSet(string path)
    {
        QuestionSet questionSet = JsonToQuestionSet(path);
        ShuffleList(questionSet);
        return questionSet;
    }
}
