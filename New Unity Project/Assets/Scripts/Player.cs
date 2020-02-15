using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[Serializable]
public class Player
{
    //player und user ID werden jetzt zusammengelegt
    /// <summary>
    /// The identfication of the player.
    /// </summary>
    public int playerID;
    /// <summary>
    /// The current score the the player.
    /// </summary>
    private int score;
    /// <summary>
    /// The roomID of the current game.
    /// </summary>
    private int roomID;
    /// <summary>
    /// The experience of the player.
    /// </summary>
    private int experience;
    /// <summary>
    /// The current level of the player
    /// </summary>
    private int level;
    /// <summary>
    /// The name of the player.
    /// </summary>
    private string playerName;

  //  public Card card;
  


    /// <summary>
    /// An empty constructor for Player.
    /// </summary>
    /// 
    public Player()
    {

    }


    public Player(int playerID, int score, int roomID, int experience, int level, string playerName)
    {
        this.playerID = playerID;
        this.score = score;
        this.roomID = roomID;
        this.experience = experience;
        this.level = level;
        this.playerName = playerName;
    }

    /// <summary>
    /// Getter/Setter for score.
    /// </summary>
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    /// <summary>
    /// Getter/Setter for roomID.
    /// </summary>
    public int RoomID
    {
        get
        {
            return roomID;
        }
        set
        {
            roomID = value;
        }
    }

    /// <summary>
    /// Getter/Setter for experience.
    /// </summary>
    public int Experience
    {
        get
        {
            return experience;
        }
        set
        {
            experience = value;
        }
    }

    /// <summary>
    /// Getter/Setter for level.
    /// </summary>
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }
    /// <summary>
    /// Getter/Setter for playerName.
    /// </summary>
    public string PlayerName
    {
        get
        {
            return playerName;
        }
        set
        {
            playerName = value;
        }
    }



    //wird zu anfang des spiels aufgerufen,
    //LoadPlayerData has a path to a json file as its parameter
    //it sets the score of a player to 0
    //it returns a player objekt.

    /// <summary>
    /// A method that uses a path to a JSON File to load the data of a player, create a Player Object and resets its score.
    /// This method will be invoked at the beginning of a game.
    /// This method is not final, it will be changed so it can work with mirror and firebase.
    /// </summary>
    /// <param name="path">The path to the JSON File</param>
    /// <returns>Returns a Playerobject repersantion of the JSON file.</returns>
    public Player LoadPlayerData(string path)
    {

        using (StreamReader reader = File.OpenText(@"" + path))
        {
            JObject on = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            string json = on.ToString();
           // Debug.Log("playerjson:" + json);
           
         
            //Debug.Log("level:" + player.Level);
            Player player = new Player();
            player = JsonConvert.DeserializeObject<Player>(json);
            player.Score = 0;
            return player;



        }


    }


    /// <summary>
    /// A method returns a Player Object converted to a string in JSON format. 
    /// </summary>
    /// <returns>json as a string</returns>
    public string PlayerToJSONString()
    {
        string output = JsonConvert.SerializeObject(this);
            return output;
    }
    /// <summary>
    /// A simple method to print out the a Player Object as a string in JSON format.
    /// This method may be used for debugging purposes.
    /// </summary>
    /// <param name="player">The specific Player Object.</param>
    public void PrintOutPlayer(Player player)
    {
        string output;
        
        output = JsonConvert.SerializeObject(player);
        Debug.Log(" " + player + " :" + output);

    }

   
}
