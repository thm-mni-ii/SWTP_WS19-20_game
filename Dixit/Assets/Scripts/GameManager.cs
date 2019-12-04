using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string questionPath;
    public QuestionSet questionSet = new QuestionSet();
    QuestionScript questionScript;
    PlayerManager pm;
    void Start()
    {
        Debug.Log("hallo");
   

        //überarbeiten da correct answer in questionScript vom ytp card ist, -> einlesen geht nicht so einfahc wie mit vorherigen question 
        //abfangen falls questionpath null;
        questionSet = questionSet.LoadQuestionSet(questionPath);
        pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        questionScript = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        questionScript.GetQuestionFromQuestionSet(questionSet);
        Debug.Log("OK");
        questionSet.PrintOutQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RegisterVotes(List<Card> answers)
    {

    }

    void GiveOutPoints()
    {

    }

    void RoundEnd()
    {

    }

}


