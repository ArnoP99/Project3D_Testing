using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : NetworkBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nurse")
        {
            if (this == isServer)
            {
                GameManager.Instance.ChangeScene(1);
            }
        }
        else if (other.tag == "Agressor")
        {
            if (this == isServer)
            {

                GameManager.Instance.ChangeScene(2);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Nurse")
        {
            if (this == isServer)
            {
                GameManager.Instance.ChangeScene(1);
            }
        }
        else if (other.tag == "Agressor")
        {
            if (this == isServer)
            {

                GameManager.Instance.ChangeScene(2);
            }
        }
    }
}
