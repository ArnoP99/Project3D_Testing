using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeConversation : NetworkBehaviour
{
    GameObject nurse;
    GameObject agressor;
    List<GameObject> tempPlayers;

    void Start()
    {
        if (this == isClient && this != isServer)
        {
            tempPlayers = new List<GameObject>();
            Debug.Log(GameObject.FindGameObjectsWithTag("Nurse").Length);
            //tempPlayers.Add(GameObject.Find("Players").transform.GetChild(0).gameObject);
            foreach (var tempPlayer in tempPlayers)
            {
                Debug.Log(tempPlayer);
                Debug.Log(tempPlayer == isClient);
                Debug.Log(tempPlayer == isLocalPlayer);
                Debug.Log(tempPlayer.transform.parent.transform.parent.transform.parent.gameObject);


                if (tempPlayer == isClient && tempPlayer == isLocalPlayer)
                {
                    nurse = tempPlayer;
                    Debug.Log(nurse);
                }
            }

        }

    }

    void Update()
    {

    }
}
