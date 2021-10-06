using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using UnityEngine.XR.Management;

public class HPReverbControls : MonoBehaviour
{
    bool textPopUp = false;
    public void PressTrigger(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger");
        if (textPopUp == false)
        {
            GameObject.FindGameObjectWithTag("ChoicePopUp").SetActive(true);
            textPopUp = true;
        }        
        else
        {
            GameObject.FindGameObjectWithTag("ChoicePopUp").SetActive(false);
            textPopUp = false;
        }

    }

    public void Joystick(InputAction.CallbackContext context)
    {
        Debug.Log("Joystick");
    }
}


