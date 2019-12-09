using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour
{
   
    public Text question;
    //ArrayList<Player> players;
    TimerScript timer;
    Color textcolor;
    Question currentQuestion;

  
  
    void Awake()
    {

        question = GetComponent<Text>();
        textcolor = question.color;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();
        //Zu Testzwecken
        startQuestion(20f);
    }

    void Update()
    {
        if(timer.timeleft <= 0)
        {
            endQuestion();
        }
    }

    void startQuestion(float time/*, Question question*/)
    {
        timer.setTimer(time);
        textcolor.a = 1f;
        question.color = textcolor;
    }

    void endQuestion()
    {
        textcolor.a = 0f;
        question.color = textcolor;

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


