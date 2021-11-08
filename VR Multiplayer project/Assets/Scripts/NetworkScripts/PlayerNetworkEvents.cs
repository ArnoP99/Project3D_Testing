using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetworkEvents : NetworkBehaviour
{
    GameObject networkEvents;

    private void Start()
    {
        networkEvents = GameObject.Find("NetworkEvents");
    }

    public void ExecuteVisualRepUpdate()
    {
        if (isServer)
        {
            Debug.Log("Rpc call from player");
            networkEvents.GetComponent<NetworkEvents>().RpcUpdateNurse(gameObject);
            networkEvents.GetComponent<NetworkIdentity>().AssignClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToServer);
        }
        else
        {
            networkEvents.GetComponent<NetworkEvents>().CmdUpdateNurse(gameObject);
            //networkEvents.GetComponent<NetworkIdentity>().RemoveClientAuthority();
        }
    }
}
