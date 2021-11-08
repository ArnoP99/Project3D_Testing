using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using System;

public class HPReverbControls : NetworkBehaviour
{

    int choice = 0;

    ConversationManager conversationManager;

    GameObject nurse;
    GameObject agressor;

    GameObject nurseTextPopUp;
    GameObject agressorTextPopUp;


    public void PressTrigger(InputAction.CallbackContext context)
    {

        try
        {
            // Try to find nurse or agressor on localPlayer client
            if (isLocalPlayer)
            {
                nurse = GameObject.FindGameObjectWithTag("Nurse");
                agressor = GameObject.FindGameObjectWithTag("Agressor");

                nurseTextPopUp = nurse.transform.GetChild(0).transform.GetChild(3).gameObject;
                agressorTextPopUp = agressor.transform.GetChild(0).transform.GetChild(3).gameObject;
            }
            if (nurseTextPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red)
            {
                if (choice == 0)
                {
                    choice = 1;
                    ConversationManager.ChooseConversation(choice);
                }
                if (isServer)
                {
                    //conversationManager.TargetUpdateConversation(ConversationManager.target);
                }
            }
            else if (nurseTextPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
            {
                if (choice == 0)
                {
                    choice = 2;
                    ConversationManager.ChooseConversation(choice);
                }
                if (isServer)
                {
                    //conversationManager.TargetUpdateConversation(ConversationManager.target);
                }
            }
            else if (nurseTextPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
            {
                if (choice == 0)
                {
                    choice = 3;
                    ConversationManager.ChooseConversation(choice);
                }
                if (isServer)
                {
                    //conversationManager.TargetUpdateConversation(ConversationManager.target);
                }
            }
            else
            {
                Debug.Log("No Active Choice Found.");
            }

            // ----------------------------------------------------------------------------------------------------------------------------

            if (agressorTextPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red)
            {
                if (choice == 0)
                {
                    choice = 1;
                    ConversationManager.ChooseConversation(choice);
                }
                if (isServer)
                {
                    //conversationManager.TargetUpdateConversation(ConversationManager.target);
                }
            }
            else if (agressorTextPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
            {
                if (choice == 0)
                {
                    choice = 2;
                    ConversationManager.ChooseConversation(choice);
                }
                if (isServer)
                {
                    //conversationManager.TargetUpdateConversation(ConversationManager.target);
                }
            }
            else if (agressorTextPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
            {
                if (choice == 0)
                {
                    choice = 3;
                    ConversationManager.ChooseConversation(choice);
                }
                if (isServer)
                {
                    // conversationManager.TargetUpdateConversation(ConversationManager.target);
                    
                }
            }
            else
            {
                Debug.Log("No Active Choice Found.");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Only nurse or agressor can be found.");
        }
    }

    public void Joystick(InputAction.CallbackContext context)
    {
    }
}
