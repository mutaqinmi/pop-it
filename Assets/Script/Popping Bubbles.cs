using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PoppingBubbles : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.Find("Character").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioSource.clip);
            Destroy(gameObject);
        }
    }
}
