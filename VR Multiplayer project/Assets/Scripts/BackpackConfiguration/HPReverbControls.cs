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
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && GameObject.Find("ConversationManager").GetComponent<ConversationManager>().ActiveConversation == -1)
            {
                Debug.Log("Executed setactivecv");
                CmdSetConversation(1);
            }
            CmdUpdateAgressorText();
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(1).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && GameObject.Find("ConversationManager").GetComponent<ConversationManager>().ActiveConversation == -1)
            {
                CmdSetConversation(2);
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(2).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && GameObject.Find("ConversationManager").GetComponent<ConversationManager>().ActiveConversation == -1)
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
            TargetSetConversationNurse(nurseID.connectionToClient, currentConversation);
            TargetSetConversationAgressor(AgressorID.connectionToClient, currentConversation);
        }

    }

    [TargetRpc]
    public void TargetSetConversationNurse(NetworkConnection target, int currentConversation)
    {
        if (this.isClient && this.GetComponent<NetworkIdentity>().isLocalPlayer && this.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
        {
            conversationManagerNurse = GameObject.Find("ConversationManager").gameObject.GetComponent<ConversationManager>();
            conversationManagerNurse.ActiveConversation = currentConversation;
        }
    }

    [TargetRpc]
    public void TargetSetConversationAgressor(NetworkConnection target, int currentConversation)
    {
        agressor = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject;
        if (agressor.GetComponent<NetworkIdentity>().isClient && agressor.GetComponent<NetworkIdentity>().isLocalPlayer && agressor.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
        {
            conversationManagerAgressor = GameObject.Find("ConversationManager").gameObject.GetComponent<ConversationManager>();
            conversationManagerAgressor.ActiveConversation = currentConversation;
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateAgressorText()
    {
        NetworkIdentity AgressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        TargetUpdateAgressorText(AgressorID.connectionToClient);

    }

    [TargetRpc]
    public void TargetUpdateAgressorText(NetworkConnection target)
    {
        agressor = GameObject.FindGameObjectWithTag("Agressor").gameObject;
        textPopUp = agressor.transform.parent.transform.GetChild(3).gameObject;
        List<ConversationElement> activeReactionElements = new List<ConversationElement>();
        //Debug.Log(conversationManagerAgressor.GetActiveConversation().startElement/*.ToString().ReactionElements[0].Text*/);
        activeReactionElements = conversationManagerServer.GetComponent<ConversationManager>().GetActiveConversation().ActiveElement.ReactionElements;
        textPopUp.SetActive(true);
        textPopUp.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[0].ToString();
        textPopUp.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[1].ToString();
        textPopUp.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[2].ToString();
        UpdateAgressorText();
    }

    public void UpdateAgressorText()
    {
        
    }
}


