using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EIU;

public class SimpleCube : MonoBehaviour {
    
    public float moveSpeed = 5;
    public float jumpLimit = 5;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //using translate method perform movements
        if (EasyInputUtility.instance)
        {
            this.transform.Translate(moveSpeed * EasyInputUtility.instance.GetAxis("Horizontal") * Time.deltaTime, 0,
                moveSpeed * EasyInputUtility.instance.GetAxis("Vertical") * Time.deltaTime);
        }
        else
        {   //standard input
            this.transform.Translate(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0,
                moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        }
       
    }
}
