using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayer : NetworkBehaviour
{
    GameObject player1;
    GameObject player2;


    // Start is called before the first frame update
    void Start()
    {
        try { 
        player1 = GameObject.Find("Players").transform.GetChild(0).gameObject;   
        player2 = GameObject.Find("Players").transform.GetChild(1).gameObject;

        Debug.Log(player1 == isLocalPlayer);
        Debug.Log(player2 == isLocalPlayer);
        }catch(Exception ex)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
