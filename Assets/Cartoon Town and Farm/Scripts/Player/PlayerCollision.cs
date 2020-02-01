using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    //public GameObject GameOverUI;
    private GameObject enemyshooting;

    public int startingHealth = 100;
    public int currentHealth;
    private AudioSource screamdead;
    private AudioSource hurt;
    private int Damageamount = 5;
    public Slider healthSlider;                                 // Reference to the UI's health bar.

    public float flashSpeed = 50f;                               // The speed the damageImage will fade at.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    bool damaged;

    private void Start()
    {
        hurt = GetComponent<AudioSource>();
        currentHealth = startingHealth;
        enemyshooting = GameObject.FindWithTag("alien");
        damaged = false;
    }

    public void Update()
    {


        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear,0.005f);
        }


        damaged = false;
        if (currentHealth <= 0)
        {
            Debug.Log("You lost!");
            Destroy(enemyshooting);
            SceneManager.LoadScene(1);
        }
    }


    void OnCollisionEnter(Collision collisionInfo)
    {



        if (collisionInfo.collider.tag == "laserball" )
        {


            if (currentHealth > 0)
            {
                damaged = true;
                currentHealth -= Damageamount;
                // Set the health bar's value to the current health.
                healthSlider.value = currentHealth;

                Destroy(collisionInfo.collider.gameObject);
                //Debug.Log("We hit something");

                //so that when we hit the obstacle the player stops 
                //movement.enabled = false; // ou bien getComponent<PlayerMovement>();
                //FindObjectOfType<GameManager>().EndGame();
                hurt.Play();

                Debug.Log("damaged");
            }

        }

    }
}
