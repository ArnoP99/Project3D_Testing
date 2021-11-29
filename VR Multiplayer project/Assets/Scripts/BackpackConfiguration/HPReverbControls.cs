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

    public List<ConversationElement> activeReactionElements = new List<ConversationElement>();
    int test;

    ConversationManager conversationManagerServer;
    ConversationManager conversationManagerNurse;
    ConversationManager conversationManagerAgressor;

    private void Start()
    {
        conversationManagerAgressor = new ConversationManager();
        conversationManagerNurse = new ConversationManager();
        conversationManagerServer = new ConversationManager();


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
                CmdSetConversation(1);
            }
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && activeReactionElements.Count > 0)
            {
                CmdUpdateActiveElement(1);
                CmdUpdateAgressorText();
            }
            else
            {
                CmdUpdateAgressorText();
            }
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && GameObject.Find("ConversationManager").GetComponent<ConversationManager>().ActiveConversation == -1)
            {
                CmdSetConversation(2);
            }
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && activeReactionElements.Count > 0)
            {
                CmdUpdateActiveElement(2);
                CmdUpdateAgressorText();
            }
            else
            {
                CmdUpdateAgressorText();
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && GameObject.Find("ConversationManager").GetComponent<ConversationManager>().ActiveConversation == -1)
            {
                CmdSetConversation(3);
            }
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && activeReactionElements.Count > 0)
            {
                CmdUpdateActiveElement(3);
                CmdUpdateAgressorText();
            }
            else
            {
                CmdUpdateAgressorText();
            }
        }
        else
        {
            Debug.Log("No Active Choice Found.");
        }
    }

    public void GetAgressorActiveChoice()
    {
        agressor = GameObject.FindGameObjectWithTag("Agressor");
        textPopUp = agressor.transform.parent.transform.GetChild(3).gameObject;

        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                CmdUpdateActiveElement(1); 
                CmdUpdateNurseText();
            }
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(1).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                CmdUpdateActiveElement(2); 
                CmdUpdateNurseText();
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(2).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                CmdUpdateActiveElement(3); 
                CmdUpdateNurseText();
            }
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
        activeReactionElements = conversationManagerServer.GetActiveConversation().activeElement.ReactionElements;
        NetworkIdentity AgressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        TargetUpdateAgressorText(AgressorID.connectionToClient);
    }

    [TargetRpc]
    public void TargetUpdateAgressorText(NetworkConnection target)
    {
        agressor = GameObject.FindGameObjectWithTag("Agressor").gameObject;
        textPopUp = agressor.transform.parent.transform.GetChild(3).gameObject;

        activeReactionElements = conversationManagerAgressor.GetActiveConversation().activeElement.ReactionElements;

        //Debug.Log(conversationManagerAgressor.GetActiveConversation().startElement/*.ToString().ReactionElements[0].Text*/);
        //Debug.Log(conversationManagerAgressor.GetComponent<ConversationManager>().GetActiveConversation()/*.ActiveElement.ReactionElements*/);
        textPopUp.SetActive(true);

        // als er geen 3 reacties zijn ... -> hier moeten we nog op controleren
        textPopUp.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[0].Text;
        textPopUp.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[1].Text;
        textPopUp.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[2].Text;
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateNurseText()
    {
        activeReactionElements = conversationManagerServer.GetActiveConversation().activeElement.ReactionElements;
        NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        TargetUpdateAgressorText(nurseID.connectionToClient);
    }

    [TargetRpc]
    public void TargetUpdateNurseText(NetworkConnection target)
    {
        nurse = GameObject.FindGameObjectWithTag("Nurse").gameObject;
        textPopUp = nurse.transform.parent.transform.GetChild(3).gameObject;

        activeReactionElements = conversationManagerNurse.GetActiveConversation().activeElement.ReactionElements;

        //Debug.Log(conversationManagerAgressor.GetActiveConversation().startElement/*.ToString().ReactionElements[0].Text*/);
        //Debug.Log(conversationManagerAgressor.GetComponent<ConversationManager>().GetActiveConversation()/*.ActiveElement.ReactionElements*/);
        textPopUp.SetActive(true);

        // als er geen 3 reacties zijn ... -> hier moeten we nog op controleren
        textPopUp.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[0].Text;
        textPopUp.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[1].Text;
        textPopUp.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[2].Text;
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateActiveElement(int activeChoice)
    {
        if (activeChoice == 1)
        {
            conversationManagerServer.GetActiveConversation().activeElement = activeReactionElements[0];
        }
        if (activeChoice == 2)
        {
            conversationManagerServer.GetActiveConversation().activeElement = activeReactionElements[1];
        }
        if (activeChoice == 3)
        {
            conversationManagerServer.GetActiveConversation().activeElement = activeReactionElements[2];
        }

        RpcUpdateActiveElement(activeChoice);
    }

    [ClientRpc(includeOwner = false)]
    public void RpcUpdateActiveElement(int activeChoice)
    {
        if (conversationManagerAgressor != null)
        {
            if (activeChoice == 1)
            {
                conversationManagerAgressor.GetActiveConversation().activeElement = activeReactionElements[0];
            }
            if (activeChoice == 2)
            {
                conversationManagerAgressor.GetActiveConversation().activeElement = activeReactionElements[1];
            }
            if (activeChoice == 3)
            {
                conversationManagerAgressor.GetActiveConversation().activeElement = activeReactionElements[2];
            }
        }

        if (conversationManagerNurse != null)
        {
            if (activeChoice == 1)
            {
                conversationManagerNurse.GetActiveConversation().activeElement = activeReactionElements[0];
            }
            if (activeChoice == 2)
            {
                conversationManagerNurse.GetActiveConversation().activeElement = activeReactionElements[1];
            }
            if (activeChoice == 3)
            {
                conversationManagerNurse.GetActiveConversation().activeElement = activeReactionElements[2];
            }
        }
    }
}


