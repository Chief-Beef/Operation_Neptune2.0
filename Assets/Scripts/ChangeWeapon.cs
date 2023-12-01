using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{

    public static ChangeWeapon Instance;

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
    public Text[] levelUpTxt = new Text[3];

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

    public GameObject upgradesMenu;
    public int[] weaponLvl = new int[3]; 

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

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

        //Set the chosen gun to active
        Cursor.SetCursor(cursors[activeGun], mouseOffset, CursorMode.ForceSoftware);
        Txt[activeGun].enabled = true;
        upgradesMenu.SetActive(false);

        PlayerShoot.Instance.damage = weaponDamage[0];
        PlayerShoot.Instance.turretRange = weaponRange[0];
        PlayerShoot.Instance.fireRate = weaponFireRate[0];
        PlayerShoot.Instance.shotTimer = 1 / weaponFireRate[0];

        weaponLvl[0] = 1;
        weaponLvl[1] = 1;
        weaponLvl[2] = 1;

        levelUpTxt[0].text = "Big Cannon Level: " + weaponLvl[0];
        levelUpTxt[1].text = "Small Cannon Level: " + weaponLvl[1];
        levelUpTxt[2].text = "Minigun Level: " + weaponLvl[2];


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
                Txt[activeGun].enabled = true;
                PlayerShoot.Instance.damage = weaponDamage[activeGun];
                PlayerShoot.Instance.turretRange = weaponRange[activeGun];
                PlayerShoot.Instance.fireRate = weaponFireRate[activeGun];
                PlayerShoot.Instance.shotTimer = 1 / weaponFireRate[activeGun];
            }
            else
            {
                //set all other guns to inactive
                Txt[i].enabled = false;
            }
        }

    }

    public void LevelUp()
    {
        levelUpTxt[0].text = "Big Cannon Level: " + weaponLvl[0];
        levelUpTxt[1].text = "Small Cannon Level: " + weaponLvl[1];
        levelUpTxt[2].text = "Minigun Level: " + weaponLvl[2];

        //pause game and open menu
        Time.timeScale = 0f;
        upgradesMenu.SetActive(true);

    }

    public void WeaponLevelUp(int i)
    {

        levelUpTxt[0].text = "Big Cannon Level: " + weaponLvl[0];
        levelUpTxt[1].text = "Small Cannon Level: " + weaponLvl[1];
        levelUpTxt[2].text = "Minigun Level: " + weaponLvl[2];

        weaponDamage[i] *= 1.1f;
        weaponRange[i] *= 1.1f;
        weaponFireRate[i] *= 1.1f;

        weaponLvl[i]++;
        
        PlayerShoot.Instance.damage = weaponDamage[activeGun];
        PlayerShoot.Instance.turretRange = weaponRange[activeGun];
        PlayerShoot.Instance.fireRate = weaponFireRate[activeGun];


        //start game again
        upgradesMenu.SetActive(false);
        Time.timeScale = 1f;


    }


}