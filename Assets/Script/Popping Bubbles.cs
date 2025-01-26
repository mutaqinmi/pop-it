using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PoppingBubbles : MonoBehaviour
{
    public AudioClip popClip;
    private AudioSource audioSource;
    private ScoreBoard.ScoreBoard scoreBoard;

    private void Start()
    {
        audioSource = GameObject.Find("Character").GetComponent<AudioSource>();
        scoreBoard = GameObject.Find("Score Board").GetComponent<ScoreBoard.ScoreBoard>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(gameObject.tag == "ShineBubble")
            {
                scoreBoard.AddScore(Random.Range(50, 100));
            }
            else
            {
                scoreBoard.AddScore(Random.Range(5, 20));
            }

            audioSource.PlayOneShot(popClip);

            Destroy(gameObject);
        }
    }
}
