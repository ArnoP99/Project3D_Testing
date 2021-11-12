using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : NetworkBehaviour
{
    void Update()
    {
        CmdSyncPosRot(transform.localPosition, transform.localRotation);

    }

    [Command(requiresAuthority = false)]
    void CmdSyncPosRot(Vector3 localPosition, Quaternion localRotation)
    {
        RpcSyncPosRot(localPosition, localRotation);
    }

    [ClientRpc]
    void RpcSyncPosRot(Vector3 localPosition, Quaternion localRotation)
    {
        if (!isLocalPlayer)
        {
            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
        }
    }
}
