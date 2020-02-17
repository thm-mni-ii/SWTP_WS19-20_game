/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    /// <summary>
    /// A list of PlayerScripts that represents the players
    /// </summary>
    public static List<PlayerScript> players = new List<PlayerScript>();
    /// <summary>
    /// A list of Cards that represents the given answers
    /// </summary>
    public static List<Card> answers = new List<Card>();
    /// <summary>
    /// A single Playerscript to communicate with
    /// </summary>
    public PlayerScript player;
    /// <summary>
    /// The GameManager to communicate with
    /// </summary>
    public GameManager gm;
    /// <summary>
    /// A counter for player votes
    /// </summary>
    private int voteCounter;
    /// <summary>
    /// A counter for 'equal answer' votes
    /// </summary>
    int equalVotes;
    /// <summary>
    /// A counter for player ids
    /// </summary>
    int playerid = 1;
    /// <summary>
    /// A list of strings
    /// </summary>
    List<string> myList = new List<string>();

    /// <summary>
    /// Adds players to the dictionary.
    /// This method is called once on startup.
    /// </summary>
    public void Start(){
       
        myList.Add("Tom");
        myList.Add("Thomas");
        myList.Add("Günther");
        myList.Add("Jochen");
        myList.Add("Helga");
        myList.Add("Inga");
        myList.Add("Nina");
        voteCounter = 0;
        BroadCastPlayers();
        equalVotes = 0;
        voteCounter = 0;
    }

    /// <summary>
    /// This method sends the count of players to each PlayerScript.
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
    /// <param name="player">The player to join the game</param>
    /// </summary>
    public void RecievePlayer(PlayerScript player){
        System.Random r = new System.Random();
        foreach (PlayerScript p in players) {
            if (p == player) {
                Debug.Log("player not added");
                return;
            }
        }
        int tmp = r.Next(myList.Count);
        player.player = new Player(playerid, 0, 1337, 0, 0, myList[tmp]);
        myList.RemoveAt(tmp);
        playerid++;
        gm.playerList.Add(player.player);
        players.Add(player);
        Debug.Log("so viele spieler:" + players.Count);
    }



    /// <summary>
    /// Broadcasts all PlayerNames to all PlayScripts in players.
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

    /// <summary>
    /// Creates a new card to write on for each player.
    /// </summary>
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
    /// Recieves a question to send to all Players.
    /// </summary>
    /// <param name="question">The Question Object to be broadcasted</param>
    /// <param name="pL">The list of players to broadcast to</param>
   public void BroadcastQuestion(Question question,List<Player> pL){
        BroadcastScores(pL);
        equalVotes = 0;
        voteCounter = 0;
        answers = new List<Card>();
       foreach (PlayerScript p in players)
       {
          p.RpcQuestionStart(pL.Count, question);
       }
   }

    /// <summary>
    /// Broadcasts the beginning of the answer phase to all players.
    /// </summary>
    public void StartAnswerPhaseForAllPlayers()
    {
        Debug.Log("startanswerphase");
        foreach (PlayerScript player in players)
        {
            player.RpcStartAnswerPhase();
        }
    }


    /// <summary>
    /// Receives the Answer from the players and marks them in the dictionary.
    /// </summary>
    /// <param name="answer">The Card Object from the player.</param>
   public void RegisterAnswer(Card answer){
        if (true)
        {
            if(answer.cardID!=99)
            answers.Add(answer);

            if (answer.PlayerObject != null)
            {
                Debug.Log("pid"+answer.PlayerObject.playerID);
            }
            if (answer.PlayerObject == null)
            {
                Debug.Log("error");
            }
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
    /// <param name="answers">The list of player answers</param>
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
    /// This method registers a vote for a player.
    /// </summary>
    /// <param name="vote">The card to be voted on</param>
    /// <param name="p">The player that voted</param>
    public void RegisterVote(Card vote,Player p)
    {
        voteCounter++;
        Debug.Log("answerscountxx:"+answers.Count);
        Debug.Log("registervotes");
        Debug.Log("votecard:" + vote.PlayerGuesses.Count);

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
    /// This method registers which card is voted as an equal answer by  the players.
    /// It calls the method RegisterEqualVotes on the GameManager if all player have voted.
    /// </summary>
    /// <param name="vote">A List of cards from the player, containing the cards, which this player voted as equal</param>
    public void RegisterEqualVote(List<Card> vote){
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
                }
            }
        }
        if (equalVotes == players.Count)
        {
            Debug.Log("call gm.equalvotes");
            gm.RegisterEqualVotes(answers);

        }
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

