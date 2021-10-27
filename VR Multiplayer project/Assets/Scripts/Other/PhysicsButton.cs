using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PhysicsButton : NetworkBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    [SerializeField] public GameObject prefabAgressor;
    [SerializeField] public GameObject prefabNurse;

    private bool isPressed = false;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    private Vector3 currentPos;

    public UnityEvent onPressed, onReleased;

    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();


        if (isServer)
        {
            List<GameObject> knoppen = new List<GameObject>();
            knoppen.Add(GameObject.FindGameObjectWithTag("AgressorButton"));
            knoppen.Add(GameObject.FindGameObjectWithTag("NurseButton"));
            knoppen.Add(GameObject.FindGameObjectWithTag("SceneButton"));

            foreach (var knop in knoppen)
            {
                NetworkServer.Spawn(knop);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        Pressed();
        currentPos = collision.transform.parent.transform.parent.position;

        if (collision.gameObject.tag == "RightController" || collision.gameObject.tag == "LeftController")
        {
            GameObject visualRep = collision.gameObject.transform.parent.transform.parent.Find("VisualRepresentation").gameObject;
            GameObject player = collision.gameObject.transform.parent.transform.parent.transform.parent.gameObject;

            if (isServer)
            {
                gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(player.GetComponent<NetworkIdentity>().connectionToClient);
            }
            if (isClient)
            {
                CmdMessageTest(player);
            }

            if (gameObject.tag == "AgressorButton")
            {
                player.tag = "Agressor";
                Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
                Instantiate(prefabAgressor, currentPos, Quaternion.identity, visualRep.transform);
                GameManager.CheckForTwoPlayers(2); // Tell gamemanager an agressor has been initialized.
            }
            if (gameObject.tag == "NurseButton")
            {
                player.tag = "Nurse";
                Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
                Instantiate(prefabNurse, currentPos, Quaternion.identity, visualRep.transform);
                GameManager.CheckForTwoPlayers(1); // Tell gamemanager a nurse has been initialized.
            }
            if (gameObject.tag == "SceneButton")
            {
                SceneManager.LoadScene("ZiekenhuisKamer");
                Scene ziekenHuisKamer = SceneManager.GetSceneByName("ZiekenhuisKamer");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone)
        {
            value = 0;
        }
        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        Debug.Log("Pressed");
        onPressed.Invoke();

    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }

    [Command]
    void CmdMessageTest(GameObject player)
    {
        Debug.Log("This is a message run from the server, initiated by the player: " + player.name);
    }

}
