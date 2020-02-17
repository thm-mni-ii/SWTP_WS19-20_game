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

    /// <summary>
    /// Initializes variables of the QuestionScript.
    /// This method is called once on startup.
    /// </summary>
    void Awake()
    {
        question = GetComponent<Text>();                                                //finds the textfield where the question is written
        textcolor = question.color;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();  //finds the timerscript
        questionEnd = false;
    }


    /// <summary>
    /// This method sets the QuestionScript object to the same state as it was when awake was called. 
    /// Initializes variables of the QuestionScript.
    /// </summary>
    public void InitializeQuestion()
    {
        question = GetComponent<Text>();
        textcolor = question.color;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();
        questionEnd = false;
    }

    /// <summary>
    /// Checks if time has run out for a question and if so, ends the answer phase.
    /// This method is called once per frame.
    /// </summary>
    void Update()
    {
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
    /// This method sets the timer to 0 and calls, if not cs is not null, the timeUP method in CardScript cs.
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
        return currentQuestion;
    }
}
