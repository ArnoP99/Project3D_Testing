using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeConversation : NetworkBehaviour
{
    GameObject nurse;
    GameObject agressor;
    GameObject[] tempPlayers;

    private void Start()
    {
        
    }

    void Update()
    {
        if (this == isClient && this != isServer)
        {
            tempPlayers = GameObject.FindGameObjectsWithTag("Player");

            foreach (var tempPlayer in tempPlayers)
            {
                Debug.Log(tempPlayer);
                Debug.Log("IsServer: " + (tempPlayer == isServer));
                Debug.Log("IsClient: " + (tempPlayer == isClient));
                Debug.Log("IsLocalPlayer: " + (tempPlayer == isLocalPlayer));

                if (tempPlayer.transform.parent.transform.parent.gameObject == isClient && tempPlayer.transform.parent.transform.parent.gameObject == isLocalPlayer)
                {
                    nurse = tempPlayer;
                    Debug.Log(nurse);
                }
            }
        }
    }
}
