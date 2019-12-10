using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Text scoreboard;
    public QuestionScript question;
    public CardScript card;
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


    CardScript c;
    public void ShowAnswers(List<Card> answers)
    {
        int i = 0;
        float offset = -4;
        foreach (Card answer in answers)
        {
            
             c = Instantiate(card, card.transform.position, Quaternion.identity);
            c.transform.position = new Vector3(card.transform.position.x + offset, card.transform.position.y, card.transform.position.z);
            c.transform.Rotate(new Vector3(270,0,0));
            c.card.cardID = answer.cardID;
            Debug.Log("vorher :" + answer.Answer);
           // textField = GameObject.FindGameObjectWithTag("TextTMP").GetComponent<TMP_Text>();

            c.textField = GameObject.FindGameObjectWithTag("TextTMP").GetComponent<TMP_Text>();
            //GetComponent<TMP_Text>();
            c.textField.text = answer.Answer;
            c.answerGiven = true;
            Debug.Log("Textfield: " + c.textField.text);
            c.card = answer;
            c.card.PlayerObject = answer.PlayerObject;
            c.votePhase = true;
            c.card.IsCorrect = answer.IsCorrect;
            if (answers.Count == 2)
            {
                offset += 7;
            }
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
