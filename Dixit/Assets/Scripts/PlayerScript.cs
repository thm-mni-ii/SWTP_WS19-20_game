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
    private TextMeshProUGUI scoreboard;
    public QuestionScript question;
    public CardScript card;
    public CardScript playerCard;
    public Player player;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI nameText;
    public CardScript cs;


    public PlayerManager pm ;
    public List<Card> vote;
    public Card voteCard;
    public Boolean startPhase = true;
    Boolean votePhase = false;
    Boolean answerPhase = false;
    public List<CardScript> answerCards;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject cur in GameObject.FindGameObjectsWithTag("PlayerManager")) {
             Debug.Log ("found :)" +cur+" ,pid="+this.player.playerID);
             pm = cur.GetComponent<PlayerManager>();
             pm.RecievePlayer(this);
        }
        if (isLocalPlayer)
        {
            CreateNewCard();
            pm.killme();

            //pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
            //CardScript cs = GameObject.FindGameObjectWithTag("Card").GetComponent<CardScript>();
            cs.ps = this;
            //pm.RecievePlayer(this);   
        }
    }


    [ClientRpc]
    public void RpcCreateNewCard()
    {
        if (isLocalPlayer)
        {
            CreateNewCard();
        }
    }
    /// <summary>
    /// This Method instantiates a new CardScript Object and calls the InitializeQuestion method in QuestionScript.
    /// </summary>
    public void CreateNewCard()
    {

        CardScript c;
        startPhase=true;
        c = Instantiate(playerCard, card.transform.position, Quaternion.identity);
        c.ps = this;
        c.transform.Rotate(new Vector3(270, 0, 0));
        c.ps = this;

        c.textField = GetComponent<TMP_Text>();
        //if(i==0)
        c.card = new Card();
      //  question.cs = c;
       // question = Instantiate(question, question.transform, 0);
        question.cs = c;
        question.InitializeQuestion();
        cs = c;
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            if (startPhase)
            {
                //Debug.Log("votecount:" +vote.Count);
            }
            else if (votePhase == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    phaseText.text = "";
                    Debug.Log("update votephase");
                    this.CmdRegisterEqualVotes(vote.ToArray());
                    //pm.RegisterEqualVote(vote);
                    //Debug.Log(vote[0].CorrectVotes);
                    //Debug.Log(vote[1].CorrectVotes);
                    votePhase = false;
                    Debug.Log("countof cardskript from answercards" + answerCards.Count);


                }
            }
            else if (answerPhase == true)// && voteCard.PlayerGuesses.Count != 0) //voteCard.cardID!=0)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (voteCard.IsCorrect)
                    {
                        Debug.Log("correctcard answer");
                    }
                    Debug.Log("update answerphase");
                    Debug.Log("Clearcount:" + voteCard.PlayerGuesses.Count);
                    phaseText.text = "";

                    //Debug.Log(voteCard.PlayerGuesses[0].playerID);
                    this.CmdRegisterVote(voteCard, this.player);
                    //Debug.Log(vote[0].CorrectVotes);
                    //Debug.Log(vote[1].CorrectVotes);
                    answerPhase = false;
                }
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
        //hier isLocalPlayer einfügen

        if (isLocalPlayer)
        {
            votePhase = false;
            answerPhase = true;
            foreach (CardScript card in answerCards)
            {
            }
            voteCard = new Card();
            answerPhase = true;
            phaseText.text = "Antwortphase: \nBitte klicke auf die Karte, welche du als richtig erachtest";
            Debug.Log(phaseText.text);
            //CardScript[] cs = GetComponents<CardScript>();

            for (int i = 0; i < answerCards.Count; i++)
            {

                Debug.Log("Answercard i" + answerCards[i]);
                answerCards[i].votePhase = false;
                answerCards[i].answerPhase = true;
                answerCards[i].isAllreadyVoted = false;
            }

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
    /// <param name="answers">A list of cards which are used to instaiate new Cardscripts </param>
    public void ShowAnswers(List<Card> answers)
    {
        Debug.Log("lokalplayer");
        if (isLocalPlayer)
        {
            Debug.Log(this.player.PlayerName + " local");
            vote = new List<Card>();
            startPhase = false;
            votePhase = true;

            Debug.Log("not null" + phaseText);
            phaseText.text = "Votingphase: \nBitte klicke Karten an die du als gleichwertig erachtest und drücke dann Space";
            Debug.Log("not null" + phaseText.text);


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
                c.transform.Rotate(new Vector3(270, 0, 0));
                c.card = answer;
                c.card.cardID = answer.cardID;
                c.card.Answer = answer.Answer;
                c.votePhase = true;
                c.card.PlayerObject = answer.PlayerObject;
                if (c.card.PlayerObject != null)
                    Debug.Log("player:" + c.card.PlayerObject);
                Debug.Log("showanser: " + c.card.Answer);
                Debug.Log("countof cardskript from answercards" + answerCards.Count);

                for (int g = 0; g < answerCards.Count; g++)
                {
                    Debug.Log("AC:" + answerCards[g].card.Answer);
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
                if (answers.Count == 3)
                {
                    offset += 7;
                }
                if (answers.Count == 4)
                {
                    offset += 4;
                }
                if (answers.Count == 5)
                {
                    offset += 2;
                }
                answerCards.Add(c);
                
                Debug.Log("countof cardskript from answercards" + answerCards.Count);

            }

            Debug.Log("pphase: "+answerCards.Count);
            phaseText = GameObject.FindGameObjectWithTag("PhaseUI").GetComponent<TextMeshProUGUI>();
            Debug.Log("not null" + phaseText);
            phaseText.text = "Votingphase: \nBitte Klicke Karten an die du als gleichwertig erachtest und drücke dann enter";
            vote = new List<Card>();
            Debug.Log("countof cardskript from answercards" + answerCards.Count);

        }
    }

    /// <summary>
    /// This method updates the scores and corresponding playernames in scoreboard.text and nameText.text.
    /// </summary>
    /// <param name="players">A List of players, used for their score and name field.</param>
    public void UpdateScores(string name,string score)
    {
        //Debug.Log("pst RPC:" + players.Count +players[0].PlayerName+players[1].PlayerName);
        scoreboard.text = "";
        nameText.text = "";
        scoreboard.text = score;
        nameText.text = name;
       /* foreach (Player p in players)
        {
            scoreboard.text += p.Score + "\n";//"\t"+ p.PlayerName + "\n";
            nameText.text += p.PlayerName + "\n";
        }*/
       // Debug.Log("nametext"+nameText.text+"count" +players.Count);
    }
    [ClientRpc]
    /// <summary>
    /// This method destroys old CardScripts with the Tag answer cards. It sets the corresponding boolean answerPhase to false.
    /// </summary>
   
    public void RpcCleanUp() 
    {
        if (isLocalPlayer)
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

    }
    [Command]
    public void CmdRegisterEqualVotes(Card[] vote)
    {
        List<Card> votes = new List<Card>(vote);
        pm.RegisterEqualVote(votes);
    }


    [Command]
    public void CmdAnswerInc(Card card){
        card.PlayerObject = this.player;
        pm.RegisterAnswer(card);
        pm.killme();
    }

    [ClientRpc]
    /// <summary>
    /// This method shows the ScoreBoard at the end of the game, including which player is placed on which place.
    /// </summary>
    /// <param name="scoreboard">a string containg the scoreboard and the ending game message.</param>
    public void RpcShowScoreBoard(string scoreboard)
    {
        phaseText.text ="Spiel beendet\nDie Ergebnisse lauten wie folgt:\n"+ scoreboard;
    }

    [ClientRpc]
    public void RpcQuestionStart(float time, Question question)
    {
        
            this.question.startQuestion(time, question.question);
        
    }

    [ClientRpc]
    public void RpcReceiveAnswers(Card[] answers)
    {
        foreach (Card a in answers)
        {
            if(a.PlayerObject!=null)
            Debug.Log("pobj "+a.PlayerObject.PlayerName + "XDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD ");
        }
        List<Card> receivedCards = new List<Card>(answers);
        foreach (Card a in receivedCards)
        {
            if (a.PlayerObject != null)
                Debug.Log("pobj " + a.PlayerObject.PlayerName + "XDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDList ");
        }
        if (isLocalPlayer)
        {
            ShowAnswers(receivedCards);
            Debug.Log("lokalplayer");
        }
        
    }

    [ClientRpc]
    public void RpcStartAnswerPhase()
    {
        if (isLocalPlayer)
        {
            Debug.Log("answerphase3,count :" + answerCards.Count);
            this.StartAnswerPhase();
            Debug.Log("start answer XD");
        }
    }


    [Command]
    public void CmdRegisterVote(Card voteCard,Player player)
    {
        pm.RegisterVote(voteCard, this.player);

    }

    [ClientRpc]
    public void RpcUpdateScores(string name,string score)
    {

        this.UpdateScores(name,score);
        Debug.Log("updateScores");
    }
    // p.UpdateScores(players2);
}

