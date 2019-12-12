/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CardScript : MonoBehaviour
{


    public Card card;
    /// <summary>
    /// Textfield to display this cards Answe text.
    /// </summary>
    public TMP_Text textField;
    /// <summary>
    /// Flag if input is finished.
    /// </summary>
    public Boolean answerGiven = false;
    /// <summary>
    /// Flag if card is clickable.
    /// </summary>
    public Boolean votePhase = false;
    public PlayerManager pm;
    Boolean isAllreadyVoted = false;
    PlayerScript ps;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        ps = GameObject.FindGameObjectWithTag("PlayerScript").GetComponent<PlayerScript>();

        if (card == null)
        {
            card = new Card();
            SetCardFromPlayerScript(ps, card);
           // Debug.Log("setcardfromplayeryscxirpt");
        }
        if(votePhase==false)
            textField =  GameObject.FindGameObjectWithTag("TextTMP").GetComponent<TMP_Text>();
        //textField = GetComponentInChildren<TMP_Text>();
        if (votePhase == true)
        {
            textField = GetComponentInChildren<TMP_Text>();
            //textField = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
            textField.text = card.Answer;
           // Debug.Log(PlayerManager.answers.Count + "start cs");

        }
        Debug.Log("tst:" + textField.text);
    }
    public void SetCardFromPlayerScript(PlayerScript ps,Card card)
    {
        card.cardID = ps.player.playerID;
        card.PlayerObject = ps.player;
        if (ps.player == null)
            Debug.Log("error play 404");
    }



    // Update is called once per frame
    void Update()
    {
        if (votePhase == true)
        {
           
            

            //textField.text=
            //textField.text = card.Answer;
          //  Debug.Log(textField.text);
        }
        else
        {

            foreach (char c in Input.inputString)
            {
                if (!answerGiven && !votePhase)
                {
                    if (Input.inputString == "\r")
                    {
                        answerGiven = true;
                        card.PlayerObject = ps.player;
                        pm.RegisterAnswer(card);
                    }
                    else if (Input.inputString == "\b" && card.Answer.Length > 0)
                        card.Answer = card.Answer.Substring(0, card.Answer.Length - 1);
                    else
                        card.Answer = card.Answer + c;
                    textField.text = card.Answer;
                    Debug.Log(card.Answer);
                }
            }
        }
    }

    public void TimeUP()
    {

        if (!answerGiven && !votePhase)
        {

            SetCardFromPlayerScript(ps, card);
            answerGiven = true;
            pm.RegisterAnswer(card);
            //Debug.Log(PlayerManager.answers.Count);
            /*   for (int i = 0; i < PlayerManager.answers.Count; i++)
           {
               Debug.Log("TimeUP: answers." + PlayerManager.answers[i].cardID + ", correctvotes:" + PlayerManager.answers[i].CorrectVotes);
           }*/
        }
            //Destroy(gameObject);
            Destroy(GameObject.FindGameObjectWithTag("Card"));
        
    }

public void GetAndSetTMP_Text(string text)
    {
        textField = GameObject.FindGameObjectWithTag("TextTMP").GetComponent<TMP_Text>();
        textField.text = card.Answer;
    }

    void OnMouseOver()
    {
        if (votePhase)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isAllreadyVoted == false)
                {

                    if (ps.player != null)
                        Debug.Log("player:" + ps.player.playerID);
                    isAllreadyVoted = true;
                    answerGiven = true; 
                    ps.vote.Add(card);
                 
                   
                }
            }
        }
    }

    



}
