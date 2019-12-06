using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string questionPath;
    public QuestionSet questionSet = new QuestionSet();
    QuestionScript questionScript;
    PlayerManager pm;
    List<Card> allCards;
    List<Player> playerList;
    public int numberOfRounds;

    void Start()
    {
        allCards = new List<Card>();
        playerList = new List<Player>();
        playerList.Add(new Player(1,200,2,0,2,"Marc"));
        playerList.Add(new Player(2, 300, 2, 0, 2, "Tom"));
        playerList.Add(new Player(3, 300, 2, 0, 2, "Robert"));
        playerList.Add(new Player(4, 200, 2, 0, 2, "Herzberg"));
        playerList.Add(new Player(1, 100, 2, 0, 2, "Priefer"));
        numberOfRounds = 0;
        RoundEnd();


        //Player(int playerID, int score, int roomID, int experience, int level, string playerName)
        Debug.Log("hallo");


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
        questionSet.PrintOutQuestions();

        questionSet = questionSet.LoadQuestionSet(questionPath);
        pm = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        questionScript = GameObject.FindGameObjectWithTag("QuestionUI").GetComponent<QuestionScript>();
        questionScript.GetQuestionFromQuestionSet(questionSet);
        Debug.Log("OK");
        questionSet.PrintOutQuestions();
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
                if (allCards[i].PlayerObject.playerID == votes[j].PlayerObject.playerID)
                {
                    allCards[i].CorrectVotes = votes[j].CorrectVotes;
                }
                    }
        }
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
                if (allCards[i].PlayerObject.playerID == answer[j].PlayerObject.playerID)
                {
                    allCards[i].PlayerGuesses = answer[j].PlayerGuesses;
                }
            }
        }
        GiveOutPoints();
    }
    void GiveOutPoints()
    {
        //for schleife über player cards
        for(int i = 0; i < allCards.Count; i++)
        {
            //wenn weniger als die hälfte der Spieler dafür gestimmt haben das die karte gleich der richtigen card ist
            if((allCards[i].CorrectVotes <= allCards.Count) && allCards[i].IsCorrect == false)
            {
                //Schleife über alle Player 
                for (int k = 0; k < playerList.Count; k++)
                {
                    if (playerList[k].playerID == allCards[i].PlayerObject.playerID && allCards[i].CorrectVotes <= playerList.Count/2)
                    {
                        //schleife über alle Playerguesses
                        for (int j = 0; j < allCards[i].PlayerGuesses.Count; j++)
                        {
                            playerList[k].Score += 50;
                        }
                    }
                }
            }
            else if( allCards[i].IsCorrect == true)
            {
                //schleife über playerList
                for (int j = 0; j < playerList.Count;j ++)
                {
                    //schleife über playerguesses
                    for(int k=0; k < allCards[i].PlayerGuesses.Count;k++)
                    {
                        if (playerList[j].playerID == allCards[i].PlayerGuesses[k].playerID)
                        {
                            playerList[j].Score += 50;
                        }
                    }
                   
                }
            }
        }


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

    public void BroadCastScoresViaPM()
    {
        pm.BoradCastAnswers(playerList);
        RoundEnd();
    }


    void RoundEnd()
    {
        if(questionSet.QuestionList.Count == 0 || numberOfRounds<=0 )
        {
            //spiel beenden
            //hud 
            //display player scores
            // display winner
            string scoreBoard = "ScoreBoard\n";
            List<Player> winner = new List<Player>();
            
           // Player temp = new Player();

            //For Schleife die die Player nahc ihrem Score sortiert
            for(int i = 0; i < playerList.Count; i++)
            {
                winner.Add(playerList[i]);
                if (i > 0) {
                    if (playerList[i].Score > winner[i - 1].Score)
                    {
                        winner[i] = winner[i - 1];
                        winner[i - 1] = playerList[i];
                    }
                }

            }
            int place = 1;
            for (int i=0;i<winner.Count;i++)
            {
                if (i == 0)
                {
                    scoreBoard = "" + place + ". Platz: " + winner[i].PlayerName + " Score:" + winner[i].Score + "\n";
                }
                else{
                    if (winner[i].Score != winner[i - 1].Score) { 
                    
                        place += 1;
                 

                    }

                    scoreBoard += "" + place + ". Platz: " + winner[i].PlayerName + " Score:" + winner[i].Score + "\n";

                }

            }
            Debug.Log(scoreBoard);

        }
        else
        {
            NextQuestion();
        }

        void NextQuestion()
        {
            Debug.Log("NextQuestion aufgerufen");
        }

    }

}


