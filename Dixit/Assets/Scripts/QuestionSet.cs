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
    /// This method requests a JSON file from a database and saves it as QuestionSet.
    /// The members or the Setter methods got to have the same corresponding names as the fields in the JSON file.
    /// This method may not be final, because it might be changed according to use with Mirror.
    /// </summary>
    /// <param name="setName">The name of the QuestionSet name</param>
    public void JsonToQuestionSet(string setName)
    {
        //active_qs = new QuestionSet();
        {
            //qs = new QuestionSet();
            RestClient.Get<QuestionSet>("https://dixit-db.firebaseio.com/QuestionSets/" + setName + ".json").Then(response =>
            {
                EditorUtility.DisplayDialog("JSON", JsonUtility.ToJson(response, true), "Ok");
                Debug.Log("response list length " + response.questionList.Count);
                //Debug.Log("questionset list length " + active_qs.QuestionList.Count);
                Debug.Log("SETNAME: " + setName);
                ShuffleList(response);
                GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                //QuestionSet qs = GameObject.Instantiate(response);
                //string json = JsonConvert.SerializeObject(response);
                //Debug.Log("JSON STRING " + json);
                gm.questionSet = response;
                //gm.questionSet = JsonConvert.DeserializeObject<QuestionSet>(json);
                //Debug.Log("ACTIVEQS " + active_qs.questionList.Count);
                //gm.questionSet = active_qs;
                Debug.Log("GM QS in JSON " + gm.questionSet.questionList.Count);
                //gm.questionSet = response;
            });
        }
        /*RestClient.Get<QuestionSet>("https://dixit-db.firebaseio.com/QuestionSets/Informatik.json").Then(response =>
       {
           EditorUtility.DisplayDialog("JSON", JsonUtility.ToJson(response, true), "Ok");
           questionSet = response;
           Debug.Log("response list length " + response.questionList.Count);
           Debug.Log("questionset list length " + questionSet.QuestionList.Count);
           Debug.Log("SETNAME: " + setName);
       });*/

        /*RestClient.GetArray<Question>("https://dixit-db.firebaseio.com/QuestionSets/" + setName + "/questionList.json").Then(responseQuestionList =>
        { 
            EditorUtility.DisplayDialog("QUESTION", JsonHelper.ArrayToJsonString<Question>(responseQuestionList, true), "Ok");

            for(int i = 0; i < responseQuestionList.Length; i++)
            {
                RestClient.Get<Card>("https://dixit-db.firebaseio.com/QuestionSets/" + setName + "/questionList/" + responseQuestionList[i].questionID + "/correctAnswer.json").Then(responseCard =>
                //RestClient.Get<Card>("https://dixit-db.firebaseio.com/QuestionSets/Informatik/questionList/0/correctAnswer.json").Then(responseCard =>
                {

                    Debug.Log("CARD ANSWER" + responseCard.answer);
                    Debug.Log("CARD CARDID" + responseCard.cardID);
                    Debug.Log("CARD ISCORRECT" + responseCard.IsCorrect);
                    Debug.Log("CARD CORRECTVOTES" + responseCard.CorrectVotes);
                    //EditorUtility.DisplayDialog("CARD", JsonHelper.ArrayToJsonString<Card>(responseCard, true), "Ok");
                    //EditorUtility.DisplayDialog("CARD", JsonUtility.ToJson(responseCard, true), "Ok");
                    //responseQuestionList[i].correctAnswer = responseCard;

                });
            }
        });*/


        //using (StreamReader reader = File.OpenText(@"" + path))
        //{

            
            //JObject on = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            //string json = on.ToString();
            //questionSet = JsonConvert.DeserializeObject<QuestionSet>(json);
            //return questionSet;

        //}
    }

    /// <summary>
    /// A method to create a new QuestionSet from a JSON File under a specific Path.
    /// The QuestionSet will also be shuffled.
    /// </summary>
    /// <param name="path">The path to the JSOn File.</param>
    /// <returns>The loaded QuestionSet questionSet.</returns>
    /*public QuestionSet LoadQuestionSet(string setName)
    {
        //QuestionSet qs = new QuestionSet();
        Debug.Log("LoadQS " + active_qs.questionList.Count);
        //JsonToQuestionSet(setName, active_qs);
        System.Threading.Thread.Sleep(5000);
        Debug.Log("LoadQS2 " + active_qs.questionList.Count);
        ShuffleList(active_qs);
        return active_qs;
    }*/


    /// <summary>
    /// Gets the next Question from questionList.
    /// </summary>
    /// <returns>The next Question.</returns>
    public Question GetNextQuestion()
    {
        Debug.Log("question list length" + questionList.Count);
        Question nextQuestion = questionList[0];

        return nextQuestion;
    }

}