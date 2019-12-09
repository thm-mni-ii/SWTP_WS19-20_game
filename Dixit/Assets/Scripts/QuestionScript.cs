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

  
  
    void Awake()
    {

        question = GetComponent<Text>();
        textcolor = question.color;
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();

        startQuestion(4, "scuur scurr");
    }

    void Update()
    {
        if(timer.timeleft <= 0)
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

        /*for(Player player in players)
        {
            if(player.answer == correctAnswer)
            {
                player.increaseScore(10);
            }
        }*/
    }



    public void GetQuestionFromQuestionSet(QuestionSet questionSet)
    {

        question.text = questionSet.QuestionList[0].question;
        //Debug.Log(questionSet.QuestionList.Count);
        questionSet.RemoveQuestionFromSet(0);
        //Debug.Log(questionSet.QuestionList.Count);

    }


}


