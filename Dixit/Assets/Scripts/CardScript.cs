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
    private Boolean answerGiven = false;
    /// <summary>
    /// Flag if card is clickable.
    /// </summary>
    public Boolean votePhase = false;
    public PlayerManager pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();

    }

    // Update is called once per frame
    void Update()
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

    void TimeUP()
    {
        answerGiven = true;
        pm.RegisterAnswer(card);
    }
}
