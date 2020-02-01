using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victory : MonoBehaviour
{
    int vic = Animator.StringToHash("win");
    private Animator anim;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - player.transform.position.z <= 0.01f && transform.position.x - player.transform.position.x <= 0.001f)
        {
            anim.SetTrigger(vic);
            Debug.Log("ssssssssssssssssssssssssssss");


        }

    }
}
