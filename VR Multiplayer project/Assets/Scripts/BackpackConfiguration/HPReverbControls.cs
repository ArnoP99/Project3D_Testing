﻿using Mirror;
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

    ConversationManager conversationManager;

    private void Start()
    {
        conversationManager = new ConversationManager();
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
            conversationManager.ActiveConversation = currentConversation;
            Debug.Log(conversationManager.ActiveConversation);
            RpcSetConversation(currentConversation);
        }

    }




    [ClientRpc(includeOwner = true)]
    public void RpcSetConversation(int currentConversation)
    {
        if (gameObject.GetComponent<NetworkIdentity>().isClient)
        {
            conversationManager.ActiveConversation = currentConversation;
            Debug.Log("cm acv: " + conversationManager.ActiveConversation);
        }
    }
}
