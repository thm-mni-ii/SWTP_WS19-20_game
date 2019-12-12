/* created by: SWT-P_WS_19/20_Game */
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
    public TextMeshProUGUI phaseText;
    public PlayerManager pm;
    public List<Card> vote;
    public Boolean votePhase;
    int aufruf = 1;
    //List<CardScript> answerCards;
    // Start is called before the first frame update
    void Start()
    {
     pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
     vote = new List<Card>();

    }

    void Update()
    {
       if(votePhase==true)
        if (Input.GetKeyDown(KeyCode.Return))
        {

                aufruf += 1;
                pm.RegisterEqualVote(vote, this.player); 
                //Debug.Log(vote[0].CorrectVotes);
                //Debug.Log(vote[1].CorrectVotes);
                votePhase = false;
        }
    }
    void Awake()
    {
        question = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        scoreboard = GetComponent<Text>();
    }


    public void StartAnswerPhase()
    {
        phaseText.text = "Antwortphase: \nBitte klicke auf die Karte, welche du als richtig erachtest";
        CardScript[] cs = GetComponents<CardScript>();
        foreach (CardScript card in cs)
        {
            Debug.Log(cs);
            card.answerPhase = true;
            card.isAllreadyVoted = false;
        }
    }


    public void ShowAnswers(List<Card> answers)
    {

        int i = 0;
        float offset = -4;
        foreach (Card answer in answers)
        {
            CardScript c;
            c = Instantiate(card, card.transform.position, Quaternion.identity);
            //c.textField = GetComponent<TMP_Text>();
            //if(i==0)
            c.card = new Card();
            c.textField = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
           /* if(i==1)
            c.textField = GameObject.FindGameObjectWithTag("Text2").GetComponent<TMP_Text>();
            */
            c.transform.position = new Vector3(card.transform.position.x + offset, card.transform.position.y, card.transform.position.z);
            c.transform.Rotate(new Vector3(270,0,0));
            c.card = answer;
            c.card.cardID = answer.cardID;
            c.card.Answer = answer.Answer;
            c.card.PlayerObject = answer.PlayerObject;
            if(c.card.PlayerObject!=null)
            Debug.Log("player:" + c.card.PlayerObject);


            //c.card.cardID = answer.cardID;
            // if(c.card.PlayerObject!=null)
            // Debug.Log("pid: " + c.card.PlayerObject.playerID);
            //Debug.Log("vorher :" + answer.Answer);
            //c.textField = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();

            //GetComponent<TMP_Text>();
            //c.textField.text = answer.Answer;
            c.card.CorrectVotes = 0;
            c.answerGiven = true;
           // Debug.Log("Textfield: " + c.textField.text);
            c.votePhase = true;
            if (answers.Count == 2)
            {
                offset += 7;
            }
            
        }
        phaseText = GameObject.FindGameObjectWithTag("PhaseUI").GetComponent<TextMeshProUGUI>();
        phaseText.text = "Votingphase: \nBitte Klicke Karten an die du als gleichwertig erachtest und drücke dann enter";
        votePhase = true;
        vote = new List<Card>();


    }

    public void UpdateScores(List<Player> players)
    {
        foreach (Player p in players)
        {
            scoreboard.text += p.Score + "\t" + p.PlayerName + "\n";
        }
    }



}

