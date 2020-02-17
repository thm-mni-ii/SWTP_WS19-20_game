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
using Mirror;


public class PlayerScript : NetworkBehaviour
{
    /*
    /// <summary>
    /// A text element to visualize scoreboard updates
    /// </summary>
    private TextMeshProUGUI scoreboardUpdate;
    /// <summary>
    /// The speed of the update animation
    /// </summary>
    public float flashSpeed = 1f;
    /// <summary>
    /// The color of the update animation
    /// </summary>
    public Color scoreboardUpdateColor = new Color(0.078f, 0.706f, 0.078f, 1f);
    /// <summary>
    /// The color while the scoreboard is not showing any animation
    /// </summary>
    public Color scoreboardNoUpdateColor = new Color(0.078f, 0.706f, 0.078f, 0f);
    /// <summary>
    /// Bool if the scoreboard should show an update
    /// </summary>
    bool scoreboardUpdating;
    */

    /// <summary>
    /// A CardScript to communicate with. This CardScript is set to istanciated in each Startphase.
    /// </summary>
    public CardScript cs;
    /// <summary>
    /// The scoreboard text element.
    /// </summary>
    private TextMeshProUGUI scoreboard;
    /// <summary>
    /// A QuestionScript to communicate with. This QuestionScript will be set at the start of each round.
    /// </summary>
    public QuestionScript question;
    /// <summary>
    /// A CardScript to communicate with. This CardScript is set to the CardScript of AnswerCards, which is used in the votingphase and answerphase.
    /// </summary>
    public CardScript card;
    /// <summary>
    /// A CardScript to communicate with. Used as c placeholder for the CardObject prefab.
    /// </summary>
    public CardScript playerCard;
    /// <summary>
    /// A Player object to communicate with.
    /// </summary>
    public Player player;
    /// <summary>
    /// The text element which displays the current stage of the game.
    /// </summary>
    public TextMeshProUGUI phaseText;
    /// <summary>
    /// The names displayed in the scoreboard.
    /// </summary>
    public TextMeshProUGUI nameText;
    /// <summary>
    /// The PlayerManager to communicate with. Can only be accessed on the ServerSide via Cmd methods.
    /// </summary>
    public PlayerManager pm;
    /// <summary>
    /// A list of cards for voting purposes.
    /// </summary>
    public List<Card> vote;
    /// <summary>
    /// A single card for voting purposes.
    /// </summary>
    public Card voteCard;
    /// <summary>
    /// Bool if the game is currently in the start phase.
    /// </summary>
    public Boolean startPhase = true;
    /// <summary>
    /// Number of players.
    /// </summary>
    int playercount;
    /// <summary>
    /// Bool if the game is currently in the voting phase.
    /// </summary>
    Boolean votePhase = false;
    /// <summary>
    /// Bool if the game is currently in the answer phase.
    /// </summary>
    Boolean answerPhase = false;
    /// <summary>
    /// Timer to limit the duration of phases.
    /// </summary>
    TimerScript timer;
    /// <summary>
    /// Variable for current time. Value that is set to this variable varies depending on the number of players.
    /// </summary>
    float time;
    /// <summary>
    /// List of CardScripts representing the answers given by the players.
    /// </summary>
    public List<CardScript> answerCards;


    /// <summary>
    /// Initializes communication with PlayerManager and Timer
    /// This method is called before the first frame update.
    /// </summary>
    void Start()
    {

        foreach(GameObject cur in GameObject.FindGameObjectsWithTag("PlayerManager")) 
        {
                    pm = cur.GetComponent<PlayerManager>();
                    pm.RecievePlayer(this);
        }  


        foreach(GameObject cur in GameObject.FindGameObjectsWithTag("Timer")) 
        {
                 timer = cur.GetComponent<TimerScript>();
        }  
        if(isLocalPlayer){

              foreach(GameObject cur in GameObject.FindGameObjectsWithTag("Timer")) 
              {
                 timer = cur.GetComponent<TimerScript>();
              }  

    
        }
    }



    /// <summary>
    /// Calls the CreateNewCard method on a local player and sets the PlayerScript of the Card created in that method to this playerscript. This is an ClientRpc method so it will be send and executed on all Clients.
    /// </summary>
    [ClientRpc]
    public void RpcCreateNewCard()
    {
       if(isLocalPlayer)
       {
            CreateNewCard();
            cs.ps=this;
       }
    }

    /// <summary>
    /// This Method instantiates a new CardScript Object and calls the InitializeQuestion method in QuestionScript.
    /// </summary>
    public void CreateNewCard()
    {
        if(isLocalPlayer)
        {
            CardScript c;
            startPhase=true;
            c = Instantiate(playerCard, card.transform.position, Quaternion.identity);
            c.transform.Rotate(new Vector3(270, 0, 0));
            c.ps = this;

            c.textField = GetComponent<TMP_Text>();
            c.card = new Card();
            question.cs = c;
            question.InitializeQuestion();
            cs=c;
        }
    }

    /// <summary>
    /// Controls the game based on the current phase.This method is called once per frame.
    /// </summary>
    void Update()
    {
        if (isLocalPlayer)
        {
           if (votePhase == true)
            {
                if (Input.GetKeyDown(KeyCode.Space)||timer.timeleft<=0)
                {
                    timer.timeleft = 0;
                    phaseText.text = "";
                    foreach(CardScript card in answerCards)
                    {
                        card.selectionObject.SetActive(false);
                        card.votePhase = false;
                    }

                    
                    this.CmdRegisterEqualVotes(vote.ToArray());
                    //Debug.Log(vote[0].CorrectVotes);
                    //Debug.Log(vote[1].CorrectVotes);
                    votePhase = false;
                    voteCard = new Card();

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
                                  
                    this.CmdRegisterVote(voteCard, this.player);

                    answerPhase = false;

                    //pm.RegisterVote(voteCard,this.player);
                    //Debug.Log(vote[0].CorrectVotes);
                    //Debug.Log(vote[1].CorrectVotes);
                }
            }
            /*
            if(scoreboardUpdating)
            {
                scoreboardUpdate.color = scoreboardUpdateColor;
                scoreboardUpdating = false;
            } else
            {
                scoreboardUpdate.color = Color.Lerp(scoreboardUpdate.color, scoreboardNoUpdateColor, flashSpeed * Time.deltaTime);
            }
            */
        }
    }

    /// <summary>
    /// Initializes variables of the PlayerScript.
    /// This method is called once on startup.
    /// </summary>
    void Awake()
    {
        question = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        phaseText = GameObject.FindGameObjectWithTag("PhaseUI").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.FindGameObjectWithTag("NamesUI").GetComponent<TextMeshProUGUI>();
        scoreboard = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<TextMeshProUGUI>();
        /*
        scoreboardUpdate = GameObject.FindGameObjectWithTag("ScoreChangeUI").GetComponent<TextMeshProUGUI>();
        scoreboardUpdate.color = scoreboardNoUpdateColor;
        */
    }


    /// <summary>
    /// This Method displays the players and their starting score of 0 points in nameText.text and scoreBoard.text. This is an ClientRpc method so it will be send and executed on all Clients.
    /// </summary>
    /// <param name="playerList">A string cotaining the players and a newline after every playername</param>
    /// <param name="playerScores">A string containing the score and a newline for every player</param>
    [ClientRpc]
    public void RpcDisplayPlayers(string playerList,string playerScores)
    {
        nameText.text = playerList;
        scoreboard.text = playerScores;

    }

    [ClientRpc]
    /// <summary>
    /// This method starts the answerphase, setting the corresponding booleans directing which phase it is. This is an ClientRpc method so it will be send and executed on all Clients.
    /// </summary>
    public void RpcStartAnswerPhase()
    {
        votePhase = false;
        answerPhase = true;

        if(isLocalPlayer){
        //votePhase = false;
        //answerPhase = true;

        
        voteCard = new Card();
        phaseText.text = "Antwortphase: \nBitte klicke auf die Karte, welche du als richtig erachtest und bestätige dann mit Enter";
        //CardScript[] cs = GetComponents<CardScript>();
        for(int i = 0; i < answerCards.Count; i++){

            answerCards[i].votePhase = false;
            answerCards[i].answerPhase = true;
            answerCards[i].isAllreadyVoted = false;
            answerCards[i].ansCards = answerCards;
        }
        timer.setTimer(time);

        /* foreach (CardScript card in answerCards)
         {
             Debug.Log(card);
             card.answerPhase = true;
             card.isAllreadyVoted = false;
         }*/
        }
    }


    /// <summary>
    /// This method shows all instantiates new CardScripts for the given answers and displays them for the player.
    /// </summary>
    /// <param name="answers">A list of cards which are used to instantiate new Cardscripts </param>
    public void ShowAnswers(List<Card> answers)
    {
        if(isLocalPlayer){
            vote = new List<Card>();
            startPhase = false;
            votePhase = true;
            phaseText.text = "Votingphase: \nBitte klicke Karten an die du als gleichwertig erachtest und drücke dann Space";
            answerCards = new List<CardScript>();
            float offset = 0; ;
            switch (answers.Count)
            {
                    case 2:
                        offset = -4;
                        break;
                    case 3:
                        offset = -6;
                        break;
                    case 4:
                        offset = -8;
                        break;
                    case 5:
                        offset = -10;
                        break;
                    case 6:
                        offset = -12;
                        break;
            }
            foreach (Card answer in answers)
            {
                CardScript c;
                c = Instantiate(card, card.transform.position, Quaternion.identity);
                c.card = new Card();
                c.textField = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
                c.transform.position = new Vector3(card.transform.position.x + offset, card.transform.position.y, card.transform.position.z);
                c.transform.Rotate(new Vector3(270,0,0));
                c.card = answer;
                c.card.cardID = answer.cardID;
                c.card.Answer = answer.Answer;
                c.votePhase = true;
                c.card.PlayerObject = answer.PlayerObject;
                c.card.CorrectVotes = 0;
                c.answerGiven = true;
                c.votePhase = true;
                    if (answers.Count == 3)
                        {
                            offset += 5;
                        }
                    if (answers.Count == 4)
                        {
                            offset += 5;
                        }
                    if (answers.Count == 5)
                        {
                            offset += 5;
                        }
                    if (answers.Count == 6)
                        {
                            offset += 5;
                        }
                answerCards.Add(c);
                timer.setTimer(time);
            }
            phaseText = GameObject.FindGameObjectWithTag("PhaseUI").GetComponent<TextMeshProUGUI>();
            phaseText.text = "Votingphase: \nBitte Klicke Karten an die du als gleichwertig erachtest und drücke dann Space";
            vote = new List<Card>();
        }
    }

    [ClientRpc]
    /// <summary>
    /// This method updates the scores and corresponding playernames in scoreboard.text and nameText.text.This is an ClientRpc method so it will be send and executed on all Clients.
    /// </summary>
    /// <param name="name">A string of player names</param>
    /// <param name="score">A string of player scores</param>
    /// <param name="scoreUpdates">An array of the difference between old and new player scores</param>
    public void RpcUpdateScores(string name,string score, int[] scoreUpdates)
    {
        scoreboard.text = "";
        //scoreboardUpdate.text = "";
        nameText.text = "";
        scoreboard.text = score;
        nameText.text = name;

        
    }
    [ClientRpc]

    /// <summary>
    /// This method destroys old CardScripts with the Tag answercards. It sets the corresponding boolean answerPhase to false. This is an ClientRpc method so it will be send and executed on all Clients.
    /// </summary>
    public void RpcCleanUp() 
    {
        if(isLocalPlayer){
            GameObject[] gameObjects;
            answerPhase = false;
            answerCards = null;
             gameObjects = GameObject.FindGameObjectsWithTag("AnswerCard");
            for (int i = 0; i < gameObjects.Length; i++)
            {
                Destroy(gameObjects[i]);
            }
        }
    }

    [ClientRpc]
    /// <summary>
    /// This method shows the ScoreBoard at the end of the game, including which player is placed on which place. This is an ClientRpc method so it will be send and executed on all Clients.
    /// </summary>
    /// <param name="scoreboard">a string containg the scoreboard and the ending game message.</param>
    public void RpcShowScoreBoard(string scoreboard)
    {
        phaseText.text ="Spiel beendet\nDie Ergebnisse lauten wie folgt:\n"+ scoreboard;
    }


    [ClientRpc]
    /// <summary>
    /// This method sets the player count and the time of the timer according to how high the player count is. For 3 players the time is set to 20, for 4 players to 25 and for 5 players to 30. This is an ClientRpc method so it will be send and executed on all Clients.
    /// </summary>
    /// <param name="pCount">The number of players.</param>
    public void RpcSetPlayerCountAndTime(int pCount)
    {
        if (isLocalPlayer)
        {
            playercount = pCount;
            if (pCount == 1)
            {
                time = 10;
            }
            if (pCount == 2)
            {
                time = 10;
            }
            if (pCount == 3)
            {
                time = 20;
            }
            else if (pCount == 4)
            {
                time = 25;
            }
            else if (pCount == 5)
            {
                time = 30;
            }
        }
    }

    /// <summary>
    /// This command registers equal votes for a card array.This is an Command method so it will be send and executed on the Server.
    /// </summary>
    /// <param name="vote">The card array </param>
    [Command]
    public void CmdRegisterEqualVotes(Card[] vote)
    {
        List<Card> votes = new List<Card>(vote);
        pm.RegisterEqualVote(votes);
    }

    /// <summary>
    /// This method receives the player answers through a card array. This is an ClientRpc method so it will be send and executed on all Clients.
    /// </summary>
    /// <param name="answers">The card array</param>
    [ClientRpc]
    public void RpcReceiveAnswers(Card[] answers)
    {
        List<Card> receivedCards = new List<Card>(answers);
        if (isLocalPlayer)
        {
            ShowAnswers(receivedCards);
        }
    }

    /// <summary>
    /// This method initializes a new question for the player. This is an ClientRpc method so it will be send and executed on all Clients.
    /// </summary>
    /// <param name="count"></param>
    /// <param name="question">The question to be set</param>
    [ClientRpc]
    public void RpcQuestionStart(int count, Question question)
    {

        if(isLocalPlayer){
            RpcSetPlayerCountAndTime(count);
            this.question.startQuestion(time, question.question);
        }
    }

    /// <summary>
    /// Registers an incoming answer for the player. This is an Command method so it will be send and executed on the Server.
    /// </summary>
    /// <param name="card">The incoming answer</param>
    [Command]
    public void CmdAnswerInc(Card card){
        card.PlayerObject = this.player;
        pm.RegisterAnswer(card);
    }

    /// <summary>
    /// Registers the voting of this player on a card. It calls the RegisterVote method of the playermanager with the voted card voteCard und the PlayerScript. This is an Command method so it will be send and executed on the Server.
    /// </summary>
    /// <param name="voteCard">The card to vote on</param>
    /// <param name="player"></param>
    [Command]
    public void CmdRegisterVote(Card voteCard,Player player)
    {
        pm.RegisterVote(voteCard, this.player);
    }
}

