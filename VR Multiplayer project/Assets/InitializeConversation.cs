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
            tempPlayer = GameObject.Find("Players").transform.GetChild(1).gameObject;
            Debug.Log(tempPlayer);
            Debug.Log(tempPlayer == isClient);
            Debug.Log(tempPlayer == isLocalPlayer);
            Debug.Log(tempPlayer.transform.parent.transform.parent.transform.parent.gameObject)
                ;


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
