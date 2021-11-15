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
        Debug.Log("ReqAuth Player: " + player);
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Other GO: " + other.gameObject);
        Debug.Log("Other GO Layer: " + other.gameObject.layer);
        Debug.Log("Other GO NetID: " + other.GetComponent<NetworkIdentity>());

        if (other.gameObject.layer == 9)
        {
            player.GetComponent<AssignAuth>().ExecuteCmdAssignAuthority(other.GetComponent<NetworkIdentity>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Other GO: " + other.gameObject);
        Debug.Log("Other GO Layer: " + other.gameObject.layer);
        Debug.Log("Other GO NetID: " + other.GetComponent<NetworkIdentity>());

        if (other.gameObject.layer == 9)
        {
            player.GetComponent<AssignAuth>().ExecuteCmdRemoveAuthority(other.GetComponent<NetworkIdentity>());
        }
    }
}
