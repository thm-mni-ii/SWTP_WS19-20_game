/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public int cardID;
    private string answer;
    private Player playerObject;
    private int correctVotes;
    private bool isCorrect;
    private List<Player> playerGuesses;

    public Card(int cardID)
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



    //alle klein zu groß buchstaben funktion toUpper();
}
