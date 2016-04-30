﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    Button StartButton;
    Button ExitButton;
    Text TimerText;
    Text Title;

    GameManager gameManager;

    //Timer functions
    float Timer = 0;
    bool TimerActive = false;

    void Awake()
    {
        TimerText = this.transform.Find("Timer").GetComponent<Text>();
        TimerText.gameObject.SetActive(false);
        StartButton = this.transform.Find("Start").GetComponent<Button>();
        ExitButton = this.transform.Find("Exit").GetComponent<Button>();
        Title = this.transform.Find("Title").GetComponent<Text>();
        gameManager = GameManager.Instance();
        if (!gameManager) Debug.LogError("cannot find gamemanager!");

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
        Title.gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        StartButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        Title.gameObject.SetActive(true);
    }


    public void ExitPress()
    {
        //   Application.Quit();
        TurnOff();
        Debug.Log("Exit Pressed");

    }

    public void HitPlay()
    {
        TurnOff();
        OnTimer();
        gameManager.BeginGame();

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
        TimerText.gameObject.SetActive(false);
        Timer = 0f;
    }

}
