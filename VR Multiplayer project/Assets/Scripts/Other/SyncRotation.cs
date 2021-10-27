using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRotation : MonoBehaviour
{
    GameObject playerCamera;
    GameObject visualRepresentation;
    Quaternion rot;


    private void Start()
    {
        playerCamera = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        visualRepresentation = gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
    }


    void Update()
    {
        rot = new Quaternion(0f, playerCamera.transform.rotation.y, 0f, 0f);
        visualRepresentation.transform.rotation = rot;
        
    }
}
