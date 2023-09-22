using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{

    //active weapons
    public int totalGuns;
    public int activeGun;


    public KeyCode switchGuns;

    //active weapon sprite
    public SpriteRenderer turret;
    public Sprite[] guns;
    public Sprite bigCannon, smallCannon, miniGun;
    
    //ammo count
    public Text[] Txt;
    public Text bigText, smallText, miniText;

    //active weapon crosshair
    public Texture2D bigCross;
    public Texture2D smallCross;
    public Texture2D miniCross;

    public Texture2D[] cursors = new Texture2D[3];

    public Vector2 mouseOffset = new Vector2(12.5f, 12.5f);

    //weapon stats
    public float bigDamage, smallDamage, miniDamage;
    public float bigRange, smallRange, miniRange;
    public float bigFire, smallFire, miniFire;


    public float[] weaponDamage = new float[3];
    public float[] weaponRange = new float[3];
    public float[] weaponFireRate = new float[3];

    // Start is called before the first frame update
    void Start()
    {
        //assign variable values
        totalGuns = 3;
        activeGun = 0;

        guns = new Sprite[totalGuns];
        Txt = new Text[totalGuns];

        //assign sprites
        guns[0] = bigCannon;
        guns[1] = smallCannon;
        guns[2] = miniGun;

        turret.sprite = guns[activeGun];

        //assign text
        Txt[0] = bigText;
        Txt[1] = smallText;
        Txt[2] = miniText;

        Txt[0].enabled = true;
        Txt[1].enabled = false;
        Txt[2].enabled = false;

        //order can be changed who cares
        cursors[0] = bigCross;
        cursors[1] = smallCross;
        cursors[2] = miniCross;

        Cursor.SetCursor(cursors[activeGun], mouseOffset, CursorMode.ForceSoftware);

        //set weapon stats
        weaponDamage[0] = bigDamage;
        weaponDamage[1] = smallDamage;
        weaponDamage[2] = miniDamage;

        weaponRange[0] = bigRange;
        weaponRange[1] = smallRange;
        weaponRange[2] = miniRange;

        weaponFireRate[0] = bigFire;
        weaponFireRate[1] = smallFire;
        weaponFireRate[2] = miniFire;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchGuns))
        {
            switchWeapon();
        }
    }



    public void switchWeapon()
    {

        //Switch Guns to the next gun
        //Switch Crosshairs to match the gun
        //Deactivate other guns and crosshairs

        Debug.Log("Switch Guns");


        //Changes ActiveGun to the next weapon and makes sure to stay inside array
        activeGun = (activeGun + 1) % totalGuns;
        turret.sprite = guns[activeGun];

        for (int i = 0; i < totalGuns; i++)
        {
            if (i == activeGun)
            {
                //Set the chosen gun to active
                Cursor.SetCursor(cursors[activeGun], mouseOffset, CursorMode.ForceSoftware);
                Txt[i].enabled = true;
            }
            else
            {
                //set all other guns to inactive
                Cursor.SetCursor(cursors[activeGun], mouseOffset, CursorMode.ForceSoftware);
                Txt[i].enabled = false;
            }
        }

    }

}