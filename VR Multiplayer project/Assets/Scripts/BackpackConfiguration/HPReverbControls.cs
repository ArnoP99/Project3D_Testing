﻿using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HPReverbControls : NetworkBehaviour
{
    public GameObject nurse;
    public GameObject agressor;
    public GameObject textPopUp;
    public GameObject activeChoice;
    public bool triggerValue = true;

    public List<ConversationElement> activeReactionElements;
    bool firstTime;

    private void Start()
    {
        if (activeReactionElements == null)
        {
            activeReactionElements = new List<ConversationElement>();
        }
        Debug.Log("Debug start ARE: " + activeReactionElements);
        Debug.Log("Debug Start ARE count: " + activeReactionElements.Count);
        //activeReactionElements = new List<ConversationElement>();

        firstTime = true;


    }
    public void PressTrigger(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger Pressed");
        if (context.performed)
        {
            if (this.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
            {
                Debug.Log("Agressor kiest optie");
                try
                {
                    GetAgressorActiveChoice();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            }

            if (this.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
            {
                Debug.Log("Nurse kiest optie");
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
    }

    private void Update()
    {
        Debug.Log(ConversationManager.Instance.GetActiveConversation());
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

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && ConversationManager.Instance.ActiveConversation == -1)
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

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && ConversationManager.Instance.ActiveConversation == -1)
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

            if (gameObject.GetComponent<NetworkIdentity>().isClient == true && ConversationManager.Instance.ActiveConversation == -1)
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
                Debug.Log("AgressorChoice CVM RE: " + ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements.Count);
                Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
                activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
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
                Debug.Log("AgressorChoice CVM RE: " + ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements.Count);
                Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
                activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
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
                Debug.Log("AgressorChoice CVM RE: " + ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements.Count);
                Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
                activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
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
            ConversationManager.Instance.ActiveConversation = currentConversation;
            activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            Debug.Log(ConversationManager.Instance.ActiveConversation);
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
        Debug.Log("hallo nurse");
        nurse = GameObject.Find("Nurse").transform.parent.transform.parent.gameObject;
        if (nurse.GetComponent<NetworkIdentity>().isClient && nurse.GetComponent<NetworkIdentity>().isLocalPlayer && nurse.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Nurse")
        {
            ConversationManager.Instance.ActiveConversation = currentConversation;
            Debug.Log("Active conversation nurse set");
        }
    }

    [TargetRpc]
    public void TargetSetConversationAgressor(NetworkConnection target, int currentConversation)
    {
        Debug.Log("hallo agressor");
        agressor = GameObject.FindGameObjectWithTag("Agressor").transform.parent.transform.parent.gameObject;
        if (agressor.GetComponent<NetworkIdentity>().isClient && agressor.GetComponent<NetworkIdentity>().isLocalPlayer && agressor.transform.GetChild(0).transform.GetChild(2).gameObject.tag == "Agressor")
        {
            ConversationManager.Instance.ActiveConversation = currentConversation;
            Debug.Log("Active conversation agressor set");
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdUpdateAgressorText()
    {
        activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
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
        Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
        activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
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
        activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
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
        Debug.Log(ConversationManager.Instance.GetActiveConversation());
        Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.Text);
        Debug.Log(ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements.Count);
        activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
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
            activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            Debug.Log("Cmd UAE: " + activeReactionElements.Count);
            if (activeChoice == 1)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = activeReactionElements[0];
            }
            if (activeChoice == 2)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = activeReactionElements[1];
            }
            if (activeChoice == 3)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = activeReactionElements[2];
            }

            RpcUpdateActiveElement(activeChoice);
        }
    }

    [ClientRpc(includeOwner = false)]
    public void RpcUpdateActiveElement(int activeChoice)
    {
        if (ConversationManager.Instance != null)
        {
            activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            if (activeChoice == 1)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = activeReactionElements[0];
            }
            if (activeChoice == 2)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = activeReactionElements[1];
            }
            if (activeChoice == 3)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = activeReactionElements[2];
            }
        }

        if (ConversationManager.Instance != null)
        {
            activeReactionElements = ConversationManager.Instance.GetActiveConversation().activeElement.ReactionElements;
            if (activeChoice == 1)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = activeReactionElements[0];
            }
            if (activeChoice == 2)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = activeReactionElements[1];
            }
            if (activeChoice == 3)
            {
                ConversationManager.Instance.GetActiveConversation().activeElement = activeReactionElements[2];
            }
        }
    }
}


