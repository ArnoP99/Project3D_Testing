using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using UnityEngine.XR.Management;

public class HPReverbControls : MonoBehaviour
{
    GameObject textPopUp = GameObject.FindGameObjectWithTag("ChoicePopUp");

    public void Start()
    {
        textPopUp.SetActive(false);
    }
    public void PressTrigger(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger");
        if (textPopUp.activeInHierarchy == false)
        {
            textPopUp.SetActive(true);
        }        
        else
        {
            textPopUp.SetActive(false);
        }
    }

    public void Joystick(InputAction.CallbackContext context)
    {
        Debug.Log("Joystick");
    }
}


