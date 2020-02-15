using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Question
{
    

    public Question(int questionID, string question, string answer) {
        this.questionID = questionID;
        this.question = question;
        correctAnswer = new Card();
        correctAnswer.Answer = answer;
    }
    public int questionID;
    public string question;
    public Card correctAnswer;
    //  List<string> playerGuesses;


    public List<QuestionScript> QuestionToQuestionScript(List<Question> question) {
        List<QuestionScript> questionList = new List<QuestionScript>();
        for(int i=0; i < question.Count; i++)
        {

           // questionList.Add(new QuestionScript(question[i].questionID, question[i].question, question[i].correctAnswer));
        }

        return questionList;
    }
   
}
