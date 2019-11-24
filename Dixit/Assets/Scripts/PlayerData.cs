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



    
}
