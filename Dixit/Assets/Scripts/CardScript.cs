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
        Debug.Log(PlayerManager.answers.Count + "start cs");

        if (card == null)
        {
            card = new Card();
            SetCardFromPlayerScript(ps, card);
        }
        if(votePhase==false)
            textField =  GameObject.FindGameObjectWithTag("TextTMP").GetComponent<TMP_Text>();
        //textField = GetComponentInChildren<TMP_Text>();
        if (votePhase == true)
        {
            textField = GetComponentInChildren<TMP_Text>();
            //textField = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
            textField.text = card.Answer;
            Debug.Log(PlayerManager.answers.Count + "start cs");

        }
        Debug.Log("tst:" + textField.text);
    }
    public void SetCardFromPlayerScript(PlayerScript ps,Card card)
    {
        card.cardID = ps.player.playerID;
        card.PlayerObject = ps.player;
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
        SetCardFromPlayerScript(ps, card);
        answerGiven = true;
        pm.RegisterAnswer(card);
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
                    card.CorrectVotes = 1;
                    ps.vote.Add(card);
                    Debug.Log("add");
                    Debug.Log(isAllreadyVoted);
                    Debug.Log(card.CorrectVotes);
                    isAllreadyVoted = true;
                }
            }
        }
    }

    



}
