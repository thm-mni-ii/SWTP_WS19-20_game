/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour
{
   
    public Text question;
    TimerScript timer;
    Color textcolor;
    Question currentQuestion;
    CardScript cs;
  
  
    void Awake()
    {
        cs = GameObject.FindGameObjectWithTag("Card").GetComponent<CardScript>();
        question = GetComponent<Text>();
        textcolor = question.color;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();
        
       // startQuestion(4, "scuur scurr");
    }

    void Update()
    {
        if((timer.timeleft <= 0 ||cs.answerGiven==true ))
        {
            Debug.Log("questionend");
            endQuestion();
        }
    }

    public void startQuestion(float time, string question)
    {
        timer.setTimer(time);
        textcolor.a = 1f;
        this.question.text = question;
        this.question.color = textcolor;
        Debug.Log("questionstart");
    }

    void endQuestion()
    {
        textcolor.a = 0f;
        question.color = textcolor;
        timer.setTimer(0);
        if (cs != null)
        {
            Debug.Log("time up called in endquestion");
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
        //Debug.Log(questionSet.QuestionList.Count);
        //questionSet.RemoveQuestionFromSet(0);
        //Debug.Log(questionSet.QuestionList.Count);
        return currentQuestion;
    }


}


