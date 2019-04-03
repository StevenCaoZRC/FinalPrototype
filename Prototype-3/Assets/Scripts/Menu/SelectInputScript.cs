using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectInputScript : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool bButtonSelected;



    // Use this for initialization
    void Start()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
        bButtonSelected = true;

     
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !bButtonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            bButtonSelected = true;
        }


        if (Input.GetMouseButtonDown(0))
            eventSystem.SetSelectedGameObject(selectedObject);
    }

    private void OnDisable()
    {
    }

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
        bButtonSelected = true;
    }
}
