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
       

    }

    void Update()
    {
        if (this == isClient && this != isServer)
        {
            tempPlayers = new List<GameObject>();
            Debug.Log(GameObject.FindGameObjectsWithTag("Nurse").Length);
            //tempPlayers.Add(GameObject.Find("Players").transform.GetChild(0).gameObject);
            foreach (var tempPlayer in GameObject.FindGameObjectsWithTag("Nurse"))
            {
                tempPlayers.Add(tempPlayer);
                Debug.Log(tempPlayer);
                Debug.Log(tempPlayer.transform.parent.transform.parent.gameObject == isClient);
                Debug.Log(tempPlayer.transform.parent.transform.parent.gameObject == isLocalPlayer);
                Debug.Log(tempPlayer.transform.parent.transform.parent.gameObject);


                if (tempPlayer.transform.parent.transform.parent.gameObject == isClient && tempPlayer.transform.parent.transform.parent.gameObject == isLocalPlayer)
                {
                    nurse = tempPlayer;
                    Debug.Log(nurse);
                }
            }

        }
    }
}
