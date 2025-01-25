using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PoppingBubbles : MonoBehaviour
{
    private AudioSource audioSource;
    private ScoreBoard.ScoreBoard scoreBoard;

    private void Start()
    {
        audioSource = GameObject.Find("Character").GetComponent<AudioSource>();
        scoreBoard = GameObject.Find("Score Board").GetComponent<ScoreBoard.ScoreBoard>();

        if(scoreBoard == null)
        {
            Debug.Log(scoreBoard);
            Debug.Log("score board not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scoreBoard.AddScore(20);
            audioSource.PlayOneShot(audioSource.clip);

            Destroy(gameObject);
        }
    }
}
