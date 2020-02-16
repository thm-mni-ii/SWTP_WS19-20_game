/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour
{
   
    public Text question;
    public TimerScript timer;
    Color textcolor;
    Question currentQuestion;
    public CardScript cs;
    public bool questionEnd;


   
    void Awake()
    {
        //cs = GameObject.FindGameObjectWithTag("Card").GetComponent<CardScript>();
        question = GetComponent<Text>();
        textcolor = question.color;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();
        questionEnd = false;
        
       // startQuestion(4, "scuur scurr");
    }


    /// <summary>
    /// This method sets the QuestionScript object to the same state as it was when awake was called. 
    /// </summary>
   public void InitializeQuestion()
    {
        //cs = GameObject.FindGameObjectWithTag("Card").GetComponent<CardScript>();
        question = GetComponent<Text>();
        textcolor = question.color;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();
        questionEnd = false;
    }
    void Update()
    {
        if (cs != null)
        {
  
        }
        if((timer.timeleft <= 0 ||cs.answerGiven==true )&& questionEnd==false)
        {
            questionEnd = true;
            endQuestion();
        }
    }

    /// <summary>
    /// This method starts the question and its corresponding timer.
    /// </summary>
    /// <param name="time">The starting time of the timer.</param>
    /// <param name="question">the question, which is to be answered.</param>
    public void startQuestion(float time, string question)
    {
        timer.setTimer(time);
        textcolor.a = 1f;
        this.question.text = question;
        this.question.color = textcolor;
        
    }

    /// <summary>
    /// This method sets the timer to null and calls, if not null, the timeUP method in CardScript.
    /// </summary>
    public void endQuestion()
    {

        textcolor.a = 0f;
        question.color = textcolor;
        timer.setTimer(0);
        if (cs != null)
        {
            cs.TimeUP();
        }
        /*for(Player player in players)
        {
            if(player.answer == correctAnswer)
            {
                player.increaseScore(10);
            }
        }*/
    }



    public Question GetQuestionFromQuestionSet(QuestionSet questionSet)
    {

        currentQuestion = questionSet.GetNextQuestion();
        currentQuestion.correctAnswer.cardID = 99;
        currentQuestion.correctAnswer.CorrectVotes = 0;
        question.text = currentQuestion.question;
        //questionSet.RemoveQuestionFromSet(0);
        return currentQuestion;
    }


}


