using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    Button StartButton;
    Button ExitButton;
    Text TimerText;
    Text Title;
    Text Score;
    Text GameOverText;
    Transform healthMeter;

    public List<Image> healthImages;

    GameManager gameManager;

    //Timer functions
    float Timer = 0;
    bool TimerActive = false;

    void Awake()
    {
        healthImages = new List<Image>();
        Score = this.transform.Find("Score").GetComponent<Text>();
        Score.gameObject.SetActive(false);

        healthMeter = this.transform.Find("HealthMeter").GetComponent<Transform>();
        healthMeter.gameObject.SetActive(false);

        TimerText = this.transform.Find("Timer").GetComponent<Text>();
        TimerText.gameObject.SetActive(false);

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
        Title.gameObject.SetActive(false);
        Score.gameObject.SetActive(false);
        healthMeter.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
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

    public void HitPlay()
    {
        TurnOff();
        OnTimer();
        Score.gameObject.SetActive(true);
        healthMeter.gameObject.SetActive(true);
        gameManager.BeginGame(this);

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
            healthImages[h].gameObject.SetActive(true);
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
        TimerText.gameObject.SetActive(false);
        Timer = 0f;
    }

    public void GameOver()
    {
        TurnOff();
        GameOverText.text = "Your Score: " + Score.text;
        GameOverText.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
    }
}
