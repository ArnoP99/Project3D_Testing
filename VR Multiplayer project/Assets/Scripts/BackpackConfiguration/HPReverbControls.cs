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

    public List<ConversationElement> activeReactionElements;
    bool firstTime;

    ConversationManager conversationManagerServer;
    ConversationManager conversationManagerNurse;
    ConversationManager conversationManagerAgressor;

    private void Start()
    {
        conversationManagerAgressor = new ConversationManager();
        conversationManagerNurse = new ConversationManager();
        conversationManagerServer = new ConversationManager();

        if (activeReactionElements == null)
        {
            activeReactionElements = new List<ConversationElement>();
        }
        Debug.Log("Debug start ARE: " + activeReactionElements);
        Debug.Log("Debug Start ARE count: " + activeReactionElements.Count);
        //activeReactionElements = new List<ConversationElement>();

        firstTime = true;

        if (this.isServer && conversationManagerServer == null)
        {
            conversationManagerServer = GameObject.Find("ConversationManager").gameObject.GetComponent<ConversationManager>();
        }
        if (this.isClient && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse" && this.GetComponent<NetworkIdentity>().isLocalPlayer && conversationManagerNurse == null)
        {
            conversationManagerNurse = GameObject.Find("ConversationManager").gameObject.GetComponent<ConversationManager>();
        }
        if (this.isClient && gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor" && this.GetComponent<NetworkIdentity>().isLocalPlayer && conversationManagerAgressor == null)
        {
            conversationManagerAgressor = GameObject.Find("ConversationManager").gameObject.GetComponent<ConversationManager>();
        }
    }
    public void PressTrigger(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger Pressed");
        if (context.performed)
        {
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

        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red && nurse.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && GameObject.Find("ConversationManager").GetComponent<ConversationManager>().ActiveConversation == -1)
            {
                CmdSetConversation(1);
            }

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && firstTime == false)
            {
                CmdUpdateActiveElement(1);
                CmdUpdateAgressorText();
            }
            else
            {
                CmdUpdateAgressorText();
                firstTime = false;
            }
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red && nurse.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(1).gameObject;
            textPopUp.SetActive(false);

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && GameObject.Find("ConversationManager").GetComponent<ConversationManager>().ActiveConversation == -1)
            {
                CmdSetConversation(2);
            }

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && firstTime == false)
            {
                CmdUpdateActiveElement(2);
                CmdUpdateAgressorText();
            }
            else
            {
                CmdUpdateAgressorText();
                firstTime = false;
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red && nurse.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(2).gameObject;
            textPopUp.SetActive(false);

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && GameObject.Find("ConversationManager").GetComponent<ConversationManager>().ActiveConversation == -1)
            {
                CmdSetConversation(3);
            }

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && firstTime == false)
            {
                CmdUpdateActiveElement(3);
                CmdUpdateAgressorText();
            }
            else
            {
                CmdUpdateAgressorText();
                firstTime = false;
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

        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red && agressor.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                Debug.Log("AgressorChoice ARE: " + activeReactionElements.Count);
                Debug.Log("AgressorChoice CVM RE: " + conversationManagerAgressor.GetActiveConversation().activeElement.ReactionElements.Count);
                Debug.Log(conversationManagerAgressor.GetActiveConversation().activeElement.Text);
                activeReactionElements = conversationManagerAgressor.GetActiveConversation().activeElement.ReactionElements;
                Debug.Log("AgressorChoice ARE: " + activeReactionElements.Count);
                CmdUpdateActiveElement(1);
                CmdUpdateNurseText();
            }
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red && agressor.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(1).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                Debug.Log("AgressorChoice ARE: " + activeReactionElements.Count);
                CmdUpdateActiveElement(2);
                CmdUpdateNurseText();
            }
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red && agressor.transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            activeChoice = textPopUp.transform.GetChild(2).gameObject;
            textPopUp.SetActive(false);
            if (gameObject.GetComponent<NetworkIdentity>().isClient == true)
            {
                Debug.Log("AgressorChoice ARE: " + activeReactionElements.Count);
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
            activeReactionElements = conversationManagerServer.GetActiveConversation().activeElement.ReactionElements;
            Debug.Log(conversationManagerServer.ActiveConversation);
            Debug.Log("Cmd SCV: " + activeReactionElements.Count);
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
        Debug.Log("Cmd UAT: " + activeReactionElements.Count);
        NetworkIdentity AgressorID = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        TargetUpdateAgressorText(AgressorID.connectionToClient);
    }

    [TargetRpc]
    public void TargetUpdateAgressorText(NetworkConnection target)
    {
        agressor = GameObject.FindGameObjectWithTag("Agressor").gameObject;
        textPopUp = agressor.transform.parent.transform.GetChild(3).gameObject;
        Debug.Log(activeReactionElements.Count);
        Debug.Log(conversationManagerAgressor.GetActiveConversation().activeElement.Text);
        activeReactionElements = conversationManagerAgressor.GetActiveConversation().activeElement.ReactionElements;
        Debug.Log("Target UAT: " + activeReactionElements.Count);
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
        Debug.Log("Cmd UNT: " + activeReactionElements.Count);
        NetworkIdentity nurseID = GameObject.FindGameObjectWithTag("Nurse").transform.parent.transform.parent.gameObject.GetComponent<NetworkIdentity>();
        TargetUpdateNurseText(nurseID.connectionToClient);
    }

    [TargetRpc]
    public void TargetUpdateNurseText(NetworkConnection target)
    {
        nurse = GameObject.FindGameObjectWithTag("Nurse").gameObject;
        textPopUp = nurse.transform.parent.transform.GetChild(3).gameObject;
        Debug.Log(activeReactionElements.Count);
        activeReactionElements = conversationManagerNurse.GetActiveConversation().activeElement.ReactionElements;
        Debug.Log("Target UNT: " + activeReactionElements.Count);
        textPopUp.SetActive(true);

        // als er geen 3 reacties zijn ... -> hier moeten we nog op controleren
        textPopUp.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[0].Text;
        textPopUp.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[1].Text;
        textPopUp.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = activeReactionElements[2].Text;
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateActiveElement(int activeChoice)
    {
        if (this.isServer)
        {
            activeReactionElements = conversationManagerServer.GetActiveConversation().activeElement.ReactionElements;
            Debug.Log("Cmd UAE: " + activeReactionElements.Count);
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
    }

    [ClientRpc(includeOwner = false)]
    public void RpcUpdateActiveElement(int activeChoice)
    {
        if (conversationManagerAgressor != null)
        {
            activeReactionElements = conversationManagerAgressor.GetActiveConversation().activeElement.ReactionElements;
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
            activeReactionElements = conversationManagerNurse.GetActiveConversation().activeElement.ReactionElements;
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


