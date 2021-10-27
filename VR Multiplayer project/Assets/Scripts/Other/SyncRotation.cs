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
        visualRepresentation.transform.up = playerCamera.transform.up;
    }
}
