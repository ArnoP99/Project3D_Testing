using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class SyncRotation : NetworkBehaviour
{
    GameObject playerCamera;
    GameObject visualRepresentation;
    GameObject textPlayer;

    Vector3 rot;

    private void Start()
    {
        if (this.isLocalPlayer)
        {
            playerCamera = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
            visualRepresentation = gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
            textPlayer = gameObject.transform.GetChild(0).transform.GetChild(3).gameObject;
        }
    }


    void Update()
    {
        if (this.isLocalPlayer) { }
        rot = new Vector3(0, playerCamera.transform.eulerAngles.y, 0);
        visualRepresentation.transform.eulerAngles = rot;
        if (playerCamera.transform.eulerAngles.y - textPlayer.transform.eulerAngles.y < -180 || playerCamera.transform.eulerAngles.y - textPlayer.transform.eulerAngles.y > 180)
        {
            textPlayer.transform.eulerAngles = rot;
        }
    }
}
