/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;

[Serializable]
public class Card : MonoBehaviour
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
            Debug.Log(answer);
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

    void Update()
    {
        
        foreach (char c in Input.inputString)
        {
            if(!answerGiven && !votePhase){
                if (Input.inputString == "\r"){
                    answerGiven = true;
                    PlayerManager.RegisterAnswer(this);
                } else if (Input.inputString == "\b" && answer.Length>0) 
                    answer = answer.Substring(0, answer.Length - 1);
                else
                    answer = answer + c;
                textField.text = Answer;
                Debug.Log(answer);
            }
        }   
    }

    void TimeUP(){
        answerGiven = true;
        PlayerManager.RegisterAnswer(this);
    }
}
