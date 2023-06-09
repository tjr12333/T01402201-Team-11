using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    public int PlayerScore;
    public Text ScoreText;
    public Text ScoreTextScreen;
    public GameObject gameOverScreen;
    public GameObject ScoreTextBox;

    private float elapsedTime = 0.0f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        
    }

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        string timeString = string.Format("Time = {0:00}:{1:00}\n", minutes, seconds);
        ScoreTextScreen.text = timeString;
    }

    [ContextMenu("Incresase Score")]
    public void addScore(int scoreToAdd)
    {
        PlayerScore = PlayerScore + scoreToAdd;
        ScoreText.text = PlayerScore.ToString();
    }
    
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        ScoreTextBox.SetActive(false);
        UpdateTimeText();
        ScoreTextScreen.text += "Score = " + PlayerScore.ToString();
    }

    public void exit()
    {
        Application.Quit();
    }
}
