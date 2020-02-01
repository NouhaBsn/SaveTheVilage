using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    GameObject enemy;
    public int startingHealth = 100;
    public int currentHealth;
    private AudioSource screamdead;
    private int nb =0;

    Animator anim;

    int deadHash = Animator.StringToHash("dead");



    public void Start()
    {
        enemy = GameObject.FindWithTag("alien");
        anim = GetComponent<Animator>();
        screamdead = GetComponent<AudioSource>();

        currentHealth = startingHealth;


    }



    public void TakeDamage(int amount, Vector3 hitPoint)
    {

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
            //Destroy(enemy);
        }
    }


    void Death()
    {
        //capsuleCollider.isTrigger = true;
        screamdead.Play();
        anim.SetTrigger(deadHash);
        nb += 1;
        //enemy.gameObject.SetActive(false);

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(10f);
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("dead"))
        {
            if (nb >= 17)
            {
                SceneManager.LoadScene(2);

            }
            wait();
            
            Destroy(gameObject);


        }

        
    }
}
