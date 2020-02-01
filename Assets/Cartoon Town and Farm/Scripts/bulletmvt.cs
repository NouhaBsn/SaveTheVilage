using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletmvt : MonoBehaviour
{

    public float forwardForce;
    Rigidbody rb;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //player = GameObject.FindWithTag("MainCamera");
        //Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y , player.transform.position.z);
        // Aim bullet in player's direction.
        //transform.rotation = Quaternion.LookRotation(playerPos);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += transform.forward * forwardForce * Time.deltaTime;
    }
}
