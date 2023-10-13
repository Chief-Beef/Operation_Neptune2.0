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
    public float regenReset;
    public float fill;
    public float healthPct;

    public Image HealthImage;

    //Game Over Stuff
    public GameObject platform, turret, healthBar, spawners;
    public GameObject gameOverScreen;
    public BoxCollider2D playerHitBox;

    //iFrames
    public float respawnImmunity, hitImmunity;
    public bool immune;

    private void Start()
    {
        immune = false;
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
        if (!immune)
        {
            health -= dmg;
            regenTimer = 5f;
            StartCoroutine(ImmunityReset(hitImmunity));     //iFrames on hit

            if (health <= 0)
            {
                //death mechanic go here
                health = 0;
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        //Game Over Stuff
        platform.SetActive(false);
        turret.SetActive(false);
        healthBar.SetActive(false);
        spawners.SetActive(false);
        //playerHitBox.enabled = false;

        PlayerMovement playerScript = this.gameObject.GetComponent<PlayerMovement>();
        playerScript.enabled = false;

        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);

        StartCoroutine(ImmunityReset(respawnImmunity));        //iFrames on respawn

    }


    IEnumerator ImmunityReset(float timer)
    {
        immune = true;
        yield return new WaitForSeconds(timer);
        immune = false;
    }
}
