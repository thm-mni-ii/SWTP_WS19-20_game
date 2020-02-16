/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CardScript : MonoBehaviour
{
    /// <summary>
    /// List of answercards.
    /// </summary>
    public List<CardScript> ansCards;
    /// <summary>
    /// Card object to this cardscript.
    /// </summary>
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
    /// Flag if the game is in votingphase.
    /// </summary>
    public Boolean votePhase = false;
    /// <summary>
    /// Flag if the game is in answerphase.
    /// </summary>
    public Boolean answerPhase = false;
    /// <summary>
    /// Playermanager of the game.
    /// </summary>
    public PlayerManager pm;
    /// <summary>
    /// Flag if this card was voted by the player.
    /// </summary>
    public Boolean isAllreadyVoted = false;
    /// <summary>
    /// Playerscript to the player.
    /// </summary>
    public PlayerScript ps;
    /// <summary>
    /// Border for voted cards.
    /// </summary>
    public GameObject selectionObject;
    /// <summary>
    /// The question that is asked at the moment.
    /// </summary>
    public QuestionScript qs;

    // Start is called before the first frame update
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
            Debug.Log("falseflase");
        }

        if (votePhase == true)
        {
            ansCards = new List<CardScript>();

          selectionObject.SetActive(false);

            ps = GameObject.FindGameObjectWithTag("PlayerScript").GetComponent<PlayerScript>();
            textField = GetComponentInChildren<TMP_Text>();
        }
        textField.text = card.Answer;
        Debug.Log("tst:" + textField.text);
    }

    // Update is called once per frame
    void Update()
    {
         if (answerPhase)
        {
            Debug.Log("answerphase!");
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
                    Debug.Log(card.Answer);
                }
            }
        }
    }

    public void SetCardFromPlayerScript(PlayerScript ps,Card card)
    {
        card.cardID = ps.player.playerID;
        card.PlayerObject = ps.player;
        if (ps.player == null)
            Debug.Log("error play 404");
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
            ps.CmdAnswerInc(card);
        }
            Debug.Log("timeup");
            foreach(GameObject cur in GameObject.FindGameObjectsWithTag("Card"))
            {
                Destroy(GameObject.FindGameObjectWithTag("Card"));
            }     
    }

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
                    isAllreadyVoted = true;
                    answerGiven = true;
                    Debug.Log("OnMouseOver");
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
                Debug.Log(isAllreadyVoted);

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
