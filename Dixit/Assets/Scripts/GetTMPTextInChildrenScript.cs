/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* created by: SWT-P_WS_19/20_Game */
using System;
using TMPro;
/* created by: SWT-P_WS_19/20_Game */

public class GetTMPTextInChildrenScript : MonoBehaviour
{


    public TMP_Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
