using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float totalTime = 30;
    public TextMeshProUGUI countdownText;

    private RandomSpawner spawner;
    private ScoreBoard.ScoreBoard scoreBoard;
    private bool gameStart;

    private void Start()
    {
        spawner = GameObject.Find("Bubbles").GetComponent<RandomSpawner>();
        scoreBoard = GameObject.Find("Score Board").GetComponent<ScoreBoard.ScoreBoard>();
    }

    private void Update()
    {
        if (gameStart)
        {
            if(totalTime > 0)
            {
                totalTime -= Time.deltaTime;

                float minutes = Mathf.FloorToInt(totalTime / 60);
                float seconds = Mathf.FloorToInt(totalTime % 60);

                countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                countdownText.text = "Time's up!";
                GameOver();
            }
        }
    }

    public void GameStart()
    {
        gameStart = true;
        spawner.StartSpawning();

        Transform uiCamera = GameObject.Find("UI Camera").transform;
        Transform hudCanvas = uiCamera.Find("HUD Canvas");
        Transform mainHUD = hudCanvas.Find("Main HUD");
        Transform mainMenu = hudCanvas.Find("Main Menu");

        mainHUD.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        spawner.StopSpawning();
        totalTime = 0;

        LevelManager.Instance.FreezeCharacters();
        SetHighScore();
    }

    private void SetHighScore()
    {
        if(scoreBoard.score > scoreBoard.highScore)
        {
            ScoreBoard.Score newScore = new ScoreBoard.Score(scoreBoard.score, scoreBoard.score);
            MMSaveLoadManager.Save(newScore, "userHighScore");
        }
    }
}
