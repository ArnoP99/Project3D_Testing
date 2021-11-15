using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignAuth : NetworkBehaviour
{

    // Function where we can check if player isClient and isLocalPlayer and not isServer before executing CmdAssignAuthority
    public void ExecuteCmdAssignAuthority(NetworkIdentity objectID)
    {
        Debug.Log("Player: " + this);

        Debug.Log("Player == IsClient: " + (this == isClient));
        Debug.Log("Player == IsServer: " + (this == isServer));
        Debug.Log("Player == IsLocalPlayer: " + (this == isLocalPlayer));

        if (this == isClient && this == isServer && this == isLocalPlayer)
        {
            CmdAssignAuthority(objectID, this.GetComponent<NetworkIdentity>());
        }
    }

    // Function where we can check if player isClient and isLocalPlayer and not isServer before executing CmdRemoveAuthority
    public void ExecuteCmdRemoveAuthority(NetworkIdentity objectID)
    {
        Debug.Log("Player: " + this);

        Debug.Log("Player == IsClient: " + (this == isClient));
        Debug.Log("Player == IsServer: " + (this == isServer));
        Debug.Log("Player == IsLocalPlayer: " + (this == isLocalPlayer));

        if (this == isClient && this == isServer && this == isLocalPlayer)
        {
            CmdRemoveAuthority(objectID);
        }
    }


    [Command]
    public void CmdAssignAuthority(NetworkIdentity objectID, NetworkIdentity playerID)
    {
        Debug.Log("Authority Assigned to: " + gameObject.transform.parent.transform.parent.transform.parent.gameObject);
        objectID.AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
    }

    [Command]
    public void CmdRemoveAuthority(NetworkIdentity objectID)
    {
        Debug.Log("Authority Removed from object.");
        objectID.RemoveClientAuthority();
    }
}
