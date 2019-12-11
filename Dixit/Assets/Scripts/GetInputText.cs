/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetInputText : MonoBehaviour
{

    public Card card;       //Card for that Input

    void Start()
    {
        var input = gameObject.GetComponent<TMP_InputField>();
        var se= new TMP_InputField.SubmitEvent();
        se.AddListener(GetInput);
        input.onEndEdit = se;                           //After ending input with enter the AddListener is called
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetInput(string input){
        //Debug.Log(input);
        card.Answer = input;
    }
}
