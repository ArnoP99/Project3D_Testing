using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{

    [SerializeField] Material defaultMaterial;
    [SerializeField] Material highlightMaterial;

    [SerializeField] GameObject gameObject;



    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            Debug.Log("Tag: " + collision.gameObject.tag);
            gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        Debug.Log("TriggerEnter");
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "LeftController" || collision.gameObject.tag == "RightController")
        {
            gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        Debug.Log("TriggerExit");
    }
}
