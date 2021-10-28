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

    public enum ConversationActiveUser
    {
        Agressor,
        Nurse,
        Anyone
    }

    private ConversationElement startElement;
    private ConversationElement activeElement;
    private ConversationActiveUser activeUser;
    private ConversationState currentState;

    public Conversation()
    {
        currentState = ConversationState.ToBegin;
        startElement = new ConversationElement();
        activeUser = ConversationActiveUser.Nurse;
        activeElement = null;

    }
    

    public List<ConversationElement> SelectNextElement(ConversationElement activeElement)
    {
        return activeElement.ReactionElements;
    }


    public ConversationElement StartElement
    {
        get
        {
            return startElement;
        }
        set
        {
            startElement = value;
        }
    }

    public ConversationElement ActiveElement
    {
        get
        {
            return activeElement;
        }
        set
        {
            activeElement = value;
        }
    }

    public ConversationActiveUser ActiveUser
    {
        get
        {
            return activeUser;
        }
        set
        {
            activeUser = value;
        }
    }

    public ConversationState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }
}
