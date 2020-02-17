/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class GameManager : NetworkBehaviour
{
    /// <summary>
    /// The name of the currently used QuestionSet
    /// </summary>
    public string questionSetName;
    /// <summary>
    /// The currently used QuestionSet
    /// </summary>
    public QuestionSet questionSet = new QuestionSet();
    /// <summary>
    /// The PlayerManager the GameManager communicates with
    /// </summary>
    public PlayerManager pm;
    /// <summary>
    /// The QuestionScript the GameManager gets new questions from
    /// </summary>
    QuestionScript questionScript;
    /// <summary>
    /// The question of the current round in a game
    /// </summary>
    Question currentQuestion;
    /// <summary>
    /// All answer cards given by the players
    /// </summary>
    List<Card> allCards;
    /// <summary>
    /// All players participating in the game
    /// </summary>
    public List<Player> playerList;
    /// <summary>
    /// The number of rounds to be played
    /// </summary>
    public int numberOfRounds;
    /// <summary>
    /// Bool to decide if the next question is to be shown
    /// </summary>
    private bool nextQuestion = false;

    /// <summary>
    /// This method gets the next question from the QuestionScript and calls the BroadcastQuestion method on the PlayerManager.
    /// </summary>
    void NextQuestion()
    {
        currentQuestion = questionScript.GetQuestionFromQuestionSet(questionSet);
        //Debug.Log("answer:" +currentQuestion.correctAnswer.Answer);
        pm.SendPlayerCount();
        pm.BroadcastQuestion(currentQuestion,playerList);
        pm.CreateNewCardForPlayers();

        //Debug.Log("NextQuestion aufgerufen");
    }

    /// <summary>
    /// Initializes the GameManager's variables.
    /// This method is called once on startup.
    /// </summary>
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        allCards = new List<Card>();
        playerList = new List<Player>();
        //equalVotesCounter = 0;
        numberOfRounds = 2;
        //questionSet = questionSet.LoadQuestionSet(questionSetName);
        Debug.Log("GM QS " + questionSet.questionList.Count);
        questionSet.JsonToQuestionSet(questionSetName);
        Debug.Log("GM QS2 " + questionSet.questionList.Count);
        questionSet.PrintOutQuestions();
        questionScript = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        //currentQuestion = questionScript.GetQuestionFromQuestionSet(questionSet);
        nextQuestion = true;
        //NextQuestion();
        Debug.Log("OK");

    }

    /// <summary>
    /// Proceeds to the next question.
    /// This method is called once per frame.
    /// </summary>
    void Update()
    {
        //Debug.Log("GM QS " + questionSet.questionList.Count);
        if(nextQuestion)
        {
            if(questionSet.questionList.Count > 0)
            {
                nextQuestion = false;
                NextQuestion();
            }
        }
    }


    /// <summary>
    /// This method is to register the player guesses concerning which answer is equal to the correct one.
    /// To annul an answer, more than half of the players must vote for it.
    /// </summary>
    /// <param name="votes">The list of cards to be voted for</param>
    public void RegisterEqualVotes(List<Card> votes)
    {
        Debug.Log("in gm equal");
        Debug.Log(allCards.Count);
        for (int j = 0; j < votes.Count; j++) {
           

            for (int i = 0; i< allCards.Count; i++) {
                Debug.Log("i+"+allCards[i].PlayerObject);
                if (allCards[i].PlayerObject != null && votes[j].PlayerObject!=null && allCards[i].PlayerObject.playerID == votes[j].PlayerObject.playerID )
                {
                    allCards[i].CorrectVotes = votes[j].CorrectVotes;
                    Debug.Log("Player:" + allCards[j].PlayerObject.PlayerName + " Card :" + j + " CorrectVotes: " + allCards[j].CorrectVotes);
                Debug.Log("xaxa");

                }
            }
            

        }
        Debug.Log("in nach gm equal");

        for (int j = 0; j < allCards.Count; j++)
        {
            if(allCards[j].PlayerObject!=null)
            Debug.Log("Player:" + allCards[j].PlayerObject.PlayerName+" Card :" + j + " CorrectVotes: " + allCards[j].CorrectVotes);
        }
        pm.StartAnswerPhaseForAllPlayers();
    }

    /// <summary>
    /// This method is to register the player guesses concerning which answer is the correct one.
    /// Calls the method GiveOutPoints.
    /// <param name="answer">The list of cards to be voted for</param>
    /// </summary>
    public void RegisterVotes(List<Card> answer)
    {
        for (int j = 0; j < answer.Count; j++)
        {
            if (answer[j].IsCorrect)
                //Debug.Log("correct card playerguess" + answer[j].PlayerGuesses[0].PlayerName);
            for (int i = 0; i < allCards.Count; i++)
            {
               
                if (allCards[i].PlayerObject!=null && answer[j].PlayerObject!=null && allCards[i].PlayerObject.playerID == answer[j].PlayerObject.playerID)
                {
                    allCards[i].PlayerGuesses = answer[j].PlayerGuesses;
                }
            }
        }
        GiveOutPoints();
    }

    /// <summary>
    /// This method increases the player scores acording to the rules of the game.
    /// Calls the Method BroadCastScoresViaPM.
    /// </summary>
    void GiveOutPoints()
    {
        Debug.Log(playerList.Count);
        //for loop iterating player cards
        for (int i = 0; i < allCards.Count; i++)
        {
            //if less than half of the players voted the card to be equal to the correct answer
            if (allCards[i].PlayerObject != null && allCards[i].CorrectVotes < (playerList.Count/2) && allCards[i].IsCorrect == false)
            {
                Debug.Log("wenn weniger als die hälfte der Spieler dafür gestimmt haben das die karte gleich der richtigen card ist");
                //loop iterating over all players
                for (int k = 0; k < playerList.Count; k++)
                {
                  if ( playerList[k].playerID == allCards[i].PlayerObject.playerID )
                    {
                        //loop iterating over all Playerguesses
                        for (int j = 0; j < allCards[i].PlayerGuesses.Count; j++)
                        {
                            playerList[k].Score += 50;
                        }
                    }
                }
            }

        }
        for (int i = 0; i < allCards.Count; i++)
        {
            if (allCards[i].IsCorrect == true)
            {
                //loop iterating over playerList
                for (int j = 0; j < playerList.Count; j++)
                {
                  
                    for (int k = 0; k < allCards[i].PlayerGuesses.Count; k++)
                    {
                        if (playerList[j].playerID == allCards[i].PlayerGuesses[k].playerID )
                        {

                            for(int cardCount = 0; cardCount < allCards.Count;cardCount++)
                            {
                                if (allCards[cardCount].PlayerObject != null && allCards[i].PlayerGuesses[k].playerID == allCards[cardCount].PlayerObject.playerID && allCards[cardCount].CorrectVotes < 1+playerList.Count / 2)
                                {
                                    Debug.Log(allCards[i].PlayerGuesses.Count);
                                    playerList[j].Score += 50;
                                    Debug.Log("pg"+allCards[i].PlayerGuesses.Count);

                                    Debug.Log(playerList[j].Score + " " + playerList[j].PlayerName);
                                }
                            }

                        }
                    }

                }
            }
    }


        Debug.Log("After points giveout");
        BroadCastScoresViaPM();

    }

    /// <summary>
    /// This Method updates all the Player Objects in allCards
    /// </summary>
    /// <param name="allCards">The List of cards which cards contain the playerobjects</param>
    public void UpdatePlayersInCardList(List<Card> allCards)
    {
        for(int i = 0; i < allCards.Count; i++)
        {
            for(int j= 0; j<=playerList.Count; j++)
            {
                if( allCards[i].PlayerObject.playerID== playerList[j].playerID)
                {
                    allCards[i].PlayerObject.Score=playerList[j].Score;
                }
            }
        }
    }

    /// <summary>
    /// Broadcasts the scores to all players via the PlayerManager.
    /// Calls the Method RoundEnd()
    /// </summary>
    public void BroadCastScoresViaPM()
    {
        Debug.Log("boradcastet");
        pm.BroadcastScores(playerList);
        Debug.Log("boradcastet");
        RoundEnd();
        Debug.Log("noend");
    }

    /// <summary>
    /// This method checks if there are still rounds to be played or questions in questionSet.
    /// If one of those is true a NextQuestion will be called, else the scoreboard will be broadcasted via ShowScoreBoard.
    /// </summary>
    void RoundEnd()
    {

        Debug.Log("End!!");
        questionSet.RemoveQuestionFromSet(0);
        Debug.Log("");
        if (questionSet.QuestionList.Count == 0 || numberOfRounds<=0 )
        {
            //Debug.Log("count null");
            //hud 
            //display player scores
            // display winner
            string scoreBoard = "ScoreBoard\n";
            
            Player temp = new Player();

            //for loop sorting players by score
            
            bool sorted;
            do
            {
                sorted = true;
                for (int i = 0; i < playerList.Count - 1; i++)
                {
                    if (playerList[i].Score < playerList[i + 1].Score)
                    {

                        temp = playerList[i];
                        playerList[i] = playerList[i + 1];
                        playerList[i + 1] = temp;
                        sorted = false;
                    }
                }

            } while (!sorted);

            int place = 1;
            for (int i=0;i<playerList.Count;i++)
            {
                if (i == 0)
                {
                    scoreBoard = "" + place + ". Platz: " + playerList[i].PlayerName + " Score:" + playerList[i].Score + "\n";
                }
                else{
                    if (playerList[i].Score != playerList[i - 1].Score) { 
                    
                        place += 1;
                 

                    }

                    scoreBoard += "" + place + ". Platz: " + playerList[i].PlayerName + " Score:" + playerList[i].Score + "\n";

                }

            }
            pm.ShowScoreBoard(scoreBoard);
            Debug.Log(scoreBoard);

        }
        else
        {
            Debug.Log("prewait");
            StartCoroutine(WaitSecondsThanCleanup());
           /* Debug.Log("postwait");
            CleanUp();
            pm.CreateNewCardForPlayers();
            //should be called to wait for 3 seconds so that the players kann see which answer was the right one
                nextQuestion = true;
            //NextQuestion();*/
        }

    }

    /// <summary>
    /// This method calls the CleanUp method on PlayerManager pm.
    /// </summary>
    public void CleanUp()
    {
        pm.CleanUp();
    }

    /// <summary>
    /// This method recieves all the answers, saves them in allCards, adds the correctcard and broadcasts allCards via BroadCastAnswers on PlayerManager pm.
    /// </summary>
    /// <param name="answers">The list of answer cards</param>
    public void HandleAnswers(List<Card> answers)
    {
        allCards = answers;
        for (int i = 0; i < answers.Count; i++)
        {
            Debug.Log("answer " + i + ": " + allCards[i].Answer);

        }
        Debug.Log("answers handled");
        Debug.Log("allcardcount:"+allCards.Count);
        questionSet.QuestionList[0].correctAnswer.AddOneAndShuffle(allCards);
        pm.BroadcastAnswers(allCards);
    }

    /// <summary>
    /// This Coroutine waits for 3 seconds before starting the cleanup of the last round, creating new player cards and setting nextQuestion=true so that a new round can start.
    /// </summary>
    /// <returns>A Coroutine does not return anything</returns>
    IEnumerator WaitSecondsThanCleanup()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("waited");
        CleanUp();
        //should be called to wait for 3 seconds so that the players kann see which answer was the right one
        nextQuestion = true;
        //NextQuestion();
    }
}
