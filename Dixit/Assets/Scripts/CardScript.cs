/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CardScript : MonoBehaviour
{
    /// <summary>
    /// List of answer cards.
    /// </summary>
    public List<CardScript> ansCards;
    /// <summary>
    /// Card object to this CardScript.
    /// </summary>
    public Card card;
    /// <summary>
    /// Text field to display this card's answer text.
    /// </summary>
    public TMP_Text textField;
    /// <summary>
    /// Flag if input is finished.
    /// </summary>
    public Boolean answerGiven = false;
    /// <summary>
    /// Flag if the game is in voting phase.
    /// </summary>
    public Boolean votePhase = false;
    /// <summary>
    /// Flag if the game is in answer phase.
    /// </summary>
    public Boolean answerPhase = false;
    /// <summary>
    /// PlayerManager of the game.
    /// </summary>
    public PlayerManager pm;
    /// <summary>
    /// Flag if this card was voted by the player.
    /// </summary>
    public Boolean isAllreadyVoted = false;
    /// <summary>
    /// PlayerScript to the player.
    /// </summary>
    public PlayerScript ps;
    /// <summary>
    /// Border for voted cards.
    /// </summary>
    public GameObject selectionObject;
    /// <summary>
    /// The question that is asked in the current round.
    /// </summary>
    public QuestionScript qs;

    /// <summary>
    /// Prepares the CardScript by initializing it's variables.
    /// This method is calles once on startup.
    /// </summary>
    void Start()
    {
        qs = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        if (card == null)
        {
            card = new Card();
            SetCardFromPlayerScript(ps, card);
        }
        if (votePhase == false)
        {
            textField = GameObject.FindGameObjectWithTag("TextTMP").GetComponent<TMP_Text>();
        }
        if (votePhase == true)
        {
            ansCards = new List<CardScript>();
            selectionObject.SetActive(false);
            ps = GameObject.FindGameObjectWithTag("PlayerScript").GetComponent<PlayerScript>();
            textField = GetComponentInChildren<TMP_Text>();
        }
        textField.text = card.Answer;
    }

    /// <summary>
    /// Sets the players input as answer of his card.
    /// This method is called every frame.
    /// </summary>
    void Update()
    {
        if (answerPhase)
        {
        }
        else if (votePhase)
        {
        }
        else
        {
            foreach (char c in Input.inputString)
            {
                if (!answerGiven && !votePhase)
                {
                    if (Input.inputString == "\r")
                    {
                        qs = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
                        qs.questionEnd = true;
                        card.PlayerObject = ps.player;
                        qs.endQuestion();
                    }
                    else if (Input.inputString == "\b" && card.Answer.Length > 0)
                    {
                        card.Answer = card.Answer.Substring(0, card.Answer.Length - 1);
                    }
                    else{
                        card.Answer = card.Answer + c;
                    }
                    textField.text = card.Answer;
                }
            }
        }
    }

    //TODO
    /// <summary>
    /// Sets the cards playerobject to the localplayer.
    /// </summary>
    /// <param name="ps">The PlayerScript containing the localplayer</param>
    /// <param name="card">The Card whichs playerobject will be set to the localplayer.</param>
    public void SetCardFromPlayerScript(PlayerScript ps,Card card)
    {
        card.cardID = ps.player.playerID;
        card.PlayerObject = ps.player;
        if (ps.player == null)
            Debug.Log("error play 404");
    }

    /// <summary>
    /// This method destroys a "Card" tagged gameobject. 
    /// If the game is neither in the votePhase nor is an answer given by the player, the SetCardFromPlayerScript method and the RegisterAnswer method are called.
    /// This method calls the CmdAnswerInc via PlayerScript ps, so that it will be executed on the Server.
    /// </summary>
    public void TimeUP()
    {
        if (!answerGiven && !votePhase)
        {
            SetCardFromPlayerScript(ps, card);
            answerGiven = true;
            ps.CmdAnswerInc(card);
        }
            foreach(GameObject cur in GameObject.FindGameObjectsWithTag("Card"))
            {
                Destroy(GameObject.FindGameObjectWithTag("Card"));
            }     
    }

    /// <summary>
    /// This method adds card to ps.vote if votephase is true and if it is left clicked by the mouse.
    /// If in answerphase and left clicked by the mouse, it sets ps.voteCard = card.
    /// This Method is also responsible for the visual marking of the cardobjects via selectionObject.
    /// </summary>
    void OnMouseOver()
    {
        if (votePhase==true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isAllreadyVoted == false)
                {
                    isAllreadyVoted = true;
                    answerGiven = true;
                    if (ps.vote != null)
                    {
                        ps.vote.Add(card);
                    }
                    selectionObject.SetActive(true);
                }
                else if (isAllreadyVoted == true)
                {
                    selectionObject.SetActive(false);

                    for (int i = 0; i < ps.vote.Count; i++)
                    {
                        if (ps.vote[i].answer == this.card.answer)
                        {
                            ps.vote.RemoveAt(i);
                            answerGiven = false;
                            isAllreadyVoted = false;

                            break;
                        }
                    }
                }
            }
        }
        else if (answerPhase==true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isAllreadyVoted == false)
                {
                    foreach (CardScript card in ansCards)
                    {
                        card.selectionObject.SetActive(false);
                        isAllreadyVoted = false;
                    }
                    this.selectionObject.SetActive(true) ;
                    ps.voteCard = card;
                }
            }

        }
    }

    



}
