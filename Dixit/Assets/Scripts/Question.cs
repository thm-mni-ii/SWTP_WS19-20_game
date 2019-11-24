/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Question 
{
    public int questionID;
    private string question;
    private string correctAnswer;
    //private List<string> playerGuesses;

 

    public Question( int questionID,string question,string correctAnswer)
    {
        this.questionID = questionID;
        this.question = question;
        this.correctAnswer = correctAnswer;
        //this.playerGuesses = new List<string>();
    }


    public string GetQuestion
    {
        get
        {
            return question;
        }
        set
        {
            question = value;
        }
    }

    public string CorrectAnswer
    {
        get
        {
            return correctAnswer;
        }
        set
        {
            correctAnswer = value;
        }
    }
   /* public List<string> PlayerGuesses
    {
        get
        {
            return playerGuesses;
        }
        set
        {
            playerGuesses = value;
        }
    }*/


}
