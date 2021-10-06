using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using UnityEngine.XR.Management;

public class HPReverbControls : MonoBehaviour
{

    public void PressTrigger(InputAction.CallbackContext context)
    {
        Debug.Log("Trigger");
        if(GameObject.FindGameObjectWithTag("ChoicePopUp").active == true)
        {
            GameObject.FindGameObjectWithTag("ChoicePopUp").active = false;
        }
        else
        {
            GameObject.FindGameObjectWithTag("ChoicePopUp").active = true;
        }

    }

    public void Joystick(InputAction.CallbackContext context)
    {
        Debug.Log("Joystick");
    }
}


