using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HPReverbControls : NetworkBehaviour
{
    GameObject nurse;
    GameObject agressor;
    GameObject textPopUp;
    GameObject activeChoice;
    bool triggerValue = true;

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
        Debug.Log("IsServer: " + gameObject.GetComponent<NetworkIdentity>().isServer);
        Debug.Log("Netid CVM in cmd: " + GameObject.Find("ConversationManager").gameObject.GetComponent<NetworkIdentity>().netId);
        if (gameObject.GetComponent<NetworkIdentity>().isServer)
        {
            if (ConversationManager.Instance.ActiveConversation != ConversationManager.Instance.GeneralCheckupConversation && ConversationManager.Instance.ActiveConversation != ConversationManager.Instance.TimeForMedicationConversation && ConversationManager.Instance.ActiveConversation != ConversationManager.Instance.HelpButtonConversation)
            {
                if (currentConversation == 1)
                {
                    ConversationManager.Instance.ActiveConversation = ConversationManager.Instance.GeneralCheckupConversation;
                    ConversationManager.Instance.GeneralCheckupConversation.CurrentState = Conversation.ConversationState.Started;
                }
                else if (currentConversation == 2)
                {
                    ConversationManager.Instance.ActiveConversation = ConversationManager.Instance.TimeForMedicationConversation;
                    ConversationManager.Instance.TimeForMedicationConversation.CurrentState = Conversation.ConversationState.Started;
                }
                else if (currentConversation == 3)
                {
                    ConversationManager.Instance.ActiveConversation = ConversationManager.Instance.HelpButtonConversation;
                    ConversationManager.Instance.HelpButtonConversation.CurrentState = Conversation.ConversationState.Started;
                }
            }
        }
        
        RpcSetConversation(currentConversation);
    }

    [ClientRpc(includeOwner = false)]
    public void RpcSetConversation(int currentConversation)
    {
        Debug.Log("Netid CVM in cmd: " + GameObject.Find("ConversationManager").gameObject.GetComponent<NetworkIdentity>().netId);
        if (ConversationManager.Instance.ActiveConversation != ConversationManager.Instance.GeneralCheckupConversation && ConversationManager.Instance.ActiveConversation != ConversationManager.Instance.TimeForMedicationConversation && ConversationManager.Instance.ActiveConversation != ConversationManager.Instance.HelpButtonConversation)
        {
            if (currentConversation == 1)
            {
                ConversationManager.Instance.ActiveConversation = ConversationManager.Instance.GeneralCheckupConversation;
                ConversationManager.Instance.GeneralCheckupConversation.CurrentState = Conversation.ConversationState.Started;
            }
            else if (currentConversation == 2)
            {
                ConversationManager.Instance.ActiveConversation = ConversationManager.Instance.TimeForMedicationConversation;
                ConversationManager.Instance.TimeForMedicationConversation.CurrentState = Conversation.ConversationState.Started;
            }
            else if (currentConversation == 3)
            {
                ConversationManager.Instance.ActiveConversation = ConversationManager.Instance.HelpButtonConversation;
                ConversationManager.Instance.HelpButtonConversation.CurrentState = Conversation.ConversationState.Started;
            }
        }
    }
}
