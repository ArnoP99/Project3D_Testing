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
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3F;

    private void Start()
    {
        if (isLocalPlayer)
        {
            playerCamera = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
            visualRepresentation = gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
            textPlayer = gameObject.transform.GetChild(0).transform.GetChild(3).gameObject;
        }
    }


    void Update()
    {
        if (isLocalPlayer)
        {
            rot = new Vector3(0, playerCamera.transform.eulerAngles.y, 0);
            visualRepresentation.transform.eulerAngles = rot;
            if (playerCamera.transform.eulerAngles.y - textPlayer.transform.eulerAngles.y < -110 || playerCamera.transform.eulerAngles.y - textPlayer.transform.eulerAngles.y > 110)
            {
                textPlayer.transform.eulerAngles = Vector3.SmoothDamp(textPlayer.transform.position, rot, ref velocity, smoothTime);
            }
        }
    }
}
