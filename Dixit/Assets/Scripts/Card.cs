/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
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

    /// <summary>
    /// Constructor for a Card Object.
    /// This Constructor initializes the List playerGuesses.
    /// </summary>
    /// <param name="cardID">The identification number of this card.</param>
    public Card(int cardID)
    {
        this.cardID = cardID;
        this.playerGuesses = new List<Player>();
    }
    /// <summary>
    /// Getter/Setter for answer. 
    /// The Setter automatically converts the String to UpperCase.
    /// </summary>
    public string Answer
    {
        get
        {
            return answer.ToUpper();
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



    

}
