﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    Button StartButton;
    Button ExitButton;
    Button CreditsButton;
    Button BackButton;
    Button ResetButton;
    Button GoBackModeButton;
    Button GoBackSurvivalButton;
    Button SurvivalButton;
    Button EasyButton;

    Button MediumButton;
    Button HardButton;
    Button CustomizeButton;
    Button StoryButton;

    Text TimerText;
    Text Title;
    Text Score;
    Text GameOverText;
    Text CreditsText;
    Transform healthMeter;

    public List<Image> healthImages;

    GameManager gameManager;

    //Timer functions
    float Timer = 0;
    bool TimerActive = false;

    void Awake()
    {
        CreditsButton = this.transform.Find("CreditsButton").GetComponent<Button>();
        ResetButton = this.transform.Find("ResetButton").GetComponent<Button>();
        ResetButton.gameObject.SetActive(false);

        BackButton = this.transform.Find("BackButton").GetComponent<Button>();
        BackButton.gameObject.SetActive(false);

        CreditsText = this.transform.Find("CreditsText").GetComponent<Text>();
        CreditsText.gameObject.SetActive(false);

        healthImages = new List<Image>();
        Score = this.transform.Find("Score").GetComponent<Text>();
        Score.gameObject.SetActive(false);

        healthMeter = this.transform.Find("HealthMeter").GetComponent<Transform>();
        healthMeter.gameObject.SetActive(false);

        TimerText = this.transform.Find("Timer").GetComponent<Text>();
        TimerText.gameObject.SetActive(false);


        GoBackModeButton = this.transform.Find("GoBackGameModes").GetComponent<Button>();
        GoBackModeButton.gameObject.SetActive(false);

        SurvivalButton = this.transform.Find("Survival").GetComponent<Button>();
         SurvivalButton.gameObject.SetActive(false);

         GoBackSurvivalButton = this.transform.Find("GoBackSurvival").GetComponent<Button>();
         GoBackSurvivalButton.gameObject.SetActive(false);

         EasyButton = this.transform.Find("Easy").GetComponent<Button>();
         EasyButton.gameObject.SetActive(false);

         MediumButton = this.transform.Find("Medium").GetComponent<Button>();
         MediumButton.gameObject.SetActive(false);

         HardButton = this.transform.Find("Hard").GetComponent<Button>();
         HardButton.gameObject.SetActive(false);

         CustomizeButton = this.transform.Find("Customize").GetComponent<Button>();
         CustomizeButton.gameObject.SetActive(false);

         StoryButton = this.transform.Find("Story").GetComponent<Button>();
         StoryButton.gameObject.SetActive(false);


        StartButton = this.transform.Find("Start").GetComponent<Button>();
        ExitButton = this.transform.Find("Exit").GetComponent<Button>();
        Title = this.transform.Find("Title").GetComponent<Text>();
        GameOverText = this.transform.Find("GameOverText").GetComponent<Text>();
        gameManager = GameManager.Instance();
        if (!gameManager) Debug.LogError("cannot find gamemanager!");

        GameOverText.gameObject.SetActive(false);

        foreach (Image i in healthMeter.GetComponentsInChildren<Image>(true))
        {
            healthImages.Add(i);
            i.gameObject.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
	    
	}


    // Update is called once per frame
    void Update () {
	
	}

    //Have a timer to display how long you have survived
    //first in seconds format 0.00 and then minutes:seconds format 00:00
    void FixedUpdate()
    {
        float minutes = Mathf.Floor(Timer / 60);
        float seconds = Timer % 60;

        if (TimerActive == true)
        {
            Timer += Time.deltaTime;
            if (minutes >= 1f)
            {
                if (seconds > 9.49)
                    TimerText.text = minutes + ":" + Mathf.RoundToInt(seconds);
                else
                    TimerText.text = minutes + ":0" + Mathf.RoundToInt(seconds);

            }
            else
                TimerText.text = (seconds).ToString("F2");
        }
    }

    public void TurnOff()
    {
        StartButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        CreditsButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        CreditsText.gameObject.SetActive(false);
        ResetButton.gameObject.SetActive(false);

        Title.gameObject.SetActive(false);
        Score.gameObject.SetActive(false);
        healthMeter.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);


        EasyButton.gameObject.SetActive(false);
        MediumButton.gameObject.SetActive(false);
        HardButton.gameObject.SetActive(false);
        GoBackSurvivalButton.gameObject.SetActive(false);
        SurvivalButton.gameObject.SetActive(false);
        CustomizeButton.gameObject.SetActive(false);
        StoryButton.gameObject.SetActive(false);
        GoBackModeButton.gameObject.SetActive(false);
    }

    public void ResetMenu()
    {
        Score.text = "0";
        TimerText.text = "0";

        StartButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        CreditsButton.gameObject.SetActive(true);
        Title.gameObject.SetActive(true);

        ResetButton.gameObject.SetActive(false);
        CreditsText.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(false);
        Score.gameObject.SetActive(false);
        healthMeter.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
        SurvivalButton.gameObject.SetActive(false);
        CustomizeButton.gameObject.SetActive(false);
        StoryButton.gameObject.SetActive(false);
        GoBackModeButton.gameObject.SetActive(false);
        ResetTimer();
        OffTimer();
    }

    public void ShowCredits()
    {
        TurnOff();
        CreditsText.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(true);
        Title.gameObject.SetActive(true);
    }

    public void TurnOn()
    {
        StartButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        Title.gameObject.SetActive(true);
        Score.gameObject.SetActive(true);
        healthMeter.gameObject.SetActive(true);
    }
    
    public void ExitPress()
    {
        TurnOff();
        Application.Quit();
        Debug.Log("Exit Pressed");

    }

    public void HitPlay(int difficulty)
    {
        TurnOff();
        OnTimer();
        gameManager.SetDifficulty(difficulty);
        Score.gameObject.SetActive(true);
        healthMeter.gameObject.SetActive(true);
        gameManager.BeginGame(this);

    }

    public void GameOptionsOn()
    {
        SurvivalButton.gameObject.SetActive(true);
        CustomizeButton.gameObject.SetActive(true);
        StoryButton.gameObject.SetActive(true);
        GoBackModeButton.gameObject.SetActive(true);
    }

    public void GameOptionsOff()
    {
        SurvivalButton.gameObject.SetActive(false);
        CustomizeButton.gameObject.SetActive(false);
        StoryButton.gameObject.SetActive(false);
        GoBackModeButton.gameObject.SetActive(false);
    }

    public void GameSurvivalDifficultyOn()
    {
        EasyButton.gameObject.SetActive(true);
        MediumButton.gameObject.SetActive(true);
        HardButton.gameObject.SetActive(true);
        GoBackSurvivalButton.gameObject.SetActive(true);

    }

    public void GameSurvivalDifficultyOff()
    {
        EasyButton.gameObject.SetActive(false);
        MediumButton.gameObject.SetActive(false);
        HardButton.gameObject.SetActive(false);
        GoBackSurvivalButton.gameObject.SetActive(false);
    }

    public void HitStart()
    {
        TurnOff();
        GameOptionsOn();
    }

    // disables a health image according to the current health value
    public void DecHealthMeter(int h, bool b)
    {
        healthImages[h].gameObject.SetActive(!b);
    }

    // sets the health meter to health value
    public void SetHealthMeter(int h)
    {
        if (h <= 0 || h > healthImages.Count)
        {
            Debug.LogError("health is negative!");
            return;
        }
        for (int i = 0; i < h; i++)
        {
            healthImages[i].gameObject.SetActive(true);
        }
    }
    public void UpdateScore(int s)
    {
        Score.text = s.ToString();
    }
    //turns on timer
    public void OnTimer()
    {
        TimerText.gameObject.SetActive(true);
        TimerActive = true;
    }
    //turns off timer
    public void OffTimer()
    {
        TimerActive = false; 
    }

    //reset timer to 0
    public void ResetTimer()
    {
        TimerActive = false;
        TimerText.text = "0";
        TimerText.gameObject.SetActive(false);
        Timer = 0f;
    }

    public void GameOver(float killScore)
    {
        TurnOff();
        OffTimer();
        GameOverText.text = "Hits: " + Score.text + "    Time: " + TimerText.text + "\nTotal Score: " + (killScore + Timer/5f).ToString("f2");
        GameOverText.gameObject.SetActive(true);
        ResetButton.gameObject.SetActive(true);
    }
}
