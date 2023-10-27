using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    //Movement Inputs
    public float xMove, yMove;  //movement inputs
    public float speed;         //multiplies direction by this magnitude
    private float lastVelocity; //velocity on previous frame

    public Vector2 direction;
    public Vector2 mousePos;
    public Vector3 turretPos; 
    public Vector2 turretLook;

    //GameObjects
    public GameObject player, platform, turret;
    public GameObject crosshair;

    //Player RigidBody
    public Rigidbody2D rb;

    //Camera
    public Camera cam;
    public float camDefaultSize;
    public float camSizeMod, camTrailMod;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Camera controls 
        var vel = rb.velocity.magnitude;
        
        //this line will change the camera size if added back in
        //cam.orthographicSize = camDefaultSize + (Mathf.Lerp(vel, lastVelocity, 0.05f) / camSizeMod);

        // Sets camera position relative to player
        cam.transform.position = Vector3.Lerp(cam.transform.position + Vector3.back, new Vector3(this.transform.position.x + rb.velocity.x / camTrailMod, this.transform.position.y + rb.velocity.y / camTrailMod, -1), 0.15f);

        lastVelocity = vel;


        //Player Move Inputs
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");

        //Player Movement
        direction = new Vector2(xMove, yMove).normalized;   //normalize to adjust for diagonal speed
        rb.velocity = (direction * speed);                  //multiply normal direction vector by speed magnitude


        //Mouse Inputs
        //Vector2 dynamicSensitivity = new Vector2(Screen.width / 1920, Screen.height / 1080);

        // Mouse position
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // * dynamicSensitivity);

        turretPos = turret.transform.position;
        turretLook = (mousePos - (Vector2)turretPos).normalized;
        turret.transform.up = turretLook;

        

    }
}
