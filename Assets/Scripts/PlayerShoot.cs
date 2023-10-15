using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public static PlayerShoot Instance;

    //Aim Stuff
    public Camera cam;
    public Vector3 mousePos;
    public LayerMask enemyLayer;

    //Shoot
    public float triggerPull;   //input
    public float turretRange;   //range of weapon
    public float fireRate;      //shots per second
    public float damage;        //damage per shot of active weapon
    public float knockback;     //per shot knockback
    public RaycastHit2D hit;    //raycast

    //public ParticleSystem gunShot;  //gunshot particle effect

    public Bullet[] bullets = new Bullet[8];
    public int totalShots = 0;

    public float shotTimer;     //delay between shots    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer -= Time.deltaTime;

        //currently set to auto shoot
        //triggerPull = Input.GetAxis("Fire1");


        // Mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // * dynamicSensitivity);

        if(shotTimer <= 0)
        {
            shootWeapon();
        }

    }


    public void shootWeapon()
    {
        
        //reset shotTimer
        shotTimer = 1 / fireRate;

        //determine shootRay
        hit = Physics2D.Raycast(this.transform.position, this.transform.up, turretRange, enemyLayer);    

        Debug.DrawRay(this.transform.position, this.transform.up * turretRange, Color.blue, 1f);


        if (hit.collider != null)
        {
            //shooty stuff here
            Debug.Log(hit.transform.name);

            //Do Damage
            HealthScript target = hit.transform.GetComponent<HealthScript>();
            Rigidbody2D targetRB = hit.transform.GetComponent<Rigidbody2D>();

            if (target != null)
            {
                targetRB.AddForce(this.transform.up * PlayerShoot.Instance.knockback, ForceMode2D.Impulse);
                target.hitMarker(PlayerShoot.Instance.damage);
            }


            bullets[totalShots % 4].HitTarget(target.transform.position);

        }
        else
        {
            bullets[totalShots % 4].Shoot();
        }

        //gunShot.Play();

        totalShots++;
    }

}
