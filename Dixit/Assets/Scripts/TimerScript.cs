using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    //public Question question;
    public float timeleft = 0f;
    Text text;
    Color textcolor;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        textcolor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeleft > 0)
        {
            text.text = timeleft.ToString("0.00");
            timeleft -= Time.deltaTime;

            if(timeleft >= 10)
            {
                textcolor = new Color(65f / 255f, 163f / 255f, 23f / 255f);
            }
            else if(timeleft < 10 && timeleft >= 3)
            {
                textcolor = new Color(255f / 255f, 170f / 255f, 29f / 255f);
            } else
            {
                textcolor = new Color(255f / 255f, 0f / 255f, 0f / 255f);
            }

        } else
        {
            textcolor.a = 0f;
        }
        text.color = textcolor;
    }

    public void setTimer(float i)
    {
        timeleft = i;
    }
}
