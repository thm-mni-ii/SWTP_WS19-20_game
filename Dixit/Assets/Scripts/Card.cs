/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;

[Serializable]
public class Card
{
    /// <summary>
    /// The identifation of the card
    /// </summary>
    public int cardID;
    /// <summary>
    /// The answer of the player
    /// </summary>
    private string answer = "";
    /// <summary>
    /// The corresponding player object
    /// </summary>
    private Player playerObject;
    /// <summary>
    /// Number of votes, concerning if the given answer is equal to the correct answer.
    /// </summary>
    private int correctVotes;
    /// <summary>
    ///  Boolean to show it is the correct answer.
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
    /// 

     
    public Card()
    {
        this.playerGuesses = new List<Player>();

    }
    /// <summary>
    /// Constructor for a card Object.
    /// This Constructor initializes the List playerGuesses.
    /// </summary>
    /// <param name="cardID">The object will be initilized with this cardID.</param>
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
           // Debug.Log(answer);
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
    /// Method to add a specific Player to the playerGuesses List of the Card Object.
    /// </summary>
    /// <param name="player">The player that will be added.</param>
    public void AddPlayerToPlayerGuesses(Player player)
    {
        this.PlayerGuesses.Add(player);
    }

    /// <summary>
    /// A method returns an Object converted to a string in JSON format. 
    /// </summary>
    /// <returns>json as a string</returns>
    public string CardToJsonString()
    {
        string output = JsonConvert.SerializeObject(this);
        return output;
    }
    /// <summary>
    /// A method that reruns a new Card from a string in JSON format.
    /// </summary>
    /// <param name="json"></param>
    /// <returns>Card object representation of the JSON file</returns>
    public Card JsonToCard(string json)
    {
        Card tempCard = new Card();
        tempCard = JsonConvert.DeserializeObject<Card>(json);
        return tempCard;
    }

    /// <summary>
    /// This method adds the Card to cardList and Shuffles the List. This method should only be called on Cards which are correct.
    /// </summary>
    /// <param name="cardList">The List to to which the cards will be added.</param>
    /// <returns></returns>
    public List<Card> AddOneAndShuffle(List<Card> cardList)
    {
        this.IsCorrect = true;
        this.correctVotes = 0;
        cardList.Add(this);
        for (int i = 0; i < cardList.Count; i++)
        {
            Card temp = cardList[i];
            int index = UnityEngine.Random.Range(i, cardList.Count);
            cardList[i] = cardList[index];
            cardList[index] = temp;
        }
        return cardList;
    }

}

