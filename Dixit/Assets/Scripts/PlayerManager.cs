/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static List<PlayerScript> players = new List<PlayerScript>();
    public static List<Card> answers = new List<Card>();
    public PlayerScript player;
    public GameManager gm;
    private int voteCounter;
    int equalVotes;

    /// <summary>
    /// Adds players to the dictionary.
    /// </summary>
    public void Start(){
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("PlayerScript").GetComponent<PlayerScript>();
        player.player = new Player(1, 0, 1337, 0, 0, "TOM");
        voteCounter = 0;
        gm.playerList.Add(player.player);
        players.Add(player);
        equalVotes = 0;
    }


    /// <summary>
    /// Recieves a question and a timer to send to all Players.
    /// </summary>
    /// <param name="player">A Question Object and the time for the timer</param>
   public void BroadcastQuestion(Question question, float time){
        //int i = 0;
       foreach (PlayerScript p in players)
       {
            
          p.question.startQuestion(time,question.question);
       }
   }

    public void StartAnswerPhaseForAllPlayers()
    {
        foreach (PlayerScript player in players)
        {
            player.StartAnswerPhase();
        }
    }


    /// <summary>
    /// Recieves the Answer from the players and marks them in the dictionary.
    /// </summary>
    /// <param name="player">The Card Object from the player.</param>
   public void RegisterAnswer(Card answer){
        //if: wird hier benötigt da sonst eine zusätzliche instanz von Card in answers hinzugefügt wird
        //woher dieser aufruf stammt kann ich noch nicht sagen
        if (player.votePhase == false)
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
                //Debug.Log("HIIIIIIIIIIIIIIIIIIIIIIIER");
                gm.HandleAnswers(answers);

                /*  for(int i = 0; i < answers.Count; i++)
                  {
                      Debug.Log("answer "+i+": "+answers[i].Answer);

                  }*/
                //Debug.Log("gm.HandleAnswers(answers");
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
            p.ShowAnswers(answers);
            //Debug.Log(""+p.)
       }
   }





    /// <summary>
    /// ´This method adds the player to playerGuesses if he voted that registers the votes concerning which cards are equal in answers.
    /// </summary>
    /// <param name="player">The specific Player Object.</param>
   public void RegisterEqualVote(List<Card> vote,Player player){
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
    /// Sends the new list of players to the player to update scores.
    /// </summary>
    /// <param name="player">A List of Player Objects.</param>
   public void BroadcastScores(List<Player> players2){
       foreach (PlayerScript p in players)
       {
           p.UpdateScores(players2);
       }
   }
}
