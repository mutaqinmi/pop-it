using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ScoreBoard
{
    public class ScoreBoard : MonoBehaviour
    {
        [Header("Show Score Board")]
        public TextMeshProUGUI scoreBoard;

        public int score = 0;
        private int highScore;

        private void Awake()
        {
            highScore = LoadHighScore();
        }

        private void Update()
        {
            scoreBoard.text = score.ToString();
        }

        private int LoadHighScore()
        {
            Score highScore = (Score)MMSaveLoadManager.Load(typeof(Score), "userHighScore");

            if(highScore == null)
            {
                return 0;
            }

            return highScore.highScore;
        }

        public void AddScore(int playerScore)
        {
            score += playerScore;
        }
    }

    [System.Serializable]
    public class Score 
    {
        public int score = 0;
        public int highScore;

        public Score(int score, int highScore)
        {
            this.score = score;
            this.highScore = highScore;
        }
    }
}
