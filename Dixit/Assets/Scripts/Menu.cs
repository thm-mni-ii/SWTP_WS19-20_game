/* created by: SWT-P_WS_19/20_Game */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{   
    /// <summary>
    /// GameObject that is disabled at start.
    /// </summary>
    public GameObject obj;

    /// <summary>
    /// On start of the script is disabled.
    /// This method is called once on startup.
    /// </summary>
    void Start()
    {
       obj.SetActive(false);
    }
}
