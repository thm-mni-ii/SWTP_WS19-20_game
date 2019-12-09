using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static List<Player> players = new List<Player>();
    public static List<Card> answers = new List<Card>();
    public Player player;
    static private bool allPlayersAnswered = true;


    /// <summary>
    /// Adds players to the dictionary.
    /// </summary>
    public void Start(){
        players.Add(player);
    }

    /// <summary>
    /// Recieves a question and a timer to send to all Players.
    /// </summary>
    /// <param name="player">A Question Object and the time for the timer</param>
   public static void BroadcastQuestion(Question question, float time){
       foreach (Player p in players)
       {
          p.question.startQuestion(time,question.question);
       }
   }

    /// <summary>
    /// Recieves the Answer from the players and marks them in the dictionary.
    /// </summary>
    /// <param name="player">The Card Object from the player.</param>
   public static void RegisterAnswer(Card answer){

       answers.Add(answer);
       if (answers.Count == players.Count)
       {
           // GameManager.HandleAnswer(answers);
       }
   }

    /// <summary>
    /// Sends the answers of all players and the system answer to all players.
    /// </summary>
    /// <param name="player">A List of Card Objects.</param>
   public void BroadcastAnswers(List<Card> answers){
        foreach (Player p in players)
       {
          p.ShowAnswers(answers);
       }
   }

    /// <summary>
    /// Sends the votes to remove a 
    /// </summary>
    /// <param name="player">The specific Player Object.</param>
   void RegisterEqualVote(List<Card> vote){
       foreach (Player p in players)
       {
          //GameManager.RegisterEqualVotes(List<Card> votes);
       }
   }

    /// <summary>
    /// Sends the new list of players to the player to update scores.
    /// </summary>
    /// <param name="player">A List of Player Objects.</param>
   public void BroadcastScores(List<Player> players2){
       foreach (Player p in players)
       {
           p.UpdateScores(players2);
       }
   }
}
