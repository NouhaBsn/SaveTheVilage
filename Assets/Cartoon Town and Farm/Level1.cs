
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Level1 : MonoBehaviour
{

    void Update()
    {
        // In standalone player we have to provide our own key
        // input for unlocking the cursor
        if (Input.GetKeyDown("space"))
            SceneManager.LoadScene(0);
    }

 }
