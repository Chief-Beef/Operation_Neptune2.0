using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //Aim Stuff
    public Camera cam;
    public Vector3 mousePos;
    public LayerMask enemyLayer;

    //Shoot
    public float triggerPull;   //input
    public float turretRange;   //range of weapon
    public float damage;        //damage per shot of active weapon
    public float fireRate;      //shots per second
    public float knockback;     //per shot knockback
    public RaycastHit2D hit;    //raycast

    public float shotTimer;     //delay between shots    
    public GameObject bulletPatricle;   //bullet Particle Effect
    private Quaternion bulletRotation;  //bullet Particle Rotation X -90

    // Start is called before the first frame update
    void Start()
    {
        
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
            bulletRotation = new Quaternion(Quaternion.identity.x, Quaternion.identity.y, transform.rotation.z, Quaternion.identity.w);
            Instantiate(bulletPatricle, this.transform);
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

            HealthScript target = hit.transform.GetComponent<HealthScript>();
            Rigidbody2D targetRB = hit.transform.GetComponent<Rigidbody2D>();
            if (target != null)
            {
                targetRB.AddForce(this.transform.up * knockback, ForceMode2D.Impulse);
                target.hitMarker(damage);
            }
        }
    }

}
