using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{

    private static ConversationManager instance = null;
    private static readonly object padlock = new object();

    private List<Conversation> allConversations;
    private Conversation activeConversation;

    ConversationManager()
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

    private void StartConversation(Conversation conversationToStart)
    {

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
