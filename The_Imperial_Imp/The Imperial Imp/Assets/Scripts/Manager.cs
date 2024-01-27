using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private TextMeshProUGUI[] texts;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI gameEndText;
    private int score;
    private float secondsRemaining;
    private int intSecondsRemaining;
    private int minutesText;
    private float secondsText;
    private bool hasWon;
    private bool gameHasEnded;
    public string currentSceneName;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        texts = (TextMeshProUGUI[])FindObjectsOfType(typeof(TextMeshProUGUI));
        for (int i = 0; i < texts.Length; i++) 
        {
            if (texts[i].name.Equals("Score Text")) 
            {
                scoreText = texts[i];
            }
            if (texts[i].name.Equals("Time Text")) 
            {
                timerText = texts[i];
            }
            if (texts[i].name.Equals("Game End Text"))
            {
                gameEndText = texts[i];
            }
        }
        secondsRemaining = 300.0f;
        hasWon = false;
        gameHasEnded = false;
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= 1000) 
        {
            hasWon = true;
        }
        secondsRemaining -= Time.deltaTime;
        scoreText.text = "Funny Points: " + score;
        intSecondsRemaining = (int)secondsRemaining;
        minutesText = intSecondsRemaining / 60;
        secondsText = secondsRemaining - (minutesText * 60.0f);
        if (secondsText >= 10.0f && !gameHasEnded)
        {
            timerText.text = "Time Remaining: " + minutesText + ":" + secondsText;
        }
        else if (!gameHasEnded && secondsText >= 0.0f)
        {
            timerText.text = "Time Remaining: " + minutesText + ":0" + secondsText;
        }
        else 
        {
            timerText.text = "Time Remaining: 0:00.000";
        }
        if (secondsRemaining <= 0.0f) 
        {
            gameHasEnded = true;
            if (hasWon)
            {
                gameEndText.text = "Congratulations! You made the king laugh!\nYour Final Score is:" + score + "\nWant to try for a better score?\nHit 'R' to Restart!";
            }
            else 
            {
                gameEndText.text = "Game Over!\nYou didn't make the king laugh.\nTry again? Hit 'R' to Restart!";
            }
        }
        if (gameHasEnded && Input.GetKeyDown(KeyCode.R)) 
        {
            scoreText.text = "";
            timerText.text = "";
            gameEndText.text = "";
            SceneManager.LoadScene(currentSceneName);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    public void increaseScoreBy(int scoreIncrease)
    {
        if (!gameHasEnded) 
        {
            score += scoreIncrease;
        }
    }
}
