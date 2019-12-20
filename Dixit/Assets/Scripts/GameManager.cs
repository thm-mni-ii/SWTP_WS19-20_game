/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string questionPath;
    public QuestionSet questionSet = new QuestionSet();
    public PlayerManager pm;
    QuestionScript questionScript;
    Question currentQuestion;
    List<Card> currentAnswers;
    List<Card> allCards;
    public List<Player> playerList;
    public int numberOfRounds;

    /// <summary>
    /// This method gets the next question from questionscript and calls the BroadcastQuestion method on PlayerManager pm.
    /// </summary>
    void NextQuestion()
    {
        currentQuestion = questionScript.GetQuestionFromQuestionSet(questionSet);
        //Debug.Log("answer:" +currentQuestion.correctAnswer.Answer);
        pm.BroadcastQuestion(currentQuestion,30f);
        //Debug.Log("NextQuestion aufgerufen");
    }
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        allCards = new List<Card>();
        playerList = new List<Player>();
        //equalVotesCounter = 0;
        numberOfRounds = 5;
        questionSet = questionSet.LoadQuestionSet(questionPath);
        questionSet.PrintOutQuestions();
        questionScript = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        //currentQuestion = questionScript.GetQuestionFromQuestionSet(questionSet);
        NextQuestion();
        Debug.Log("OK");
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// This method is to register the player guesses concerning which answer is equal to the correct one.
    /// To annul an answer, more than half of the players must vote for it.
    /// </summary>
    /// <param name="answers"></param>
    /// votes[j].CorrectVotes > voteLimit 
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

        //AUfgabe der Correct VOtes zum Debuggen
        for (int j = 0; j < allCards.Count; j++)
        {
            if(allCards[j].PlayerObject!=null)
            Debug.Log("Player:" + allCards[j].PlayerObject.PlayerName+" Card :" + j + " CorrectVotes: " + allCards[j].CorrectVotes);
        }
        pm.StartAnswerPhaseForAllPlayers();
    }

    /// <summary>
    /// This method is to register the player guesses concerning which answer is the correct one.
    /// Calls the method GiveOutPoints
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
        /*
        Debug.Log("Card :" + 0 + " PlayerGuesses: " + allCards[0].PlayerGuesses.Count);

        for (int j = 0; j < allCards.Count; j++)
        {
            if (allCards[j].PlayerObject != null)
                Debug.Log("Player:" + allCards[j].PlayerObject.PlayerName + " Card :" + j + " PlayerGuesses: " + allCards[j].PlayerGuesses.Count);
        }*/
        Debug.Log("registervotes");

        GiveOutPoints();
    }
    /// <summary>
    /// This method increases the player scores acording to the rules of the game.
    /// Calls the Method BroadCastScoresViaPM.
    /// </summary>
    void GiveOutPoints()
    {
        Debug.Log(playerList.Count);
        //for schleife über player cards
        for (int i = 0; i < allCards.Count; i++)
        {
            //wenn weniger als die hälfte der Spieler dafür gestimmt haben das die karte gleich der richtigen card ist
            if (allCards[i].PlayerObject != null && allCards[i].CorrectVotes < (playerList.Count/2) && allCards[i].IsCorrect == false)
            {
                Debug.Log("wenn weniger als die hälfte der Spieler dafür gestimmt haben das die karte gleich der richtigen card ist");
                //Schleife über alle Player 
                for (int k = 0; k < playerList.Count; k++)
                {
                  if ( playerList[k].playerID == allCards[i].PlayerObject.playerID )
                    {
                        //schleife über alle Playerguesses
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
                //schleife über playerList
                for (int j = 0; j < playerList.Count; j++)
                {
                  
                    for (int k = 0; k < allCards[i].PlayerGuesses.Count; k++)
                    {
                        if (playerList[j].playerID == allCards[i].PlayerGuesses[k].playerID )
                        {

                            for(int cardCount = 0; cardCount < allCards.Count;cardCount++)
                            {
                                //1 + wieder entfernen wenn nicht mehr single player mode
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
    /// 
    /// </summary>
    /// <param name="allCards">The List of cards,which cards contain the playerobjects.</param>
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
    /// 
    /// Calls the Method RoundEnd()
    /// </summary>
    public void BroadCastScoresViaPM()
    {
        pm.BroadcastScores(playerList);
        RoundEnd();
    }

    /// <summary>
    /// This method checks if there are still rounds to be played or questions ind questionSet.
    /// If one of those is true a NextQuestionwill be called, else the scoreboard will be broadcastet via ShowScoreBoard.
    /// </summary>
    void RoundEnd()
    {


        questionSet.RemoveQuestionFromSet(0);
        Debug.Log("");
        if (questionSet.QuestionList.Count == 0 || numberOfRounds<=0 )
        {
            //Debug.Log("count null");
            //spiel beenden
            //hud 
            //display player scores
            // display winner
            string scoreBoard = "ScoreBoard\n";
            
            Player temp = new Player();

            //For Schleife die die Player nahc ihrem Score sortiert
            
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
            CleanUp();
            NextQuestion();
        }

    }

    /// <summary>
    /// This method acalls the CleanUp method on PlayerManager pm.
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

}





