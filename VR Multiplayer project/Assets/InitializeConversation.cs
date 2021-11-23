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
            Debug.Log(tempPlayer.transform.parent.transform.parent.transform.parent == isClient);
            Debug.Log(tempPlayer.transform.parent.transform.parent.transform.parent == isLocalPlayer);
            Debug.Log(tempPlayer.transform.parent.transform.parent.transform.parent);
            Debug.Log(tempPlayer.transform.parent.transform.parent.transform.parent.gameObject);


            if (tempPlayer.transform.parent.transform.parent.transform.parent.gameObject == isClient && tempPlayer.transform.parent.transform.parent.transform.parent.gameObject == isLocalPlayer)
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
