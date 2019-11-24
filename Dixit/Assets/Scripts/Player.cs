using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Player
{
    //player und user ID werden jetzt zusammengelegt
   
    public int playerID;
    private int score;
    private int roomID;
    private int experience;
    private int level;
    private string playerName;
    //playerobject fällt hier auch weg.

    public Player()
    {
    }
    public Player(PlayerData playerData)
    {
        this.playerID = playerData.playerID;
        this.score = playerData.Score;
        this.roomID = playerData.RoomID;
        this.level = playerData.Level;
        this.playerName = playerData.PlayerName;
    }

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
    public Player LoadPlayerData(string path)
    {

        using (StreamReader reader = File.OpenText(@"" + path))
        {
            JObject on = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            string json = on.ToString();
            Debug.Log("playerjson:" + json);
            PlayerData playerData = new PlayerData();
            playerData= JsonConvert.DeserializeObject<PlayerData>(json);
            Debug.Log("level:" + playerData.Level);
            playerData.Score = 0;
            Player player = new Player(playerData);
            return player;



        }


    }


    public void PrintOutPlayer(Player player)
    {
        string output;
        
        output = JsonConvert.SerializeObject(player);
        Debug.Log(" " + player + " :" + output);

    }
}
