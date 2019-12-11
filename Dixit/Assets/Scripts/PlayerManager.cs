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
    //List<Card> voteList;

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
        if (player.votePhase == false)
        {
            answers.Add(answer);
            Debug.Log("Answercount in registeranswer: " + answers.Count);

            if (answers.Count == players.Count)
            {
                Debug.Log(players.Count + ":pc");
                Debug.Log("HIIIIIIIIIIIIIIIIIIIIIIIER");
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
        foreach (PlayerScript p in players)
       {
            Debug.Log(answers.Count + ":answercount before show answers anfang registerequalvote");
            p.ShowAnswers(answers);
            //Debug.Log(""+p.)
       }
   }



    int aufruf = 0;


    /// <summary>
    /// ´This method adds the player to playerGuesses if he voted that registers the votes concerning which cards are equal in answers.
    /// </summary>
    /// <param name="player">The specific Player Object.</param>
   public void RegisterEqualVote(List<Card> vote,Player player){
        //foreach (PlayerScript p in players)
        //{
        // Debug.Log(answers.Count);
        aufruf += 1;


        Debug.Log(aufruf+".aufruf");
        for (int i = 0; i < answers.Count; i++)
        {
            for (int j=0;j<vote.Count;j++)
            {
                if (answers[i].cardID == vote[j].cardID  )
                {
                    answers[i].CorrectVotes += vote[j].CorrectVotes;
                    if(answers[i].CorrectVotes!=0)
                    Debug.Log("Correct Votes:" + answers[i].CorrectVotes);
                   // answers[i].PlayerGuesses.Add(player);
                }
            }
        }

       //}
   }

    public void RegisterEqualVotesOnGM(List<Card> vote)
    {
        gm.RegisterEqualVotes(vote);
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
