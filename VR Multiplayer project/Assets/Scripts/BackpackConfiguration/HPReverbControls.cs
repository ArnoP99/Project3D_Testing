using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HPReverbControls : NetworkBehaviour
{
    public GameObject nurse;
    public GameObject agressor;
    public GameObject textPopUp;
    public GameObject activeChoice;
    public bool triggerValue = true;

    int test;

    ConversationManager conversationManagerServer;
    ConversationManager conversationManagerNurse;
    ConversationManager conversationManagerAgressor;

    private void Start()
    {
        if (this.isServer)
        {
            conversationManagerServer = GameObject.Find("ConversationManager").gameObject.GetComponent<ConversationManager>();
        }
        if (this.isClient && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
        {
            conversationManagerNurse = GameObject.Find("ConversationManager").gameObject.GetComponent<ConversationManager>();
        }
        if (this.isClient && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
        {
            conversationManagerAgressor = GameObject.Find("ConversationManager").gameObject.GetComponent<ConversationManager>();
        }
    }
    public void PressTrigger(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger Pressed");
        try
        {
            GetAgressorActiveChoice();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

        try
        {
            GetNurseActiveChoice();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

    public void Joystick(InputAction.CallbackContext context)
    {
        //Debug.Log("Joystick");
    }

    public void PrimaryButton(InputAction.CallbackContext context)
    {
        //Debug.Log("PrimaryButton Pressed");

        //if (triggerValue == true)
        //{
        //    this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = false;

        //    this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<BoxCollider>().isTrigger = false;

        //    triggerValue = false;
        //}
        //else if (triggerValue == false)
        //{
        //    this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;

        //    this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<BoxCollider>().isTrigger = true;

        //    triggerValue = true;
        //}
    }

    public void GetNurseActiveChoice()
    {
        nurse = GameObject.FindGameObjectWithTag("Nurse");
        textPopUp = nurse.transform.parent.transform.GetChild(3).gameObject;

        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                CmdSetConversation(1);
            }

        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(1).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                CmdSetConversation(2);
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(2).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                CmdSetConversation(3);
            }
        }
        else
        {
            Debug.Log("No Active Choice Found.");
        }

        Debug.Log("ActiveChoice Nurse: " + activeChoice);
    }

    public void GetAgressorActiveChoice()
    {
        agressor = GameObject.FindGameObjectWithTag("Agressor");
        textPopUp = agressor.transform.parent.transform.GetChild(3).gameObject;

        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);

        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(1).gameObject;
            textPopUp.SetActive(false);

        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(2).gameObject;
            textPopUp.SetActive(false);

        }
        else
        {
            Debug.Log("No Active Choice Found.");
        }

        Debug.Log("ActiveChoice Agressor: " + activeChoice);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetConversation(int currentConversation)
    {
        if (gameObject.GetComponent<NetworkIdentity>().isServer)
        {
            conversationManagerServer.ActiveConversation = currentConversation;
            Debug.Log(conversationManagerServer.ActiveConversation);
            NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            NetworkIdentity AgressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
            TargetSetConversation(nurseID.connectionToClient, currentConversation);
            TargetSetConversation(AgressorID.connectionToClient, currentConversation);
        }

    }




    [TargetRpc]
    public void TargetSetConversation(NetworkConnection target, int currentConversation)
    {
        Debug.Log("Dit is enkel op de client te zien");

        if (this.isClient && this.GetComponent<NetworkIdentity>().isLocalPlayer && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
        {
            Debug.Log("Nurse executed");
            conversationManagerNurse.ActiveConversation = currentConversation;
        }
        if (this.isClient && this.GetComponent<NetworkIdentity>().isLocalPlayer && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
        {
            Debug.Log("Agressor executed");
            conversationManagerAgressor.ActiveConversation = currentConversation;
        }

    }
}
