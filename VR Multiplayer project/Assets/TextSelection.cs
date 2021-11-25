using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSelection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "ChoicePopUp")
        {
            other.gameObject.GetComponent<TextMeshPro>().color = Color.red;

        }
    }
    private void OntriggerStay(Collider other)
    {
        other.gameObject.tag = "ActiveChoice";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "ChoicePopUp")
        {
            other.gameObject.GetComponent<TextMeshPro>().color = Color.white;

        }
    }
}
