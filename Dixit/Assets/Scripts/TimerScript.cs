/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class TimerScript : NetworkBehaviour
{
    /// <summary>
    /// Time left on the timer.
    /// </summary>
    public float timeleft = 0f;
    /// <summary>
    /// Textfield wher the timer is displayed.
    /// </summary>
    Text text;
    /// <summary>
    /// Textcolor is used to change the color of the text depending on the time left on the timer.
    /// </summary>
    Color textcolor;

    /// <summary>
    /// Initializes variables of the TimerScript.
    /// This method is called once on startup.
    /// </summary>
    void Awake()
    {
        text = GetComponent<Text>();        //Get the textfield from scene.
        textcolor = text.color;
    }

    /// <summary>
    /// Controls the color of the timer
    /// This method is called once per frame.
    /// </summary>
    void Update()
    {
        if(timeleft > 0)
        {
            text.text = timeleft.ToString("0.00");
            timeleft -= Time.deltaTime;

            if(timeleft >= 10)                     //Change the color to orange when the timer is below 10 seconds
            {
                textcolor = new Color(65f / 255f, 163f / 255f, 23f / 255f);
            }
            else if(timeleft < 10 && timeleft >= 3)             //Change the color to orange when the timer is below 10 seconds
            {
                textcolor = new Color(255f / 255f, 170f / 255f, 29f / 255f);
            } else                                              //Change the color to red when the timer is below 3 seconds
            {
                textcolor = new Color(255f / 255f, 0f / 255f, 0f / 255f);
            }

        } else
        {
            textcolor.a = 0f;
        }
        text.color = textcolor;
    }

    /// <summary>
    /// This method sets timeleft.
    /// </summary>
    /// <param name="time">The value that timeleft is set to.</param>
    public void setTimer(float time)
    {
        timeleft = time;
    }
}
