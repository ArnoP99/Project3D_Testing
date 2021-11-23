using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeConversation : NetworkBehaviour
{
    GameObject nurse;
    GameObject agressor;
    GameObject tempPlayer;

    void Start()
    {
        if (this == isClient)
        {
            tempPlayer = GameObject.FindGameObjectWithTag("Nurse");
            if (tempPlayer == isClient && tempPlayer == isLocalPlayer)
            {
                nurse = tempPlayer;
                Debug.Log(nurse); 
            }
        }

    }

    void Update()
    {

    }
}
