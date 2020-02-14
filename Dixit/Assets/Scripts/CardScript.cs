/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Mirror;

public class CardScript : NetworkBehaviour
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
    public Boolean answerPhase = false;
    public Boolean isAllreadyVoted = false;
    public PlayerScript ps;
    QuestionScript qs;
    // Start is called before the first frame update
    void Start()
    {
        
        qs = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        if (card == null)
        {
            card = new Card();
            SetCardFromPlayerScript(ps, card);
            // Debug.Log("setcardfromplayeryscxirpt");
        }
         if (votePhase == false)
        {
            textField = GameObject.FindGameObjectWithTag("TextTMP").GetComponent<TMP_Text>();
            Debug.Log("falseflase");
        }
        //ps.answerCards.Add(this);

        //textField = GetComponentInChildren<TMP_Text>();
        if (votePhase == true)
        {
            ps = GameObject.FindGameObjectWithTag("PlayerScript").GetComponent<PlayerScript>();
            //ps.answerCards.Add(this);
            textField = GetComponentInChildren<TMP_Text>();
            //textField = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
        }
        textField.text = card.Answer;

            Debug.Log("trueeeeeeee");
            // Debug.Log(PlayerManager.answers.Count + "start cs");

        
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
         if (answerPhase)
        {
            //Debug.Log("answerphase!");
        }
        else if (votePhase)
        {
            //textField.text = card.Answer;


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
                        ps.AnswerInc(card);
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
    /// <summary>
    /// This method destroys a "Card" tagged gameobject. If the game is neither in the votePhase nor is an answer given by the player, the SetCardFromPlayerScript method and the RegisterAnswer method are called. 
    /// </summary>
    public void TimeUP()
    {

        Debug.Log("timeup");
        if (!answerGiven && !votePhase)
        {

            SetCardFromPlayerScript(ps, card);
            answerGiven = true;
            ps.AnswerInc(card);
            //Debug.Log(PlayerManager.answers.Count);
            /*   for (int i = 0; i < PlayerManager.answers.Count; i++)
           {
               Debug.Log("TimeUP: answers." + PlayerManager.answers[i].cardID + ", correctvotes:" + PlayerManager.answers[i].CorrectVotes);
           }*/
        }
        //Destroy(gameObject);
            Debug.Log("timeup");
            Destroy(GameObject.FindGameObjectWithTag("Card"));
        
    }
    /*
    /// <summary>
    /// This method gets the TMP_Text component of textfield and sets in
    /// </summary>
    /// <param name="text"></param>
public void GetAndSetTMP_Text()
    {
        textField = GameObject.FindGameObjectWithTag("TextTMP").GetComponent<TMP_Text>();
        textField.text = card.Answer;
    }*/


    /// <summary>
    /// This method adds card to ps.vote if  votephase is true and if it is leftclicked by the mouse.
    /// If in answerphase and leftclicked by the mouse, it sets ps.voteCard = card.

    /// </summary>
    void OnMouseOver()
    {
        if (votePhase==true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isAllreadyVoted == false)
                {

                    if (ps.player != null)
                        Debug.Log("player:" + ps.player.playerID);
                    votePhase = false;
                    isAllreadyVoted = true;
                    answerGiven = true;
                    Debug.Log("OnMouseOver");
                    if(ps.vote!=null)
                    //Debug.Log("votecount" + ps.vote.Count);
                    ps.vote.Add(card);
                    //Debug.Log("votecount" + ps.vote.Count);


                }
            }
        }
        else if (answerPhase==true)
        {
            if (ps.player != null)
                //Debug.Log("player:" + ps.player.playerID);
            //Debug.Log("Answerphase");
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(isAllreadyVoted);

                if (isAllreadyVoted == false)
                {
                    // ps.vote = new List<Card>();
                    //ps.vote.Clear();
                    card.PlayerGuesses.Add(ps.player);
                    Debug.Log(ps.player);
                    Debug.Log(card.PlayerGuesses[0].playerID);
                    ps.voteCard = card;
                    isAllreadyVoted = true;
                    for (int i = 0; i < ps.answerCards.Count; i++)
                    {
                        ps.answerCards[i].answerPhase = false;
                    }
                    //Debug.Log(card.PlayerGuesses[0].playerID);
                    //Debug.Log(PlayerManager.answers[0].PlayerGuesses[0].playerID);
                }
            }

        }
    }

    



}
