using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    //PlayerStats
    public float regen;
    public float health;
    public float maxHealth;
    public float regenTimer;
    public float fill;
    public float healthPct;

    public Image HealthImage;
    

    private void Start()
    {
        Instance = this;
    }


    void Update()
    {
        regenTimer -= Time.deltaTime;

        if (regenTimer <= 0f)
        {
            health += regen;

            if (health > maxHealth)
            {
                health = maxHealth;
                regenTimer = 5f;

            }

        }

        fill = (health / maxHealth);

        HealthImage.fillAmount = fill;


        healthPct = (float)health / (float)maxHealth;

        //change blood overlay to have opacity cooresponding to the remainging health
        //bloodEffect.color = new Color(1f, 1f, 1f, 1f - healthPct);

    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        regenTimer = 5f;

        if (health <= 0)
        {
            //death mechanic go here
            health = 0;

        }
    }

}
