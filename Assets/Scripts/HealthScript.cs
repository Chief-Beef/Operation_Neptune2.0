using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float health;

    public float maxHealth;

    public Rigidbody2D rb;
    public GameObject enemy;
    public GameObject deathExplosion;

    public float killXP;

    //BossHealthBar
    public float fill;
    public float healthPct;

    public Image HealthImage;



    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void hitMarker(float damage)
    {

        health -= damage;


        //NoisyBoi.Instance.hitMarker();

        if (health <= 0)
        {
            //Die
            //ScoreBoard.Instance.playerScore += 50;
            Instantiate(deathExplosion, this.transform.position, Quaternion.identity);

            //EnemyScript obj = enemy.GetComponent<EnemyScript>();
            //obj.deathAnimation();

            //this.GetComponent<HealthScript>().enabled = false;
            UIUX.Instance.AddXP(killXP);
            Destroy(gameObject);
        }

        if (HealthImage != null)
        {
            fill = (health / maxHealth);
            HealthImage.fillAmount = fill;
            healthPct = (float)health / (float)maxHealth;
        }
    }
}
