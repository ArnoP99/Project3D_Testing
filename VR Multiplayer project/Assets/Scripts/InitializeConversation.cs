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
        tempPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (var tempPlayer in tempPlayers)
        {
            Debug.Log(tempPlayer);
            Debug.Log("IsServer: " + (tempPlayer == isServer));
            Debug.Log("IsClient: " + (tempPlayer == isClient));
            Debug.Log("IsLocalPlayer: " + (tempPlayer.GetComponent<NetworkIdentity>().isLocalPlayer));

            if (tempPlayer.GetComponent<NetworkIdentity>().isLocalPlayer == true && tempPlayer.GetComponent<NetworkIdentity>().isClient == true)
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
