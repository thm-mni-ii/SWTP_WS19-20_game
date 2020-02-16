/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetInputText : MonoBehaviour
{
    /// <summary>
    /// Card from which the inputtext is recieved.
    /// </summary>
    public Card card;       

    void Start()
    {
        var input = gameObject.GetComponent<TMP_InputField>();
        var se= new TMP_InputField.SubmitEvent();
        se.AddListener(GetInput);
        input.onEndEdit = se;                           //After ending input with enter the AddListener is called
    }


    void GetInput(string input){
        card.Answer = input;
    }
}
