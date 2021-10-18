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
            nurseChoice = 1;
        }
        if (gameObject.tag == "OptionB")
        {
            Debug.Log("TriggerEnter B");
            nurseChoice = 2;

        }
        if (gameObject.tag == "OptionC")
        {
            Debug.Log("TriggerEnter C");
            nurseChoice = 3;

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


}
