using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class CardData
{
    //the identifation of the card
    public int cardID;
    //the answer of the player
    private string answer;
    //the corresponding player bject
    private Player playerObject;
    //is the answer equal to the correct answer
    //
    private int correctVotes;
    //is the vote of the player correct
    private bool isCorrect;
    //A list of which players havevoted for this card.
    private List<Player> playerGuesses;

   public CardData(int cardID)
    {
        this.cardID = cardID;
        this.playerGuesses = new List<Player>();
    }




    public string Answer
    {
        get
        {
            return answer;
        }

        set
        {
            answer = value;
        }
    }

    public Player PlayerObject
    {
        get
        {
            return playerObject;
        }

        set
        {
            playerObject = value;
        }
    }
    public int CorrectVotes
    {
        get
        {
            return correctVotes;
        }

        set
        {
            correctVotes = value;
        }
    }
    public bool IsCorrect
    {
        get
        {
            return isCorrect;
        }

        set
        {
            isCorrect = value;
        }
    }
    public List<Player> PlayerGuesses
    {
        get
        {
            return playerGuesses;
        }

        set
        {
            playerGuesses = value;
        }
    }


    public void AddPlayerToPlayerGuesses(CardData cardData, Player player)
    {
        cardData.PlayerGuesses.Add(player);
    }


}
