using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBehaviour : MonoBehaviour
{
   
    public GameManager code;
    public GameObject deathPanel;
    public GameObject pausePanel;
    public GameObject highScorePanel;
    public void SetActiveDeathMenu(bool t)//set the buttons on or off
    {
        deathPanel.SetActive(t);
        if (t==true)
        {
            code.inDeathScreen = true;
            Time.timeScale = 0;
        }
        
        
    }
    public void SetActiveHighScoreMenu(bool t)
    {
        highScorePanel.SetActive(t);
        if (t == true)
        {
            code.inDeathScreen = true;
            Time.timeScale = 0;
        }
    }
    public void SetOptionScreen(bool t)
    {
        code.inoptionsscreen = t;
    }
    public void SetActivePauseMenu(bool t)
    {
        pausePanel.SetActive(t);
       
    }
    public void RetryLevel()//Retries level retry btn
    {
        code.Lives = 3;
        code.Score = 0;
        code.multiballs = 0;
        code.LoadLevel();
        code.prePreText = "";
        code.UpdateScore(false);
        SetActiveDeathMenu(false);
        code.inDeathScreen = false;
        code.UpdateLives();
        code.Init();
        code.ResetTimeScale();
    }
    public void RestartGame()//to restart the game restart button
    {
        code.CurrentLevel = 0;
        code.Score = 0;
        code.inDeathScreen = false;
        code.prePreText = "";
        code.UpdateScore(false);
        SetActiveDeathMenu(false);
        code.multiballs = 0;
        code.Lives = 3;
        code.UpdateLives();
        code.Init();
        code.LoadLevel();
        code.ResetTimeScale();
       
    }
   public void ResumeGame()
    {
        SetActivePauseMenu(false);
        Time.timeScale = code.timeScaler;
        code.paused = false;
    }
   
    public void DoQuit()//quits game
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;//recover gamemanager
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
