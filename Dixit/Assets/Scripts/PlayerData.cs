using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[Serializable]
public class PlayerData 
{
    public int playerID;
    private int score;
  
    private int roomID;
    private int experience;
    private int level;
    private string playerName;
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



    
}
