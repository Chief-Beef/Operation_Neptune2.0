using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamgeNumbers : MonoBehaviour
{
    public static DamgeNumbers Instance;


    public Rigidbody2D playerRB, parentRB;

    public Text[] damageNumbers = new Text[30];
    public float vertOffset;


    public int i;


    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        for (int x = 0; x < 30; x++)
            damageNumbers[x].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Instance = this;

        //have parent move at same speed in opposite direction of the player
        parentRB.velocity = -(playerRB.velocity);

    }


    public void DisplayNumber(Transform location, float damage)
    {
        //move number to just over the target
        damageNumbers[i].transform.position = new Vector3(location.position.x, location.position.y + vertOffset, location.position.z);

        damageNumbers[i].text = "" + (int)damage;        //set damage text to be the damage value

        damageNumbers[i].enabled = true;            //make damage number visible


        StartCoroutine(HideNumbers(i));
        i = (i + 1) % 30;
    }

    IEnumerator HideNumbers(int index)
    {
        yield return new WaitForSeconds(1f);
        damageNumbers[index].enabled = false;

    }

}
