using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour
{   
    Text question;
    //Card correctAnswer;
    //ArrayList<Player> players;
    TimerScript timer;
    Color textcolor;

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
}
