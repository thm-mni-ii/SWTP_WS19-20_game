/* created by: SWT-P_WS_19/20_Game */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto26;
using UnityEditor;

[Serializable]
public class QuestionSet 
{
    //static QuestionSet active_qs = new QuestionSet();
    /// <summary>
    /// The Identification Number of the QuestionSet
    /// </summary>
    public int questionSetID;
    /// <summary>
    /// The List of all Questions of is QuestionSet
    /// </summary>
    public List<Question> questionList;

    /// <summary>
    /// Setter and Getter for the questionList
    /// </summary>
    public List<Question> QuestionList
    {
        get
        {
            return questionList;
        }

        set
        {
            questionList = value;
        }
    }

    /// <summary>
    /// Constructer for QuestionSet
    /// </summary>
    public QuestionSet ()
    {
        this.questionList = new List<Question>();
    }

    /// <summary>
    /// A Shuffle function to randomize the order of questions
    /// </summary>
    /// <param name="questSet">Parameter is of type QuestionSet.The member questionList of this QuestionSet will be shuffled.</param>
    void ShuffleList(QuestionSet questSet)
    {
        Debug.Log("shufflelist length" + questSet.questionList.Count);
        if (questSet.QuestionList == null)
        {
            Debug.Log("qs is null");
        } else
        {
            Debug.Log("qs is not null");
            Debug.Log("qs is not null 2");
            Debug.Log("qs is not null, answer is " + questSet.questionList[0].correctAnswer.Answer);
            Debug.Log("qs is not null 3");
        }
        for (int i = 0; i < questSet.QuestionList.Count; i++)
        {
            Question temp = questSet.QuestionList[i];
            int index = UnityEngine.Random.Range(i, questSet.QuestionList.Count);
            questSet.QuestionList[i] = questSet.QuestionList[index];
            questSet.QuestionList[index] = temp;
        }
    }

    /// <summary>
    /// A function to Remove a Specific Question from a QuestionSet. This function will be called when before the next question will be asked.
    /// </summary>
    /// <param name="index">the index of the list where the question shall be removed.</param>
    public void RemoveQuestionFromSet(int index)
    {
        this.QuestionList.RemoveAt(index);
    }

    /// <summary>
    /// Debug function to print out the whole QuestionSet as a string. The string will be formatted as Json.
    /// </summary>
    public void PrintOutQuestions()
    {
        string output;
  
        output = JsonConvert.SerializeObject(this);
        Debug.Log(" " + this + " :" + output);
    }

    /// <summary>
    /// This method requests a JSON file from a database and saves it as QuestionSet.
    /// The members or the Setter methods got to have the same corresponding names as the fields in the JSON file.
    /// This method may not be final, because it might be changed according to use with Mirror.
    /// </summary>
    /// <param name="setName">The name of the QuestionSet name</param>
    public void JsonToQuestionSet(string setName)
    {
        {
            RestClient.Get<QuestionSet>("https://dixit-db.firebaseio.com/QuestionSets/" + setName + ".json").Then(response =>
            {
                ShuffleList(response);
                GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                gm.questionSet = response;
            });
        }
    }

    /// <summary>
    /// Gets the next Question from questionList.
    /// </summary>
    /// <returns>The next Question.</returns>
    public Question GetNextQuestion()
    {
        Question nextQuestion = questionList[0];

        return nextQuestion;
    }
}