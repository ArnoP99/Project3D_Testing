using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRotation : MonoBehaviour
{
    GameObject playerCamera;
    GameObject visualRepresentation;
    Vector3 rot;


    private void Start()
    {
        playerCamera = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        visualRepresentation = gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
    }


    void Update()
    {
        rot = new Vector3(0, playerCamera.transform.eulerAngles.y, 0);
        visualRepresentation.transform.eulerAngles = rot;
    }
}
