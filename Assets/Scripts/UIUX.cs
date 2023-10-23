using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIUX : MonoBehaviour
{

    public static UIUX Instance;

    public KeyCode button;

    public Text arenaTimerTxt;

    public float arenaTime;
    private int minutes, seconds;

    public float totalXP, newLevelXP, level, levelXP, totalLevelXP, fill;
    public Image XPBar;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        arenaTime = 0f;
        totalXP = 0f;
        level = 0f;
        levelXP = 0f;
        totalLevelXP = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        arenaTime += Time.deltaTime;

        //arenaTime     250
        //displayTime   2500
        //              250 + "." + 0


        minutes = (int)arenaTime / 60;
        seconds = (int)arenaTime % 60;

        //arenaTimerTxt.text = Mathf.Round(arenaTime) + "." + (displayTime % 10);

        if (minutes < 10 && seconds < 10)
            arenaTimerTxt.text = "0" + minutes + ":0" + seconds;
        else if (minutes < 10)
            arenaTimerTxt.text = "0" + minutes + ":" + seconds;
        else if (seconds < 10)
            arenaTimerTxt.text = minutes + ":0" + seconds;
        else if (minutes > 10)
            WinLose.Instance.WinCondition();
        else
            arenaTimerTxt.text = minutes + ":" + seconds;

    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void AddXP(float xp)
    {
        totalXP += xp;
        levelXP = totalXP - totalLevelXP;

        if(levelXP >= newLevelXP)
        {
            level++;
            totalLevelXP += newLevelXP;
            newLevelXP = 10f + level * 3f;
        }

        fill = levelXP / newLevelXP;
        XPBar.fillAmount = fill;
    }


}
