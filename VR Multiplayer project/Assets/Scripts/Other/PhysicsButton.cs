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



    GameManager gameManager;

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


            GameObject visualRep = collision.gameObject.transform.parent.transform.parent.GetChild(2).gameObject;
            GameObject player = collision.gameObject.transform.parent.transform.parent.transform.parent.gameObject;

            Debug.Log("VisualRep = " + visualRep);

            //if (isServer)
            //{
            //    //RpcTest();
            //    TargetTest(player.GetComponent<NetworkIdentity>().connectionToClient);

            //}
            //if (isClient)
            //{
            //    CmdMessageTest(player);
            //}

            if (gameObject.tag == "AgressorButton")
            {

                //CmdUpdateAgressor(player);
                if (isClient)
                {
                    CmdUpdateAgressor(player, visualRep);
                }
                //player.tag = "Agressor";
                //Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
                //Instantiate(prefabAgressor, currentPos, Quaternion.identity, visualRep.transform);
                //GameManager.CheckForTwoPlayers(2, player); // Tell gamemanager an agressor has been initialized.
            }
            if (gameObject.tag == "NurseButton")
            {

                //CmdUpdateNurse(player);

                if (isClient)
                {
                    CmdUpdateNurse(player, visualRep);
                }

                //player.tag = "Nurse";
                //Destroy(visualRep.transform.gameObject.transform.GetChild(0).gameObject);
                //Instantiate(prefabNurse, currentPos, Quaternion.identity, visualRep.transform);
                //GameManager.CheckForTwoPlayers(2, player); // Tell gamemanager an agressor has been initialized.

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

    //[Command(requiresAuthority = false)]
    //void CmdMessageTest(GameObject player)
    //{
    //    Debug.Log("This is a message run from the server, initiated by the player: " + player.GetComponent<NetworkIdentity>().netId);
    //}

    //[ClientRpc(includeOwner = false)]
    //public void RpcTest()
    //{
    //    Debug.Log("Message from Server To Client");
    //}

    //[TargetRpc]
    //public void TargetTest(NetworkConnection target)
    //{
    //    Debug.Log("server to specific target");
    //}

    [Command(requiresAuthority = false)]
    public void CmdUpdateNurse(GameObject player, GameObject visualRep)
    {
        player.tag = "Nurse";
        if (visualRep != null && visualRep.transform.GetChild(0) != null)
        {
            visualRep.transform.GetChild(0).gameObject.SetActive(false);
        }
        Instantiate(prefabNurse, currentPos, Quaternion.identity, visualRep.transform);
        //gameManager.CheckForTwoPlayers(2, player); // Tell gamemanager an agressor has been initialized.
        Debug.Log("TestNurse  :" + player);
    }


    [Command(requiresAuthority = false)]
    public void CmdUpdateAgressor(GameObject player, GameObject visualRep)
    {
        player.tag = "Agressor";
        if (visualRep != null && visualRep.transform.GetChild(0) != null)
        {
            visualRep.transform.GetChild(0).gameObject.SetActive(false);
        }
        Instantiate(prefabAgressor, currentPos, Quaternion.identity, visualRep.transform);
        //gameManager.CheckForTwoPlayers(2, player); // Tell gamemanager an agressor has been initialized.
        Debug.Log("TestAgressor  :" + player);
    }

}
