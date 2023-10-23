using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{

    public static PauseScript Instance;

    public GameObject pauseMenu;    //UI Canvas
    public GameObject playerUI;     //Player UI/HUD           

    public KeyCode pauseKey;        //esc

    public bool pauseState;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //start level with time = 1
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            playerUI.SetActive(false);
            pauseState = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            playerUI.SetActive(true);
            pauseState = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}