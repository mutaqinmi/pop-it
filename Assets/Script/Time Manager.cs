using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time Manager")]
    [HideInInspector] public float totalTime = 30;
    public TextMeshProUGUI countdownText;
    public GameObject gameOverOverlay;

    [Header("Game Over")]
    public TextMeshProUGUI totalBubble;
    public TextMeshProUGUI initialScore;
    public TextMeshProUGUI recentHighScore;

    private RandomSpawner spawner;
    private ScoreBoard.ScoreBoard scoreBoard;
    private bool gameStart;
    private float initialTimer = 3;

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
        Transform uiCamera = GameObject.Find("UI Camera").transform;
        Transform hudCanvas = uiCamera.Find("HUD Canvas");
        Transform mainHUD = hudCanvas.Find("Main HUD");
        Transform mainMenu = hudCanvas.Find("Main Menu");
        Transform countdownBase = mainMenu.Find("Countdown Base");
        Transform title = mainMenu.Find("Title");
        Transform subtitle = mainMenu.Find("Subtitle");

        title.gameObject.SetActive(false);
        subtitle.gameObject.SetActive(false);
        countdownBase.gameObject.SetActive(true);

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        Transform uiCamera = GameObject.Find("UI Camera").transform;
        Transform hudCanvas = uiCamera.Find("HUD Canvas");
        Transform mainHUD = hudCanvas.Find("Main HUD");
        Transform mainMenu = hudCanvas.Find("Main Menu");
        Transform countdownBase = mainMenu.Find("Countdown Base");
        Transform countdown = countdownBase.Find("Countdown");

        while (initialTimer > 0)
        {
            countdown.gameObject.GetComponent<TextMeshProUGUI>().text = initialTimer.ToString();

            yield return new WaitForSeconds(1);

            initialTimer -= 1;
        }

        countdown.gameObject.GetComponent<TextMeshProUGUI>().text = "Go!";

        yield return new WaitForSeconds(1);

        gameStart = true;
        spawner.StartSpawning();

        mainHUD.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        Transform uiCamera = GameObject.Find("UI Camera").transform;
        Transform hudCanvas = uiCamera.Find("HUD Canvas");
        Transform mainHUD = hudCanvas.Find("Main HUD");

        mainHUD.gameObject.SetActive(false);

        spawner.StopSpawning();
        totalTime = 0;

        LevelManager.Instance.FreezeCharacters();
        SetHighScore();

        Summary();
        gameOverOverlay.SetActive(true);
    }

    private void SetHighScore()
    {
        if(scoreBoard.score > scoreBoard.highScore)
        {
            ScoreBoard.Score newScore = new ScoreBoard.Score(scoreBoard.score, scoreBoard.score);
            MMSaveLoadManager.Save(newScore, "userHighScore");
        }
    }

    private void Summary()
    {
        totalBubble.text = scoreBoard.bubblePopped.ToString();
        initialScore.text = scoreBoard.score.ToString();
        recentHighScore.text = scoreBoard.highScore.ToString();
    }
}
