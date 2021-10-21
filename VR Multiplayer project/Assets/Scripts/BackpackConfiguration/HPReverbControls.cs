using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HPReverbControls : MonoBehaviour
{
    GameObject textPopUp;
    GameObject activeChoice;

    public void Start()
    {
        textPopUp = GameObject.FindGameObjectWithTag("ChoicePopUp");
        
    }
    public void PressTrigger(InputAction.CallbackContext context)
    {
        if (textPopUp.transform.GetChild(0).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(0).gameObject;
            textPopUp.SetActive(false);
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(1).gameObject;
            textPopUp.SetActive(false);
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TextMeshPro>().color == Color.red)
        {
            activeChoice = textPopUp.transform.GetChild(2).gameObject;
            textPopUp.SetActive(false);
        }
        else
        {
            Debug.Log("No Active Choice Found.");
        }

        Debug.Log(activeChoice);
    }

    public void Joystick(InputAction.CallbackContext context)
    {
        Debug.Log("Joystick");
    }
}
