using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    Animator anim;
    GameObject player;
    private EnemyHealth h;

    public GameObject bullet;
    public Transform SpawnPoint;


    private float shotsPerSeconds = 4;



    void Awake()
    {

        h = GetComponent<EnemyHealth>();

        anim = GetComponent<Animator>();

        player = GameObject.FindWithTag("MainCamera");


    }

    // Update is called once per frame
    void Update()
    {

        if (h.currentHealth > 0)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("shot") && anim != null)
            {
                //InvokeRepeating("shoot", spawnTime, spawnTime);
                float probability = Time.deltaTime * shotsPerSeconds;
                if (Random.value < probability)
                {
                    shoot();
                }
            }

        }
    }

    void shoot()
    {
        Vector3 offset = player.transform.position - transform.position;
        Quaternion facingPlayerX = Quaternion.LookRotation(offset, Vector3.up);
        Instantiate(bullet, SpawnPoint.position, facingPlayerX);
        //bullet.transform.position += transform.forward * movementSpeed * Time.deltaTime;
        //rb.velocity = (player.transform.position - bullet.transform.position).normalized * constant;
    }
}
