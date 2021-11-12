using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNetworkObjects : NetworkBehaviour
{
    GameObject player;
    List<GameObject> bottles = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;

        Debug.Log("player == isServer: " + (player == isServer));

        if(player == isServer)
        {
            bottles.Add(GameObject.Find("Bottles").transform.GetChild(0).gameObject);
            bottles.Add(GameObject.Find("Bottles").transform.GetChild(1).gameObject);
            bottles.Add(GameObject.Find("Bottles").transform.GetChild(2).gameObject);

            foreach (var bottle in bottles)
            {
                NetworkServer.Spawn(bottle);
            }
        }
    }
}


