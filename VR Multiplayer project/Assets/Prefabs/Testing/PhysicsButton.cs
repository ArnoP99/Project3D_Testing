using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    [SerializeField] public GameObject prefabAgressor;
    [SerializeField] public GameObject prefabNurse;

    private bool isPressed = false;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    private Vector3 vectorforobject;


    public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Pressed();
    }

    private void OnCollisionExit(Collision collision)
    {
        Released();
        if(gameObject.tag == "AgressorButton")
        {
            vectorforobject = collision.transform.position;
            Instantiate(prefabAgressor, vectorforobject, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        if (gameObject.tag == "NurseButton")
        {
            vectorforobject = collision.transform.position;
            Instantiate(prefabNurse, vectorforobject, Quaternion.identity);
            Destroy(collision.gameObject);
        }



    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if(Math.Abs(value) < deadZone)
        {
            value = 0;
        }
        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        Debug.Log("Pressed");
        onPressed.Invoke();

    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
