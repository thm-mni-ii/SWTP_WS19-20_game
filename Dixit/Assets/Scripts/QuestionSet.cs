/* created by: SWT-P_WS_19/20_Game */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[Serializable]
public class QuestionSet 
{
    /// <summary>
    /// The Identification Number of the QuestionSet
    /// </summary>
    public int questionSetID;
    /// <summary>
    /// The List of all Questions of is QuestionSet
    /// </summary>
    private List<Question> questionList;

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
        for (int i = 0; i < questSet.QuestionList.Count; i++)
        {
            Question temp = questSet.QuestionList[i];
            int index = UnityEngine.Random.Range(i, questSet.QuestionList.Count);
            questSet.QuestionList[i] = questSet.QuestionList[index];
            questSet.QuestionList[index] = temp;
        }
    }

  
    //
    /// <summary>
    /// A function to Remove a Specific Question from a QuestionSet. This function will be called when before the next question will be asked.
    /// </summary>
    /// <param name="questSet">The QuestionSet, from which the Question shall be removed.</param>
    /// <param name="index">the index of the list where the question shall be removed.</param>
    public void RemoveQuestionFromSet(int index)
    {
        this.QuestionList.RemoveAt(index);
    }

    /// <summary>
    /// Debug function to print out the whole QuestionSet as a string. The string will be formatted as Json.
    /// </summary>
    /// <param name="questionSet">The QuestionSet </param>
    public void PrintOutQuestions()
    {
        string output;
  
        output = JsonConvert.SerializeObject(this);
        Debug.Log(" " + this + " :" + output);

    }



    //Methode verwendet momentan ein pfad zur json datei, wenn die Datei später über

    /// <summary>
    /// This method reads a JSON File from a specific path and creates a new QuestionSet from that file.
    /// The members or the Setter methods got to have the same corresponding names as the fields in the JSON file.
    /// This method may not be final, because it might be changed according to use with Mirror.
    /// </summary>
    /// <param name="path">A pathto the json file.</param>
    /// <returns></returns>
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

    /// <summary>
    /// A method to create a new QuestionSet from a JSON File under a specific Path.
    /// The QuestionSet will also be shuffled.
    /// </summary>
    /// <param name="path">The path to the JSOn File.</param>
    /// <returns></returns>
    public QuestionSet LoadQuestionSet(string path)
    {
        QuestionSet questionSet = JsonToQuestionSet(path);
        ShuffleList(questionSet);
        return questionSet;
    }

    public Question GetNextQuestion()
    {
        Question nextQuestion = QuestionList[0];
        RemoveQuestionFromSet(0);

        return nextQuestion;
    }

}