using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boundstrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.FindGameObjectWithTag("PlayerHUD").GetComponentInChildren<Image>().color = new Color(0, 0, 0, 255);
        Debug.Log("Trigger");
    }

}
