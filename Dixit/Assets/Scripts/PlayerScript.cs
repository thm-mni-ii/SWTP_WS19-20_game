/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private TextMeshProUGUI scoreboard;
    public QuestionScript question;
    public CardScript card;
    public CardScript playerCard;
    public Player player;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI nameText;
    public PlayerManager pm;
    public List<Card> vote;
    public Card voteCard;
    public Boolean startPhase = true;
    int playercount;
    Boolean votePhase = false;
    Boolean answerPhase = false;
    TimerScript timer;
    float time;
    public List<CardScript> answerCards;
    // Start is called before the first frame update
    void Start()
    {
     pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
     CardScript cs = GameObject.FindGameObjectWithTag("Card").GetComponent<CardScript>();
     cs.ps = this;
     pm.RecievePlayer(this);
     timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();
     if(time==0)
        time = 10;
    // if(pla)

    }

    /// <summary>
    /// This Method instantiates a new CardScript Object and calls the InitializeQuestion method in QuestionScript.
    /// </summary>
    public void CreateNewCard()
    {

        CardScript c;
        startPhase=true;
        c = Instantiate(playerCard, card.transform.position, Quaternion.identity);
        c.transform.Rotate(new Vector3(270, 0, 0));
        c.ps = this;

        c.textField = GetComponent<TMP_Text>();
        //if(i==0)
        c.card = new Card();
      //  question.cs = c;
       // question = Instantiate(question, question.transform, 0);
        question.cs = c;
        question.InitializeQuestion();
    }

    void Update()
    {
        if (startPhase)
        {
            //Debug.Log("votecount:" +vote.Count);

        }
        else if (votePhase == true)
        {
            if (Input.GetKeyDown(KeyCode.Space)||timer.timeleft<=0)
            {
                timer.timeleft = 0;
                phaseText.text = "";
                Debug.Log("update votephase");

                foreach(CardScript card in answerCards)
                {
                    card.selectionObject.SetActive(false);

                    /*if (card.card.isCorrect)
                    {
                        card.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0);
                    }*/
                    card.votePhase = false;
                }

                
                pm.RegisterEqualVote(vote);
                //Debug.Log(vote[0].CorrectVotes);
                //Debug.Log(vote[1].CorrectVotes);
                votePhase = false;

            }
        }
        else if (answerPhase==true )//& voteCard.PlayerGuesses.Count!=0) //voteCard.cardID!=0)
        {
            if (Input.GetKeyDown(KeyCode.Return)|| timer.timeleft <= 0)
            {
                
                timer.timeleft = 0;
                //Debug.Log("Clearcount:" + voteCard.PlayerGuesses.Count);
                phaseText.text = "";

                //Debug.Log(voteCard.PlayerGuesses[0].playerID);
                for (int i = 0; i < answerCards.Count; i++)
                {
                    if (answerCards[i].card.isCorrect)
                    {
                        answerCards[i].gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0);
                    }
                    answerCards[i].answerPhase = false;
                }
                answerPhase = false;

                pm.RegisterVote(voteCard,this.player);
                //Debug.Log(vote[0].CorrectVotes);
                //Debug.Log(vote[1].CorrectVotes);
            }
        }
    }
    void Awake()
    {
        question = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        phaseText = GameObject.FindGameObjectWithTag("PhaseUI").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.FindGameObjectWithTag("NamesUI").GetComponent<TextMeshProUGUI>();
        scoreboard = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<TextMeshProUGUI>();
    }


    /// <summary>
    /// This Method displays the players and their starting score of 0 points in nameText.text and scoreBoard.text.
    /// 
    /// </summary>
    /// <param name="playerList">A string cotaining the players and a newline after every playername</param>
    /// <param name="playerScores">A string containing the score and a newline for every player</param>
    public void DisplayPlayers(string playerList,string playerScores)
    {
        nameText.text = playerList;
        scoreboard.text = playerScores;

    }


    /// <summary>
    /// This method starts the answerphase, setting the corresponding booleans directing which phase it is.
    /// </summary>
    public void StartAnswerPhase()
    {
        votePhase = false;
        answerPhase = true;

        
        voteCard = new Card();
        answerPhase = true;
        phaseText.text = "Antwortphase: \nBitte klicke auf die Karte, welche du als richtig erachtest";
        //CardScript[] cs = GetComponents<CardScript>();
        for(int i = 0; i < answerCards.Count; i++){

            answerCards[i].votePhase = false;
            answerCards[i].answerPhase = true;
            answerCards[i].isAllreadyVoted = false;
        }
        timer.setTimer(time);

        /* foreach (CardScript card in answerCards)
         {
             Debug.Log(card);
             card.answerPhase = true;
             card.isAllreadyVoted = false;
         }*/
    }


    /// <summary>
    /// This method shows all instantiates new CardScripts for the given answers and displays them for the player.
    /// </summary>
    /// <param name="answers">A list of cards which are used to instaiate new Cardscripts </param>
    public void ShowAnswers(List<Card> answers)
    {

        vote = new List<Card>();
        startPhase = false;
        votePhase = true;
        phaseText.text = "Votingphase: \nBitte klicke Karten an die du als gleichwertig erachtest und drücke dann Space";


        answerCards = new List<CardScript>();

        //int i = 0;
        float offset = -4;
        foreach (Card answer in answers)
        {
            CardScript c;

            c = Instantiate(card, card.transform.position, Quaternion.identity);

            //c.textField = GetComponent<TMP_Text>();
            //if(i==0)
            c.card = new Card();
            c.textField = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
           /* if(i==1)
            c.textField = GameObject.FindGameObjectWithTag("Text2").GetComponent<TMP_Text>();
            */
            c.transform.position = new Vector3(card.transform.position.x + offset, card.transform.position.y, card.transform.position.z);
            c.transform.Rotate(new Vector3(270,0,0));
            c.card = answer;
            c.card.cardID = answer.cardID;
            c.card.Answer = answer.Answer;
            c.votePhase = true;
            c.card.PlayerObject = answer.PlayerObject;
            if(c.card.PlayerObject!=null)
         
            for (int g= 0; g < answerCards.Count; g++)
            {
            }

            //c.card.cardID = answer.cardID;
            // if(c.card.PlayerObject!=null)
            // Debug.Log("pid: " + c.card.PlayerObject.playerID);
            //Debug.Log("vorher :" + answer.Answer);
            //c.textField = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();

            //GetComponent<TMP_Text>();
            //c.textField.text = answer.Answer;
            c.card.CorrectVotes = 0;
            c.answerGiven = true;
           // Debug.Log("Textfield: " + c.textField.text);
            c.votePhase = true;
            //answerCards.Add(c);
            if (answers.Count == 2)
            {
                offset += 7;
            }
            answerCards.Add(c);
            timer.setTimer(time);



        }

        phaseText = GameObject.FindGameObjectWithTag("PhaseUI").GetComponent<TextMeshProUGUI>();
        phaseText.text = "Votingphase: \nBitte Klicke Karten an die du als gleichwertig erachtest und drücke dann enter";
        vote = new List<Card>();


    }

    /// <summary>
    /// This method updates the scores and corresponding playernames in scoreboard.text and nameText.text.
    /// </summary>
    /// <param name="players">A List of players, used for their score and name field.</param>
    public void UpdateScores(List<Player> players)
    {
        scoreboard.text = "";
        nameText.text = "";
        foreach (Player p in players)
        {
            scoreboard.text += p.Score + "\n";//"\t"+ p.PlayerName + "\n";
            nameText.text += p.PlayerName + "\n";
        }
    }

    /// <summary>
    /// This method destorys old CardScripts with the Tag answer cards. It sets the corresponding boolean answerPhase to false.
    /// </summary>
    public void CleanUp() 
    {
        GameObject[] gameObjects;

      
        answerPhase = false;
        answerCards = null;


      
         gameObjects = GameObject.FindGameObjectsWithTag("AnswerCard");

            for (int i = 0; i < gameObjects.Length; i++)
            {
                Destroy(gameObjects[i]);
            }
        

    }


    /// <summary>
    /// This method shows the ScoreBoard at the end of the game, including which player is placed on which place.
    /// </summary>
    /// <param name="scoreboard">a string containg the scoreboard and the ending game message.</param>
    public void ShowScoreBoard(string scoreboard)
    {
        phaseText.text ="Spiel beendet\nDie Ergebnisse lauten wie folgt:\n"+ scoreboard;
    }

    /// <summary>
    /// This method sets the player count and the time of the timer according to how high the player count is.
    /// For 3 players the time is set to 20, for 4 players to 25 and for 5 players to 30.
    /// </summary>
    /// <param name="pCount">The number of players.</param>
    public void SetPlayerCountAndTime(int pCount)
    {
        playercount = pCount;
        if( pCount == 3)
        {
            time = 20;
        } else if (pCount == 4)
        {
            time = 25;
        } else if (pCount == 5)
        {
            time = 30;
        }
    }


}

