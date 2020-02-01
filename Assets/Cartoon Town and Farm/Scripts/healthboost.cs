using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthboost : MonoBehaviour
{


    private GameObject player;
    private Slider healthSlider;                                 // Reference to the UI's health bar.
    private PlayerCollision h;
    private int amount = 50;
    private GameObject medkit;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        healthSlider = FindObjectOfType<Slider>();

        h = player.GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("hhhh");
            if (h.currentHealth < h.startingHealth)
            {
                if (h.currentHealth <= h.startingHealth - amount)
                {
                    h.currentHealth += amount;

                }
                else
                {
                    h.currentHealth = 100;
                }
                healthSlider.value = h.currentHealth;
                Destroy(gameObject);

            }

        }
    }

    
}
