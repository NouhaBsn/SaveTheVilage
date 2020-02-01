using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : MonoBehaviour
{

    GameObject player;
    private Quaternion DoorOpen;
    private Quaternion DoorClosed;
    public float smooth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");


    }

    // Update is called once per frame
    void Update()
    {

        if (player.transform.position.z >= transform.position.z - 0.3f)
        {
            DoorOpen = transform.rotation = Quaternion.Euler(90, 0, 0);
            DoorClosed = transform.rotation;

            transform.rotation = Quaternion.Lerp(DoorClosed, DoorOpen, Time.deltaTime * smooth);
            Debug.Log("Door Opened");
        }
        
    }
}
