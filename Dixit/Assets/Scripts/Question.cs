/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Question
{
    /// <summary>
    /// ID of the question.
    /// </summary>
    public int questionID;
    /// <summary>
    /// The question to be ansered as string.
    /// </summary>
    public string question;
    /// <summary>
    /// A card with the correct answer to the question.
    /// </summary>
    public Card correctAnswer;

    
    /// <summary>
    /// Default constructor that creates an empty cardobject as answer.
    /// </summary>
    public Question()
    {
        correctAnswer = new Card();
        correctAnswer.cardID = -1;
        correctAnswer.Answer = null;
    }

    /// <summary>
    /// Constructor for question object.
    /// </summary>
    /// <param name="questionID">The Identification Number of the Question Object</param>
    /// <param name="question">The question as a string.</param>
    /// <param name="answer">The answer to the question as a string.</param>
    public Question(int questionID, string question, string answer) {
        this.questionID = questionID;
        this.question = question;
        correctAnswer = new Card();
        correctAnswer.cardID = 99;
        correctAnswer.Answer = answer;
    }
}
