using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TriggerEvents : MonoBehaviour
{
    private int nurseChoice;
    private int agressorChoice;
    public GameObject gameobjectplayer;


    private void Start()
    {
    }

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            
            gameObject.GetComponent<TextMeshPro>().color = Color.red;
        }
        Debug.Log("Tag: " + collision.gameObject.tag);

        if(gameObject.tag == "OptionA") { 
            Debug.Log("TriggerEnter A");
        }
        if (gameObject.tag == "OptionB")
        {
            Debug.Log("TriggerEnter B");
        }
        if (gameObject.tag == "OptionC")
        {
            Debug.Log("TriggerEnter C");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.white;
        }
        Debug.Log("Tag: " + collision.gameObject.tag);
        Debug.Log("TriggerExit");
    }

   public void SetTextNurse(string A, string B, string C , GameObject gameObjectplayer)
    {
        if(gameObjectplayer.tag == "nurse")
        {
            if(gameObject.tag == "OptionA") { 
                gameObject.GetComponent<TextMeshPro>().text = A;
            }
            if (gameObject.tag == "OptionB")
            {
                gameObject.GetComponent<TextMeshPro>().text = B;
            }
            if (gameObject.tag == "OptionC")
            {
                gameObject.GetComponent<TextMeshPro>().text = C;
            }
        }

    }
}
