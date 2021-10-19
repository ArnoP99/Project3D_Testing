using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextNurse(string A, string B, string C, GameObject gameObjectplayer)
    {
        if (gameObjectplayer.tag == "Nurse")
        {
            
                gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = A;
                gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = B;    
                gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text = C;
            
        }

    }


    public void SetTextAgressor(string A, string B, string C, GameObject gameObjectplayer)
    {
        if (gameObjectplayer.tag == "Agressor")
        {

            gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = A;
            gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = B;
            gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text = C;
        }

    }
}
