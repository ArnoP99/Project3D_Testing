using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    public enum ConversationState
    {
        Started,
        Ended,
        ToBegin
    }

    public enum ConversationStartUser
    {
        Agressor,
        Nurse,
        Anyone
    }

    private List<ConversationElement> startElements = new List<ConversationElement>();
    private ConversationElement activeElement;
    private ConversationStartUser conversationStartUser;
    public ConversationState currentState;

    public Conversation(ConversationState m_beginState, List<ConversationElement> m_startElements, ConversationStartUser m_conversationStartUser)
    {
        currentState = m_beginState;
        startElements = m_startElements;
        conversationStartUser = m_conversationStartUser;
        activeElement = null;
    }

    private void SelectNextElement()
    {
        
    }
}
