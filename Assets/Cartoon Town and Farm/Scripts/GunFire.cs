
using UnityEngine;

public class GunFire : MonoBehaviour

{

    private AudioSource gunsound;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        gunsound = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gunsound.Play();
            anim.Play("GunShot");
        }
    }


}