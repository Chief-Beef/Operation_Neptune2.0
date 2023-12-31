using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{

    public static WinLose Instance;

    public GameObject loseScreen;

    public KeyCode winTestButton;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        loseScreen.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void WinCondition()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void Respawn()
    { 
        loseScreen.SetActive(false);

        //Game Over Stuff
        PlayerHealth.Instance.platform.SetActive(true);
        PlayerHealth.Instance.turret.SetActive(true);
        PlayerHealth.Instance.healthBar.SetActive(true);
        PlayerHealth.Instance.spawners.SetActive(true);
        //playerHitBox.enabled = false;

        PlayerMovement playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerScript.enabled = true;

        //player respawns with half health, will have to give them some immunity so they do not insta die
        PlayerHealth.Instance.health = PlayerHealth.Instance.maxHealth / 2f;

        Time.timeScale = 1f;

        EnemySpawn.Instance.canSpawn = true;

        PlayerHealth.Instance.gameMusic.Play();

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Test Scene");
        Time.timeScale = 1f;
    }

}
