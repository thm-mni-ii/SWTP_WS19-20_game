/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour
{
    /// <summary>
    /// Text where the question is displayed
    /// </summary>
    public Text question;
    /// <summary>
    /// Timer for the question.
    /// </summary>
    public TimerScript timer;
    /// <summary>
    /// Textcolor is used to regulate the transparency of the displayed text.
    /// </summary>
    Color textcolor;
    /// <summary>
    /// The currently displayed question object.
    /// </summary>
    Question currentQuestion;
    /// <summary>
    /// Card, that the player uses to answer the question.
    /// </summary>
    public CardScript cs;
    /// <summary>
    /// Flag that indicates if the question has ended.
    /// </summary>
    public bool questionEnd;

    void Awake()
    {
        question = GetComponent<Text>();                                                //finds the textfield where the question is written
        textcolor = question.color;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();  //finds the timerscript
        questionEnd = false;
    }


    /// <summary>
    /// This method sets the QuestionScript object to the same state as it was when awake was called. 
    /// </summary>
    public void InitializeQuestion()
    {
        question = GetComponent<Text>();
        textcolor = question.color;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();
        questionEnd = false;
    }

    void Update()
    {
        if (cs != null)
        {
            //Debug.Log("questionENd:" + questionEnd);
            //Debug.Log(cs.card.Answer);
        }
        if((timer.timeleft <= 0 ||cs.answerGiven==true )&& questionEnd==false)
        {
            //Debug.Log("questionend");
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
    }

    /// <summary>
    /// This method delivers the next question.
    /// </summary>
    /// <param name="questionSet">The questionset from which the next question is requested.</param>
    /// <returns>The next question from the set</returns>
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
