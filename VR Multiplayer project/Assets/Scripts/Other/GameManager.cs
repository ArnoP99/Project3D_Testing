using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private static GameManager instance = null;
    private static readonly object padlock = new object();

    private bool nurseOnSpawn = false;
    private bool agressorOnSpawn = false;

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
    public static void CheckForTwoPlayers(int button)
    {
        bool nursePlayer = false;
        bool agressorPlayer = false;

        if (button == 1)
        {
            nursePlayer = true;
        }
        else if (button == 2)
        {
            agressorPlayer = true;
        }
        else
        {
            Debug.Log("Invalid number received from button! Check if the correct numbers are passed from each button ...");
        }

        /*if(nursePlayer == true && agressorPlayer == true)
        {
            Debug.Log("Conversation Started.");
            ConversationManager.StartConversation(nurse, agressor);
        }*/

        if (nursePlayer == true)
        {
            Debug.Log("Conversation Started.");
            ConversationManager.StartConversation();
        }
    }

    public void ChangeScene(int onSpawnCheck)
    {
        if (onSpawnCheck == 1 && nurseOnSpawn == false)
        {
            nurseOnSpawn = true;
        }
        else if (onSpawnCheck == 1 && nurseOnSpawn == true)
        {
            nurseOnSpawn = false;
        }

        if (onSpawnCheck == 2 && agressorOnSpawn == false)
        {
            agressorOnSpawn = true;
        }
        else if (onSpawnCheck == 2 && agressorOnSpawn == true)
        {
            agressorOnSpawn = false;
        }

        if (nurseOnSpawn == true /*&& agressorOnSpawn == true*/ && this == isServer)
        {
            NetworkManager.singleton.ServerChangeScene("ZiekenhuisKamer");
        }
    }
}
