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
            networkEvents.GetComponent<NetworkEvents>().RpcUpdateNurse(gameObject);
        }
        else
        {
            networkEvents.GetComponent<NetworkIdentity>().AssignClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToServer);
            networkEvents.GetComponent<NetworkEvents>().CmdUpdateNurse(gameObject);
            networkEvents.GetComponent<NetworkIdentity>().RemoveClientAuthority();
        }
    }
}
