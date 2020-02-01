using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    protected Joystick joystick;
    protected joystickButton joybutton;

    protected bool jump;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<joystickButton>();

    }

    // Update is called once per frame
    void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity =new  Vector3(joystick.Horizontal* -10f, rigidbody.velocity.y, joystick.Vertical * -10f);

        if (!jump && joybutton.pressed)
        {
            jump = true;
            rigidbody.velocity += Vector3.up * 25f;


        }

        if (jump && !joybutton.pressed)
        {
            jump = false;
        }
    }
}
