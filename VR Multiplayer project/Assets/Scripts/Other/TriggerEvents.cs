using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TriggerEvents : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.red;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.white;
        }
    }
}
