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
    private static Conversation activeConversation;
    private static List<GameObject> conversationParticipants = new List<GameObject>();
    private static GameObject activeParticipant;
    public static NetworkConnection target;

    private static List<ConversationElement> reactionsToActiveElement = new List<ConversationElement>();

    private static Conversation generalCheckUpConv;
    private static Conversation medicationTimeConv;
    private static Conversation alarmButtonConv;

    private static GameObject nurse;
    private static GameObject agressor;

    private static GameObject activeParticipantTextPopUp;

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
        if (isServer)
        {
            NetworkServer.Spawn(gameObject);
        }
        generalCheckUpConv = new Conversation();
        medicationTimeConv = new Conversation();
        alarmButtonConv = new Conversation();

        generalCheckUpConv.StartElement = ConversationElementInitializer.GeneralCheckupConversation();
        generalCheckUpConv.ActiveElement = generalCheckUpConv.StartElement;

        medicationTimeConv.StartElement = ConversationElementInitializer.MedicationConversation();
        medicationTimeConv.ActiveElement = medicationTimeConv.StartElement;

        alarmButtonConv.StartElement = ConversationElementInitializer.AlarmButtonConversation();
        alarmButtonConv.ActiveElement = alarmButtonConv.StartElement;
    }

    // Set starting elements of each conversation
    public static void StartConversations()
    {
        nurse = GameObject.FindGameObjectWithTag("Nurse").gameObject;
        agressor = GameObject.FindGameObjectWithTag("Agressor").gameObject;

        conversationParticipants.Add(nurse);
        conversationParticipants.Add(agressor);

        activeParticipant = nurse;
        target = agressor.GetComponent<NetworkIdentity>().connectionToClient;
        activeParticipantTextPopUp = activeParticipant.transform.GetChild(0).transform.GetChild(3).gameObject;

        activeParticipant.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshPro>().text = generalCheckUpConv.StartElement.Text;
        activeParticipant.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro>().text = medicationTimeConv.StartElement.Text;
        activeParticipant.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro>().text = alarmButtonConv.StartElement.Text;
    }

    private void EndConversation(Conversation conversationToEnd)
    {

    }

    public static void ChooseConversation(int choice)
    {
        switch (choice)
        {
            case 1:
                activeConversation = generalCheckUpConv;
                activeConversation.ActiveUser = Conversation.ConversationActiveUser.Nurse;
                break;

            case 2:
                activeConversation = medicationTimeConv;
                activeConversation.ActiveUser = Conversation.ConversationActiveUser.Nurse;
                break;

            case 3:
                activeConversation = alarmButtonConv;
                activeConversation.ActiveUser = Conversation.ConversationActiveUser.Nurse;
                break;

            default:
                Debug.Log("Invalid choice reveived.");
                break;
        }

    }

    [TargetRpc]
    public void TargetUpdateConversation(NetworkConnection target)
    {
        if (activeConversation.ActiveUser == Conversation.ConversationActiveUser.Nurse)
        {
            activeParticipantTextPopUp.SetActive(false);
            activeConversation.ActiveUser = Conversation.ConversationActiveUser.Agressor;
            activeParticipant = agressor;
            target = agressor.GetComponent<NetworkIdentity>().connectionToClient;
            activeParticipantTextPopUp = activeParticipant.transform.GetChild(0).transform.GetChild(3).gameObject;

        }
        else if (activeConversation.ActiveUser == Conversation.ConversationActiveUser.Agressor)
        {
            activeParticipantTextPopUp.SetActive(false);
            activeConversation.ActiveUser = Conversation.ConversationActiveUser.Nurse;
            activeParticipant = nurse;
            target = nurse.GetComponent<NetworkIdentity>().connectionToClient;
            activeParticipantTextPopUp = activeParticipant.transform.GetChild(0).transform.GetChild(3).gameObject;

        }

        reactionsToActiveElement = activeConversation.SelectNextElement(activeConversation.ActiveElement);

        if (activeConversation.CurrentState == Conversation.ConversationState.Ended)
        {
            Debug.Log("Conversation ended.");
        }


        if (reactionsToActiveElement.Count == 2)
        {
            //ChangeTextPopUpSize
            activeParticipant.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshPro>().text = reactionsToActiveElement[0].Text;
            activeParticipant.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro>().text = reactionsToActiveElement[1].Text;
            activeParticipant.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro>().text = null;
        }
        else
        {
            activeParticipant.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshPro>().text = reactionsToActiveElement[0].Text;
            activeParticipant.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro>().text = reactionsToActiveElement[1].Text;
            activeParticipant.gameObject.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro>().text = reactionsToActiveElement[2].Text;
        }
        activeParticipantTextPopUp.SetActive(true);
    }

    public GameObject ActiveParticipantTextPopUp
    {
        get
        {
            return activeParticipantTextPopUp;
        }
    }
}
