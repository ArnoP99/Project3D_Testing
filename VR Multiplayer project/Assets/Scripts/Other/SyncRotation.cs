using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRotation : MonoBehaviour
{
    GameObject playerCamera;
    GameObject visualRepresentation;
    GameObject textPlayer;
    Vector3 rot;


    private void Start()
    {
        playerCamera = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        visualRepresentation = gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
        textPlayer = gameObject.transform.GetChild(0).transform.GetChild(3).gameObject;
    }


    void Update()
    {
        rot = new Vector3(0, playerCamera.transform.eulerAngles.y, 0);
        visualRepresentation.transform.eulerAngles = rot;
        if (playerCamera.transform.eulerAngles.y - textPlayer.transform.eulerAngles.y < -90 || playerCamera.transform.eulerAngles.y - textPlayer.transform.eulerAngles.y > 90)
        {
            Debug.Log("rotated to far");
        }



        textPlayer.transform.eulerAngles = rot;
    }
}
