using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private EnemyHealth h;
    private GameObject player;
    private float movementTime = 20;
    private Animator anim;
    private Rigidbody rb;
    int getagun = Animator.StringToHash("getagun");

    int attackHash = Animator.StringToHash("shoot");
    //int getagunHash = Animator.StringToHash("getagun");

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");
        anim = GetComponent<Animator>();
        h = GetComponent<EnemyHealth>();

    }

    // Update is called once per frame
    void Update()
    {

        if (h.currentHealth > 0)
        {
            if (Mathf.Abs(player.transform.position.z - transform.position.z) <=   10f && Mathf.Abs(player.transform.position.x - transform.position.x) <=   10f)
            {
                anim.SetTrigger(getagun);
                anim.SetTrigger(attackHash);



                Vector3 newPos = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1.4f), 0.5F / movementTime);
                transform.LookAt(player.transform);
                transform.position = newPos;
                Debug.Log("look at me");


            }
        }

        
        


   
    }
}
