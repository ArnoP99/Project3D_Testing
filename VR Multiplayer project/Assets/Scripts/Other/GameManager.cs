using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private static readonly object padlock = new object();

    private GameManager()
    {
    }

    public static GameManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
    }

    // Check if there are 2 different players in the game (Nurse & Agressor) and if they are both present, start a new conversation.
    public static void CheckForTwoPlayers(int button, GameObject player)
    {
        bool nursePlayer = false;
        bool agressorPlayer = false;

        GameObject nurse = new GameObject();
        GameObject agressor = new GameObject();

        if (button == 1)
        {
            nursePlayer = true;
            nurse = player;
        }
        else if (button == 2)
        {
            agressorPlayer = true;
            agressor = player;
        }
        else
        {
            Debug.Log("Invalid number received from button! Check if the correct numbers are passed from each button ...");
        }

        if (nursePlayer == true && agressorPlayer == true)
        {
            Debug.Log("Conversation Started.");
            if (nurse != null && agressor != null)
            {
                ConversationManager.StartConversations(nurse, agressor);
            }
        }
        else
        {
            Debug.Log("You need a nurse and an agressor to start a conversation.");
        }

        //if(nursePlayer == true)
        //{
        //    Debug.Log("Conversation Started.");
        //    ConversationManager.StartConversations();
        //}
    }
}
