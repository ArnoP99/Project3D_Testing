using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TriggerEvents : MonoBehaviour
{
    GameObject activeChoice;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.red;
            activeChoice = gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.white;
            activeChoice = null;
        }
    }

    public GameObject GetActiveChoice()
    {
        try
        {
            return activeChoice;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return null;
        }
    }
}
