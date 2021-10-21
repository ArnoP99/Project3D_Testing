using System.Collections;
using System.Collections.Generic;
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
        if (textPopUp.transform.GetChild(0).GetComponent<TriggerEvents>().GetActiveChoice() != null)
        {
            activeChoice = textPopUp.transform.GetChild(0).GetComponent<TriggerEvents>().GetActiveChoice();
            textPopUp.SetActive(false);
        }
        else if (textPopUp.transform.GetChild(1).GetComponent<TriggerEvents>().GetActiveChoice() != null)
        {
            activeChoice = textPopUp.transform.GetChild(1).GetComponent<TriggerEvents>().GetActiveChoice();
            textPopUp.SetActive(false);
        }
        else if (textPopUp.transform.GetChild(2).GetComponent<TriggerEvents>().GetActiveChoice() != null)
        {
            activeChoice = textPopUp.transform.GetChild(2).GetComponent<TriggerEvents>().GetActiveChoice();
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
