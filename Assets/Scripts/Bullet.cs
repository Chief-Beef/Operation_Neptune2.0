using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed, range, muzzleVelocity, flightTime, timeElapsed;
    public bool shoot, hit;
    public GameObject turret;
    public Vector3 shootDirection;
    public Vector3 shootPosition, startPosition;

    public SpriteRenderer bullet;
    public TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        range = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot && flightTime <= range)
        {
            bullet.enabled = true;
            trail.enabled = true;

            speed = muzzleVelocity;
            flightTime += Time.deltaTime;
            this.transform.position = (this.transform.position + shootDirection.normalized * speed);
        }
        else if (hit && timeElapsed <1f && Vector3.Distance(this.transform.position, shootPosition) > .5f)
        {

            bullet.enabled = true;
            trail.enabled = true;

            speed = 5f;
            this.transform.position = new Vector3(Mathf.Lerp(startPosition.x, shootPosition.x, timeElapsed * speed), Mathf.Lerp(startPosition.y, shootPosition.y, timeElapsed * speed), 0f);
            timeElapsed += Time.deltaTime;
;
        }
        else
        {
            InvisibleBullet();
        }
    }


    public void Shoot()
    {
        InvisibleBullet();
        
        shoot = true;
        shootDirection = turret.transform.up;
    }

    public void HitTarget(Vector3 target)
    {
        InvisibleBullet();

        hit = true;

        startPosition = turret.transform.position;
        shootPosition = target;
    }

    public void InvisibleBullet()
    {
        shoot = false;
        bullet.enabled = false;
        trail.enabled = false;
        trail.Clear();

        speed = 0f;
        flightTime = 0f;
        timeElapsed = 0f;
        hit = false;

        //TP bullet to turret
        this.transform.position = turret.transform.position;

    }

}
