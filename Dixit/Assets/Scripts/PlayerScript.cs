using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Text scoreboard;
    public QuestionScript question;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        question = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        scoreboard = GetComponent<Text>();
    }


    public void ShowAnswers(List<Card> answers)
    {
        foreach (Card answer in answers)
        {
            //Card c = Instantiate(card, new Vector3(0,0,0), Quaternion.identity);
            //c.cardID = answer.cardID;
            //c.textField.text = answer.Answer;
            //c.PlayerObject = answer.PlayerObject;
            //c.votePhase = true;
            //c.IsCorrect = answer.IsCorrect;
        }
    }

    public void UpdateScores(List<Player> players)
    {
        foreach (Player p in players)
        {
            scoreboard.text += p.Score + "\t" + p.PlayerName + "\n";
        }
    }
}
