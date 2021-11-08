using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class NetworkEvents : NetworkBehaviour
{
    private void Start()
    {
        NetworkServer.Spawn(gameObject);
    }


    [Command]
    public void CmdUpdateNurse(GameObject nursePlayer)
    {
        Debug.Log("Cmd call before rpc");
        RpcUpdateNurse(nursePlayer);
        Debug.Log("Cmd call after rpc");
    }

    [ClientRpc]
    public void RpcUpdateNurse(GameObject nursePlayer)
    {
        Debug.Log("Rpc call");
    }

        //if (visualRep != null && visualRep.transform.GetChild(0) != null)
        //{
        //    visualRep.transform.GetChild(0).gameObject.SetActive(false);
        //}
        //Instantiate(prefabNurse, currentPos, Quaternion.identity, visualRep.transform);
        //gameManager.CheckForTwoPlayers(2, player); // Tell gamemanager an agressor has been initialized.
        //Debug.Log("TestNurse  :" + player);
}
