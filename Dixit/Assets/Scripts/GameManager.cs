using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string questionPath;
    public QuestionSet questionSet = new QuestionSet();
    QuestionScript questionScript;
    Question currentQuestion;
    List<Card> currentAnswers;
    PlayerManager pm;
    List<Card> allCards;
    List<Player> playerList;
    public int numberOfRounds;


    void NextQuestion()
    {
        currentQuestion = questionScript.GetQuestionFromQuestionSet(questionSet);
        pm.BroadcastQuestion(currentQuestion);
        //Debug.Log("NextQuestion aufgerufen");
    }
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        allCards = new List<Card>();
        playerList = new List<Player>();
        playerList.Add(new Player(1,0,2,0,2,"Marc"));
        playerList.Add(new Player(2, 0, 2, 0, 2, "Tom"));
        playerList.Add(new Player(3, 0, 2, 0, 2, "Robert"));
        playerList.Add(new Player(4, 0, 2, 0, 2, "Herzberg"));
        //playerList.Add(new Player(1, 0, 2, 0, 2, "Priefer"));
        List<Card> voteList = new List<Card>();
      //   Test ob bei tippen auf richtige Karte punkt everteilt werden
        allCards.Add(new Card());
        allCards[0].IsCorrect = true;
        allCards[0].PlayerObject = null;
        allCards[0].Answer = "Richtig";
        allCards[0].cardID = 42;
        allCards[0].AddPlayerToPlayerGuesses(new Player(2, 0, 2, 0, 2, "Tom"));
        allCards[0].AddPlayerToPlayerGuesses(new Player(1, 0, 2, 0, 2, "Marc"));
        allCards.Add(new Card());
        allCards[1].CorrectVotes = 2;
        allCards[1].IsCorrect = false;
        allCards[1].PlayerObject = new Player(2, 0, 2, 0, 2, "Tom");
        allCards[1].Answer = "Richtig";
        allCards[1].cardID = 1;
        //allCards[1].AddPlayerToPlayerGuesses(new Player(2, 0, 2, 0, 2, "Marc"));
        allCards.Add(new Card());
        allCards[2].IsCorrect = false;
        allCards[2].PlayerObject = new Player(3, 0, 2, 0, 2, "Robert");
        allCards[2].Answer = "Richtig";
        allCards[2].cardID = 3;
        //allCards[2].AddPlayerToPlayerGuesses(new Player(3, 0, 2, 0, 2, "Tom"));
        allCards.Add(new Card());
        allCards[3].IsCorrect = false;
        allCards[3].PlayerObject = new Player(1, 0, 2, 0, 2, "Marc");
        allCards[3].Answer = "Richtig";
        allCards[3].CorrectVotes = 2;
        allCards[3].cardID = 3;
        voteList.Add(new Card());
        voteList.Add(new Card());
        voteList.Add(new Card());
        voteList.Add(new Card());
        //vote equal test

        //vote test
        voteList[0].cardID = 1;
        voteList[0].IsCorrect = false;
        voteList[0].PlayerObject = playerList[1];
        //voteList[0].AddPlayerToPlayerGuesses(new Player(1,50,2,0,2,"Marc"));
        voteList[0].CorrectVotes = 1;
        voteList[0].AddPlayerToPlayerGuesses(new Player(2, 0, 2, 0,2, "Tom"));
        voteList[0].AddPlayerToPlayerGuesses(new Player(3, 0, 2, 0,2, "Robert"));
        //voteList[1].AddPlayerToPlayerGuesses(new Player(1, 0, 2, 0, 2, "Marc"));
        //voteList[1].cardID = 42;

        RegisterEqualVotes(voteList);
        RegisterVotes(voteList);
        //Debug.Log("" + allCards[1].PlayerObject.Score);
        numberOfRounds = 0;
        RoundEnd();


        //Player(int playerID, int score, int roomID, int experience, int level, string playerName)
        //Debug.Log("hallo");


        //überarbeiten da correct answer in questionScript vom ytp card ist, -> einlesen geht nicht so einfahc wie mit vorherigen question 
        //abfangen falls questionpath null;

        /*
        questionSet.QuestionList[0].questionID = 0;
        Debug.Log("hallo2");
        questionSet.QuestionList[0].correctAnswer.answer = "42";
        Debug.Log("hallo3");
        questionSet.QuestionList[0].question = "Wie lautet die Antwort auf alle Fragen?";
        questionSet.QuestionList[1].questionID = 0;
        questionSet.QuestionList[1].correctAnswer.answer = "42";
        questionSet.QuestionList[1].question = "Wie lautet die Antwort auf alle Fragen?";
        questionSet.QuestionList[2].questionID = 0;
        questionSet.QuestionList[2].correctAnswer.answer = "42";
        questionSet.QuestionList[2].question = "Wie lautet die Antwort auf alle Fragen?";*/
        //questionSet.PrintOutQuestions();

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
    /// This method is to register the player guesses concerning which answer is the correct one.
    /// To annul an answer, more than half of the players must vote for it.
    /// </summary>
    /// <param name="answers"></param>
    /// votes[j].CorrectVotes > voteLimit 
    public void RegisterEqualVotes(List<Card> votes)
    {
        for (int j = 0; j < votes.Count; j++) {
            for (int i = 0; i< allCards.Count; i++) {
                if (allCards[i].PlayerObject != null && votes[j].PlayerObject!=null && allCards[i].PlayerObject.playerID == votes[j].PlayerObject.playerID )
                {
                    allCards[i].CorrectVotes = votes[j].CorrectVotes;
                }
                    }
            
            }
        /*AUfgabe der Correct VOtes zum Debuggen
        for (int j = 0; j < allCards.Count; j++)
        {
            if(allCards[j].PlayerObject!=null)
            Debug.Log("Player:" + allCards[j].PlayerObject.PlayerName+" Card :" + j + " CorrectVotes: " + allCards[j].CorrectVotes);
        }*/
    }

    /// <summary>
    /// 
    /// Calls the method GiveOutPoints
    /// </summary>
    public void RegisterVotes(List<Card> answer)
    {
        for (int j = 0; j < answer.Count; j++)
        {
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
        
        GiveOutPoints();
    }
    /// <summary>
    /// 
    /// Calls the Method BroadCastScoresViaPM.
    /// </summary>
    void GiveOutPoints()
    {

        //for schleife über player cards
        for (int i = 0; i < allCards.Count; i++)
        {
            //wenn weniger als die hälfte der Spieler dafür gestimmt haben das die karte gleich der richtigen card ist
            if (allCards[i].PlayerObject != null && allCards[i].CorrectVotes < (playerList.Count/2) && allCards[i].IsCorrect == false)
            {
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
                                if(allCards[cardCount].PlayerObject != null && allCards[i].PlayerGuesses[k].playerID==allCards[cardCount].PlayerObject.playerID  && allCards[cardCount].CorrectVotes< playerList.Count/2)
                                    playerList[j].Score += 50;

                            }

                        }
                    }

                }
            }
    }
        BroadCastScoresViaPM();

    }

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
        pm.BoradCastAnswers(playerList);
        RoundEnd();
    }

    /// <summary>
    /// 
    /// </summary>
    void RoundEnd()
    {
        if(questionSet.QuestionList.Count == 0 || numberOfRounds<=0 )
        {
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
            Debug.Log(scoreBoard);

        }
        else
        {
            NextQuestion();
        }

    }


    void HandleAnswers(List<Card> answers)
    {
        currentAnswers = answers;
    }

}


