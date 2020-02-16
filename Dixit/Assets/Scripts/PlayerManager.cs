﻿/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public static List<PlayerScript> players = new List<PlayerScript>();
    public static List<Card> answers = new List<Card>();
    public PlayerScript player;
    public GameManager gm;
    private int voteCounter;
    int equalVotes;
    int playerid = 1;
    List<string> myList = new List<string>();

    /// <summary>
    /// Adds players to the dictionary.
    /// </summary>
    public void Start(){
        //player = GameObject.FindGameObjectWithTag("PlayerScript").GetComponent<PlayerScript>();
       
        myList.Add("Tom");
        myList.Add("Thomas");
        myList.Add("Günther");
        myList.Add("Jochen");
        myList.Add("Helga");
        myList.Add("Inga");
        myList.Add("Nina");
        //player.player = new Player(1, 0, 1337, 0, 0, "TOM");
        voteCounter = 0;
        //gm.playerList.Add(player.player);
        //players.Add(player);
        BroadCastPlayers();
        /*foreach (PlayerScript p in players)
        {
            Debug.Log("player:" + p.player.PlayerName);
        }*/
        equalVotes = 0;
        voteCounter = 0;
    }

    /// <summary>
    /// This method sends the count of players to each playerscript.
    /// </summary>

    public void SendPlayerCount()
    {
        foreach(PlayerScript p in players)
        {
            p.RpcSetPlayerCountAndTime(players.Count);
        }
    }

    /// <summary>
    /// Used by player to join the list of players
    /// </summary>
    public void RecievePlayer(PlayerScript player){
        System.Random r = new System.Random();
        foreach (PlayerScript p in players) {
            if (p == player) {
                Debug.Log("player not added");
                return;
            }
        }
        player.player = new Player(playerid, 0, 1337, 0, 0, myList[r.Next(myList.Count)]);
        playerid++;
        gm.playerList.Add(player.player);
        players.Add(player);
        //gm.playerList.Add(player.player);
        Debug.Log("so viele spieler:" + players.Count);
    }



    /// <summary>
    /// Broadcasts all PlayerNames to all all Playscripts in players.
    /// </summary>
    public void BroadCastPlayers()
    {
        string playerScores = "";
        string playerList = "";
        foreach (PlayerScript player in players)
        {
            playerList += player.player.PlayerName + "\n";
            playerScores += "0\n";
        }
        foreach(PlayerScript player in players)
        {
            player.RpcDisplayPlayers(playerList,playerScores);
        }
    }


    public void CreateNewCardForPlayers()
    {

        Debug.Log("create clean");

        foreach (PlayerScript p in players)
        {
            Debug.Log("create clean");
            Debug.Log("playercount: "+players.Count);
            p.RpcCreateNewCard();
        }
    }
    /// <summary>
    /// Recieves a question and a timer to send to all Players.
    /// </summary>
    /// <param name="player">A Question Object and the time for the timer</param>
    /// 
   public void BroadcastQuestion(Question question,List<Player> pL){
        //int i = 0;
        BroadcastScores(pL);
        equalVotes = 0;
        voteCounter = 0;
        answers = new List<Card>();
       foreach (PlayerScript p in players)
       {
          p.RpcQuestionStart(pL.Count, question);
          //p.CreateNewCard();
          //p.question.startQuestion(time,question.question);
       }
   }

    public void StartAnswerPhaseForAllPlayers()
    {
       /* foreach(Card answer in answers)
        {
            answer.PlayerGuesses = new List<Player>();
            answer.PlayerGuesses.Clear();
        }*/
        Debug.Log("startanswerphase");
        foreach (PlayerScript player in players)
        {
            player.RpcStartAnswerPhase();
        }
    }


    /// <summary>
    /// Recieves the Answer from the players and marks them in the dictionary.
    /// </summary>
    /// <param name="player">The Card Object from the player.</param>
   public void RegisterAnswer(Card answer){
        //if: wird hier benötigt da sonst eine zusätzliche instanz von Card in answers hinzugefügt wird
        //woher dieser aufruf stammt kann ich noch nicht sagen
        if (true)
        {

            if(answer.cardID!=99)
            answers.Add(answer);

            if (answer.PlayerObject != null)
                Debug.Log("pid"+answer.PlayerObject.playerID);
            if (answer.PlayerObject == null)
                Debug.Log("error");
            if (answers.Count == players.Count)
            {
                Debug.Log(players.Count + ":pc");
                gm.HandleAnswers(answers);

            }
        }
   }

    /// <summary>
    /// Sends the answers of all players and the system answer to all players.
    /// </summary>
    /// <param name="player">A List of Card Objects.</param>
   public void BroadcastAnswers(List<Card> answers){
        foreach(Card answer in answers)
        {
            answer.CorrectVotes = 0;
            if(answer.PlayerObject!=null)
            Debug.Log("player:" + answer.PlayerObject.playerID);

        }
        PlayerManager.answers = answers;

       foreach (PlayerScript p in players)
        {
            if (players.Count == (answers.Count - 1))
            {
                Card[] cardArray = answers.ToArray();
                foreach (Card ans in cardArray)
                {
                    if (ans.PlayerObject != null)
                        Debug.Log("Player still there " + ans.PlayerObject.PlayerName);
                }
                p.RpcReceiveAnswers(cardArray);
            }
   }

   }



    /// <summary>
    /// ´This method adds the player to playerGuesses if he voted that registers the votes concerning which cards are equal in answers.
    /// </summary>
    /// <param name="player">The specific Player Object.</param>
    /// 
    public void RegisterVote(Card vote,Player p)
    {
        voteCounter++;
        Debug.Log("answerscountxx:"+answers.Count);
        Debug.Log("registervotes");
        Debug.Log("votecard:" + vote.PlayerGuesses.Count);


        //auskommentiert solange hier mit shallow copy von answer gearbietet wird, wenn das ganze über das netzwerk laufne sollen müssen hie ränderungen vorgenommen werden
        for (int i = 0; i < answers.Count; i++)
        {
            Debug.Log("answer in reg:" + answers[i].Answer);
            Debug.Log("registervotes2");
            if (vote != null)
            {
                if (answers[i].cardID == vote.cardID)
                {
                    Debug.Log("registervotes3");
                    for (int g = 0; g < answers[i].PlayerGuesses.Count; i++)
                    {

                        //Wenn die Karte die man votet die eigene ist, bricht die forschleife ab
                        if (answers[i].PlayerGuesses[g].playerID == p.playerID)
                        {
                            i = answers.Count - 1;
                            break;
                        }
                    }
                    if (answers[i].PlayerGuesses.Count == 0)
                    {
                        Debug.Log("registervotes4");
                        answers[i].PlayerGuesses = new List<Player>();
                        Debug.Log("answerspgerst:" + answers[i].PlayerGuesses.Count);
                        //answers[i].PlayerGuesses.Clear();
                        Debug.Log("registervotes5");
                    }


                    Debug.Log("registervotes5");
                    Debug.Log("pgpre:" + vote.PlayerGuesses.Count);
                    Debug.Log("answerspgpre:" + answers[i].PlayerGuesses.Count);

                    answers[i].PlayerGuesses.Add(p);
                    Debug.Log("pgpost:" + vote.PlayerGuesses.Count);
                    Debug.Log("registervotes3");
                    Debug.Log("answerspg:" + answers[i].PlayerGuesses.Count);
                    break;
                }
            }
        }
        Debug.Log("registervotes");
        Debug.Log("votecounter:" + voteCounter);
        Debug.Log("playerCount" + players.Count);
        if (voteCounter == players.Count)
        {
            Debug.Log("registervotes");


            gm.RegisterVotes(answers);
        }
    }

    /// <summary>
    /// This method registers  which card is voted as an equal answer by  the players.
    /// It calls the method RegisterEqualVotes on the GameManager if all player have voted.
    /// </summary>
    /// <param name="vote">A List of cards from the player, containing the cards, which this player voted as equal.</param>
    public void RegisterEqualVote(List<Card> vote){
        //foreach (PlayerScript p in players)
        //{
        // Debug.Log(answers.Count);
        equalVotes++;
        for (int i = 0; i < vote.Count; i++)
        {
            for (int j=0;j<answers.Count;j++)
            {
                if (answers[j].cardID == vote[i].cardID  )
                {
                    if (answers[j].PlayerObject != null)
                        Debug.Log("playerregeq:" + answers[j].PlayerObject);
                    answers[j].CorrectVotes++;
                   // answers[i].PlayerGuesses.Add(player);
                }
            }
        }
        if (equalVotes == players.Count)
        {
            Debug.Log("call gm.equalvotes");
            gm.RegisterEqualVotes(answers);

        }

        //}
    }

    
    /// <summary>
    /// This method calls the CleanUp method on PlayerScript player.
    /// </summary>
    public void CleanUp()
    {
        Debug.Log("call cleanup");
        foreach (PlayerScript p in players)
       {
            p.RpcCleanUp();
       }
       
    }


    /// <summary>
    /// Sends the new list of players to the player to update scores.
    /// </summary>
    /// <param name="players2">A List of Player Objects.</param>
   public void BroadcastScores(List<Player> players2){
        string names = "";
        string score = "";
        int[] scoreUpdates = new int[players2.Count];
        int counter = 0;
       foreach (PlayerScript p in players)
       {
            scoreUpdates[counter] = p.player.Score;
            names += p.player.PlayerName + "\n";
            score += p.player.Score+"\n";
            scoreUpdates[counter] = p.player.Score - scoreUpdates[counter];
            counter++;
          // p.UpdateScores(players2);
       }
       foreach(PlayerScript p in players){
            p.RpcUpdateScores(names,score, scoreUpdates);

       }
       
   }

    /// <summary>
    /// This method calls the ShowScoreBoard method on PlayerScript player.
    /// </summary>
    /// <param name="scoreboard"></param>
    public void ShowScoreBoard(string scoreboard)
    {
        foreach (PlayerScript p in players)
       {
            p.RpcShowScoreBoard(scoreboard);
       }
    }
}

