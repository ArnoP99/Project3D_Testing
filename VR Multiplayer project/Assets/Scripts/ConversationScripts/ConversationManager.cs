using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationManager : NetworkBehaviour
{

    private static ConversationManager instance = null;
    private static readonly object padlock = new object();

    private List<Conversation> allConversations;
    private Conversation activeConversation;
    private static List<GameObject> conversationParticipants = new List<GameObject>();
    private static GameObject activeParticipant;

    private static Conversation generalCheckUp;
    private static Conversation timeForMedication;
    private static Conversation helpButton;

    private ConversationManager()
    {

    }

    public static ConversationManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ConversationManager();
                }
                return instance;
            }
        }
    }

    // Initialize different Conversations that will be used in the game
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (this.GetComponent<NetworkIdentity>().isServer == true)
        {
            generalCheckUp = new Conversation();
            timeForMedication = new Conversation();
            helpButton = new Conversation();

            generalCheckUp.StartElement = ConversationElementInitializer.GeneralCheckupConversation();
            generalCheckUp.ActiveElement = generalCheckUp.StartElement;

            timeForMedication.StartElement = ConversationElementInitializer.TimeForMedicationConversation();
            timeForMedication.ActiveElement = timeForMedication.StartElement;

            helpButton.StartElement = ConversationElementInitializer.HelpButtonConversation();
            helpButton.ActiveElement = helpButton.StartElement;
        }
    }

    public void StartConversation(GameObject nurse)
    {
        Debug.Log("Before cmd: " + nurse);

        if (this.GetComponent<NetworkIdentity>().isClient == true)
        {
            CmdStartConversation(nurse);
        }
        Debug.Log("After cmd: " + nurse);
    }

    private void EndConversation(Conversation conversationToEnd)
    {

    }

    private void ChooseConversation(Conversation chosenConversation)
    {

    }

    private void UpdateConversation(Conversation conversationToUpdate)
    {
        if (conversationToUpdate.CurrentState == Conversation.ConversationState.Ended)
        {

        }
    }

    [Command (requiresAuthority = false)]
    public void CmdStartConversation(GameObject nurse)
    {
        Debug.Log("During cmd: " + nurse);
        TargetStartConversation(nurse.GetComponent<NetworkIdentity>().connectionToClient, nurse);
        Debug.Log("After TargetRpc: " + nurse);
    }

    [TargetRpc]
    public void TargetStartConversation(NetworkConnection target, GameObject nurse)
    {
        Debug.Log("During TargetRpc: " + nurse);
        conversationParticipants.Add(nurse);
        activeParticipant = nurse;

        nurse.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
        nurse.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshPro>().text = generalCheckUp.StartElement.Text;
        nurse.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro>().text = timeForMedication.StartElement.Text;
        nurse.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro>().text = helpButton.StartElement.Text;
    }


}
