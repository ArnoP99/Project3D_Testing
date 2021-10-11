using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void NextScene()
    {
        SceneManager.LoadScene("TestScene");
        Scene level2 = SceneManager.GetSceneByName("TestScene");

    }

    public void GiveTag()
    {
        GameObject player = GameObject.FindGameObjectWithTag("VRplayer");
        player.tag = "ItWorks";

    }
}
