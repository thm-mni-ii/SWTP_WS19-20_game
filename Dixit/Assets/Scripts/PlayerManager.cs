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


    /// <summary>
    /// Adds players to the dictionary.
    /// </summary>
    public void Start(){
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("PlayerScript").GetComponent<PlayerScript>();
        player.player = new Player(1, 0, 1337, 0, 0, "TOM");
        gm.playerList.Add(player.player);
        players.Add(player);
    }

    /// <summary>
    /// Recieves a question and a timer to send to all Players.
    /// </summary>
    /// <param name="player">A Question Object and the time for the timer</param>
   public void BroadcastQuestion(Question question, float time){
       foreach (PlayerScript p in players)
       {
          p.question.startQuestion(time,question.question);
       }
   }

    /// <summary>
    /// Recieves the Answer from the players and marks them in the dictionary.
    /// </summary>
    /// <param name="player">The Card Object from the player.</param>
   public void RegisterAnswer(Card answer){

       answers.Add(answer);
       if (answers.Count == players.Count)
       {
            
            gm.HandleAnswers(answers);
          /*  for(int i = 0; i < answers.Count; i++)
            {
                Debug.Log("answer "+i+": "+answers[i].Answer);

            }*/
            //Debug.Log("gm.HandleAnswers(answers");
       }
   }

    /// <summary>
    /// Sends the answers of all players and the system answer to all players.
    /// </summary>
    /// <param name="player">A List of Card Objects.</param>
   public void BroadcastAnswers(List<Card> answers){
        foreach (PlayerScript p in players)
       {
            Debug.Log("before show pid"+p.player.playerID);
          p.ShowAnswers(answers);
            //Debug.Log(""+p.)
       }
   }

    /// <summary>
    /// Sends the votes to remove a 
    /// </summary>
    /// <param name="player">The specific Player Object.</param>
   public void RegisterEqualVote(List<Card> vote){
       foreach (PlayerScript p in players)
       {
          gm.RegisterEqualVotes(vote);

       }
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
