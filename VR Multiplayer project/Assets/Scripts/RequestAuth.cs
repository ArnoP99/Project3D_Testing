using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestAuth : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = this.transform.parent.transform.parent.transform.parent.gameObject;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            player.GetComponent<AssignAuth>().ExecuteCmdAssignAuthority(other.GetComponent<NetworkIdentity>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            player.GetComponent<AssignAuth>().ExecuteCmdRemoveAuthority(other.GetComponent<NetworkIdentity>());
        }
    }
}
