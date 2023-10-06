using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LintScript : MonoBehaviour
{
    //Move Variables
    public Transform playerPos;     //Player Position
    public Vector2 direction;      //move direction
    public float speed;             //move speed

    public Rigidbody2D rb;          //enemy rigidbody

    //Attack Values
    public float attackRange;
    public float attackDamage;
    public bool canAttack, canMove, shooting, shot, inRange;
    public float distanceFromPlayer;
    public float timer;

    //shooting variables
    public GameObject gun;
    public LayerMask playerLayer;

    public bool coroutineReset = false;
    public float waitTime = 5f;

    //Laser Variables
    public GameObject laser;
    private BoxCollider2D laserCol;
    private SpriteRenderer laserSpr;

    // Start is called before the first frame update
    void Start()
    {

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        laserCol = laser.GetComponent<BoxCollider2D>();
        laserSpr = laser.GetComponent<SpriteRenderer>();

        canAttack = false;
        canMove = true;
        shooting = false;
        shot = false;
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerPos != null)
        {
            distanceFromPlayer = Vector2.Distance(playerPos.position, transform.position);
            var dif = playerPos.position - gun.transform.position;
            var rayHit = Physics2D.Raycast(gun.transform.position, dif, Mathf.Infinity, playerLayer);

            
            if (canMove && !shooting && !canAttack)
            {
                //Lint Movement
                direction = (playerPos.position - this.transform.position).normalized;   //normalize to adjust for diagonal speed
                rb.velocity = (direction * speed);                                       //multiply normal direction vector by speed magnitude

                //if player is in attack range
                if (distanceFromPlayer <= attackRange && rayHit.collider.tag == "Player")
                {
                    canAttack = true;
                    inRange = true;
                    Debug.Log("CanAttack = true");
                }
                else if ((shooting || shot) && distanceFromPlayer > attackRange)
                {
                    ResetLaser();
                }

            }
            else if(canMove && inRange)
            {
                if (rayHit.collider != null)
                {
                    if (coroutineReset && !shot && rayHit.collider.gameObject.tag == "Player" && distanceFromPlayer < attackRange)
                    {
                        coroutineReset = false;
                        shooting = true;
                        timer = 1.5f;
                        StartCoroutine(ChargeUp());                           
                        //GetComponent<AudioPlay>().PlayWithPitch(0.8f);        //add this in later
                    }
                }
                if (distanceFromPlayer > attackRange)
                {
                    inRange = false;
                    ResetLaser();
                }

                if (shooting && !shot)
                {

                    if (timer > -0.28f)
                    {
                        if (rayHit.collider != null)
                        {
                            if (rayHit.collider.tag == "Player")
                            {
                                Vector3 diff = playerPos.transform.position - gun.transform.position;
                                Vector3 rotatedDiff = Quaternion.Euler(0, 0, 90) * diff;
                                Quaternion targetAngle = Quaternion.LookRotation(Vector3.forward, rotatedDiff);
                                gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, targetAngle, 150 * Time.deltaTime);
                            }
                        }

                        timer -= Time.deltaTime;
                        laserSpr.color = new Color(1 - timer, 0, 0);
                    }

                    laser.transform.localScale = new Vector3(1, laser.transform.localScale.y + (Time.deltaTime * 0.125f), 1);
                }
                else if (shot)
                {
                    // Laser shot and cooldown
                    if (timer > 0)
                    {
                        if (timer < 0.75f)
                            laserCol.enabled = false;
                        else
                            laserCol.enabled = true;
                        laser.transform.localScale = new Vector3(1, timer, 1);
                        laserSpr.color = new Color(timer, timer / 4, timer / 4);
                        timer -= Time.deltaTime;
                    }
                    // Ends laser shooting
                    else
                    {
                        shot = false;
                        shooting = false;
                        laser.transform.localScale = Vector3.zero;
                    }
                }
                else
                {

                    Vector3 diff = playerPos.transform.position - gun.transform.position;
                    Vector3 rotatedDiff = Quaternion.Euler(0, 0, 90) * diff;
                    Quaternion targetAngle = Quaternion.LookRotation(Vector3.forward, rotatedDiff);

                    gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, targetAngle, 150 * Time.deltaTime);
                }
            }

        }
    }
    
    // Resets laser variables when player exits range
    private void ResetLaser()
    {
        StopAllCoroutines();
        shot = false;
        shooting = false;
        canAttack = false;
        laser.transform.localScale = Vector3.zero;
        laserCol.enabled = false;
        coroutineReset = true;
        //canMove = true;

        //GetComponent<AudioPlay>().asource.Stop();
    }

    IEnumerator ChargeUp()
    {
        if (playerPos != null)
        {
            yield return new WaitForSeconds(2);
            shot = true;
            timer = 1f;
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(waitTime);

        if (playerPos != null)
        {
            coroutineReset = true;
        }
        else
        {
            laser.transform.localScale = Vector3.zero;
        }
    }
}
