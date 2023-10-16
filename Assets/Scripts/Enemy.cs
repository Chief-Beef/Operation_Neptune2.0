using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Move Variables
    public Transform playerPos;     //Player Position
    public Vector2 direction;      //move direction
    public float speed;             //move speed

    public Rigidbody2D rb;          //enemy rigidbody

    //Attack Values
    public float attackRange;
    public float attackDamage;
    public bool canAttack;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        canMove = true;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {   //Enemy Movement
            direction = (playerPos.position - this.transform.position).normalized;   //normalize to adjust for diagonal speed
            rb.velocity = (direction * speed);                                       //multiply normal direction vector by speed magnitude
        }
        else
        {
            rb.velocity = (direction * 0f);
        }

        if (Vector3.Distance(this.transform.position, playerPos.position) <= attackRange && canAttack)
        {
            Debug.Log("Attack");

            Attack();
        }

    }

    public void Attack()
    {
        PlayerHealth.Instance.TakeDamage(attackDamage);
        canAttack = false;
        canMove = false;
        StartCoroutine(AttackDelay());
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.4f);
        canAttack = true;
        canMove = true;
        
    }

}
