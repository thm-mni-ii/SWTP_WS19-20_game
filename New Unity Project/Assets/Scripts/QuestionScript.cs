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
        if(timer.timeleft <= 0 ||cs.answerGiven==true)
        {
            endQuestion();
        }
    }

    public void startQuestion(float time, string question)
    {
        timer.setTimer(time);
        textcolor.a = 1f;
        this.question.text = question;
        this.question.color = textcolor;
    }

    void endQuestion()
    {
        textcolor.a = 0f;
        question.color = textcolor;
        timer.setTimer(0);
        cs.TimeUP();
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
        question.text = currentQuestion.question;
        //Debug.Log(questionSet.QuestionList.Count);
        //questionSet.RemoveQuestionFromSet(0);
        //Debug.Log(questionSet.QuestionList.Count);
        return currentQuestion;
    }


}


