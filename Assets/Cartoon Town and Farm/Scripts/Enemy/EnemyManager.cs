using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

   /* public GameObject monster;
    public GameObject spawner;
    private GameObject go;
    private GameObject player;
    public Animator anim;
    private Rigidbody rb;
    //int attackHash = Animator.StringToHash("Attack");


    public float spawnTime = 0.1f;            // How long between each spawn.
    //public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public void Start()
    {


        player = GameObject.FindWithTag("MainCamera");
        //generator();
        InvokeRepeating("Spawn", spawnTime, spawnTime);

    }

    // Update is called once per frame
    public void Update()

    {
        //anim = go.GetComponent<Animator>();
        //rb = go.GetComponent<Rigidbody>();



    }

    void Spawn()
    {
        // If the player has no health left...
        /*if (playerHealth.currentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }*/

        // Find a random index between zero and one less than the number of spawn points.
        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Vector3 pos;
        /*pos.x = spawner.transform.position.x;
        pos.y = spawner.transform.position.y + 0.03F;
        pos.z = spawner.transform.position.z - 1;

        pos.x = spawner.transform.position.x;
        pos.y = spawner.transform.position.y + 1f;
        pos.z = spawner.transform.position.z - 1;
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        go = Instantiate(monster, pos , monster.transform.rotation);


    }*/
}

