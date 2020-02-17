/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetInputText : MonoBehaviour
{
    /// <summary>
    /// Card from which the input text is recieved.
    /// </summary>
    public Card card;       

    /// <summary>
    /// Adds a listener to an input field.
    /// This method is called once on startup.
    /// </summary>
    void Start()
    {
        var input = gameObject.GetComponent<TMP_InputField>();
        var se= new TMP_InputField.SubmitEvent();
        se.AddListener(GetInput);
        input.onEndEdit = se;                           //After ending input with enter the AddListener is called
    }

    /// <summary>
    /// Sets Card card's text to the given string.
    /// </summary>
    /// <param name="input">The string to which the answer shall be set</param>
    void GetInput(string input){
        card.Answer = input;
    }
}
