using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class CardData
{
    /// <summary>
    /// The identifation of the card
    /// </summary>
    public int cardID;
    /// <summary>
    /// The answer of the player
    /// </summary>
    private string answer;
    /// <summary>
    /// The corresponding player object
    /// </summary>
    private Player playerObject;
    /// <summary>
    /// Number of votes, concerning if the given answer is equal to the correct answer.
    /// </summary>
    private int correctVotes;
    /// <summary>
    ///  Boolean to show if the Player chose for the correct answer.
    /// </summary>
    private bool isCorrect;
    /// <summary>
    /// A list of players who chose this card as the correct answer.
    /// </summary>
    private List<Player> playerGuesses;

    public CardData(int cardID)
    {
        this.cardID = cardID;
        this.playerGuesses = new List<Player>();
    }



    /// <summary>
    /// Getter/Setter for answer.
    /// </summary>
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
    /// <summary>
    /// Getter/Setter for playerObject.
    /// </summary>
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
    /// <summary>
    /// Getter/Setter for correctVotes.
    /// </summary>
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
    /// <summary>
    /// Getter/Setter for IsCorrect.
    /// </summary>
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
    /// <summary>
    /// Getter/Setter for playerGuesses.
    /// </summary>
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


    /// <summary>
    /// Method to add a specific Player to the playerGuesses List of the CardData Object.
    /// </summary>
    /// <param name="player">The player that will be added.</param>
    public void AddPlayerToPlayerGuesses(Player player)
    {
        this.PlayerGuesses.Add(player);
    }


}
