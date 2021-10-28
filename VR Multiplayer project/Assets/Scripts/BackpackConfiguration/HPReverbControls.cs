using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class HPReverbControls : NetworkBehaviour
{
    GameObject activeChoice;
    int choice = 0;

    ConversationManager conversationManager;


    private void Start()
    {
        NetworkServer.Spawn(gameObject);
    }

    public void PressTrigger(InputAction.CallbackContext context)
    {

        if (conversationManager.ActiveParticipantTextPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = conversationManager.ActiveParticipantTextPopUp.transform.GetChild(0).gameObject;
            if (choice == 0)
            {
                choice = 1;
                ConversationManager.ChooseConversation(choice);
            }
            if (isServer)
            {
                conversationManager.TargetUpdateConversation(ConversationManager.target);
            }
        }
        else if (conversationManager.ActiveParticipantTextPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = conversationManager.ActiveParticipantTextPopUp.transform.GetChild(1).gameObject;
            if (choice == 0)
            {
                choice = 2;
                ConversationManager.ChooseConversation(choice);
            }
            if (isServer)
            {
                conversationManager.TargetUpdateConversation(ConversationManager.target);
            }
        }
        else if (conversationManager.ActiveParticipantTextPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = conversationManager.ActiveParticipantTextPopUp.transform.GetChild(2).gameObject;
            if (choice == 0)
            {
                choice = 3;
                ConversationManager.ChooseConversation(choice);
            }
            if (isServer)
            {
                conversationManager.TargetUpdateConversation(ConversationManager.target);
            }
        }
        else
        {
            Debug.Log("No Active Choice Found.");
        }

        Debug.Log(activeChoice);
    }

    public void Joystick(InputAction.CallbackContext context)
    {
        Debug.Log("Joystick");
    }
}
