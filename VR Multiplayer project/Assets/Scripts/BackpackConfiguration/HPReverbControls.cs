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
    }

    public void Joystick(InputAction.CallbackContext context)
    {
        Debug.Log("Joystick");
    }
}


