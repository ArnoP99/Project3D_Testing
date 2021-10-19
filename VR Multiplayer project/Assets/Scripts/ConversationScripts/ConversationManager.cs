using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{

    private static ConversationManager instance = null;
    private static readonly object padlock = new object();

    private List<Conversation> allConversations;
    private Conversation activeConversation;
    private List<GameObject> conversationParticipants;
    private GameObject activeParticipant;

    private ConversationManager()
    {
        Conversation generalCheckUp = new Conversation();
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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void StartConversation()
    {
        //set conversation participants
        //set active participant
    }

    private void EndConversation(Conversation conversationToEnd)
    {

    }

    private void ChooseConversation(Conversation chosenConversation)
    {

    }

    private void UpdateConversation(Conversation conversationToUpdate)
    {
        if(conversationToUpdate.currentState == Conversation.ConversationState.Ended)
        {
           
        }
    }
}
