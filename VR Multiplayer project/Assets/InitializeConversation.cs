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
            Debug.Log(tempPlayer);
            if (tempPlayer.transform.parent.transform.parent.transform.parent == isClient && tempPlayer.transform.parent.transform.parent.transform.parent == isLocalPlayer)
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
