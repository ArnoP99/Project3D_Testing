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
    private List<GameObject> conversationParticipants = new List<GameObject>();
    private GameObject activeParticipant;

    private Conversation generalCheckUpCv;
    private Conversation timeForMedicationCv;
    private Conversation helpButtonCv;

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

        generalCheckUpCv = new Conversation();
        timeForMedicationCv = new Conversation();
        helpButtonCv = new Conversation();
        activeConversation = new Conversation();

        generalCheckUpCv.StartElement = ConversationElementInitializer.GeneralCheckupConversation();
        generalCheckUpCv.ActiveElement = generalCheckUpCv.StartElement;

        timeForMedicationCv.StartElement = ConversationElementInitializer.TimeForMedicationConversation();
        timeForMedicationCv.ActiveElement = timeForMedicationCv.StartElement;

        helpButtonCv.StartElement = ConversationElementInitializer.HelpButtonConversation();
        helpButtonCv.ActiveElement = helpButtonCv.StartElement;

        activeConversation = generalCheckUpCv;
        Debug.Log("local active cv: " + activeConversation.StartElement.Text);

    }

    public void StartConversation(GameObject nurse)
    {
        conversationParticipants.Add(nurse);
        activeParticipant = nurse;

        nurse.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
        nurse.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = ConversationElementInitializer.GeneralCheckupConversation().Text;
        nurse.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = ConversationElementInitializer.TimeForMedicationConversation().Text;
        nurse.transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = ConversationElementInitializer.HelpButtonConversation().Text;
    }

    private void EndConversation(Conversation conversationToEnd)
    {

    }

    private void UpdateConversation(Conversation conversationToUpdate)
    {
        if (conversationToUpdate.CurrentState == Conversation.ConversationState.Ended)
        {

        }
    }

    public void Update()
    {
        //Debug.Log(activeConversation.StartElement);
        Debug.Log(activeConversation.StartElement.Text);
        Debug.Log(generalCheckUpCv.StartElement.Text);
        Debug.Log(timeForMedicationCv.StartElement.Text);
        Debug.Log(helpButtonCv.StartElement.Text);
    }
    public void SetConversation(int choice)
    {
        if (activeConversation == null)
        {
            if (choice == 1)
            {
                activeConversation = generalCheckUpCv;

            }
            else if (choice == 2)
            {
                activeConversation = timeForMedicationCv;

            }
            else if (choice == 3)
            {
                activeConversation = helpButtonCv;

            }
            Debug.Log(choice);
        }
    }



    //[Command(requiresAuthority = false)]
    //public void CmdStartConversation(GameObject nurse)
    //{
    //    Debug.Log("During cmd: " + nurse);
    //    TargetStartConversation(nurse.GetComponent<NetworkIdentity>().connectionToServer, nurse);
    //    Debug.Log("After TargetRpc: " + nurse);
    //}

    //[TargetRpc]
    //public void TargetStartConversation(NetworkConnection target, GameObject nurse)
    //{
    //    Debug.Log("During TargetRpc: " + nurse);

    //}

    //pass choice to server with an int and then from server to agressor

    public Conversation ActiveConversation
    {
        get
        {
            return activeConversation;
        }
        set
        {
            activeConversation = value;
        }
    }

    public Conversation GeneralCheckupConversation
    {
        get
        {
            return generalCheckUpCv;
        }
    }

    public Conversation TimeForMedicationConversation
    {
        get
        {
            return timeForMedicationCv;
        }
    }

    public Conversation HelpButtonConversation
    {
        get
        {
            return helpButtonCv;
        }
    }

}
