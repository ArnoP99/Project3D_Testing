using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using UnityEngine.XR.Management;

public class selectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;


    private Transform selection;


    private float distance = 2.5f;


    public void Update()
    {
        if (selection != null)
        {
            var selectionRenderer = selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            selection = null;
        }
        try
        {
            GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
            foreach (var item in cameras)
            {
                Camera tempCam = item.GetComponent<Camera>();
                var ray = tempCam.ScreenPointToRay(GameObject.FindGameObjectWithTag("LeftController").transform.position);


                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.distance < distance)
                {
                    var selection = hit.transform;
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        selectionRenderer.material = highlightMaterial;
                    }
                    this.selection = selection;
                }
            }
        }

        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

    }
}
