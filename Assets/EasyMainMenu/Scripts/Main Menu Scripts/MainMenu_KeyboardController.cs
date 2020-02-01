using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu_KeyboardController : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject currentSelectedGameobject;

	// Use this for initialization
	void Start () {

        currentSelectedGameobject = eventSystem.currentSelectedGameObject;

    }
	
	// Update is called once per frame
	void Update () {
		
        if(eventSystem.currentSelectedGameObject != currentSelectedGameobject)
        {

            if (eventSystem.currentSelectedGameObject == null)
                eventSystem.SetSelectedGameObject(currentSelectedGameobject);
            else
                currentSelectedGameobject = eventSystem.currentSelectedGameObject;
        }

    }

    public void SetNextSelectedGameobject(GameObject NextGameObject)
    {
        if(NextGameObject != null)
        {

            currentSelectedGameobject = NextGameObject;
            eventSystem.SetSelectedGameObject(currentSelectedGameobject);

        }
        
    }
}
