using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform bubblesParent;

    [Header("Bubbles")]
    public GameObject bubble;
    public GameObject shiningBubble;
    public GameObject bomb;

    [Header("Game")]
    public BoxCollider arenaCollider;
    private bool isSpawning;

    private IEnumerator BubbleRandomSpawn()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));

            Vector2 randomPosition = GetRandomPositionInArena();

            Instantiate(bubble, randomPosition, Quaternion.identity, bubblesParent);
        }
    }

    private IEnumerator ShiningBubbleRandomSpawn()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));

            Vector2 randomPosition = GetRandomPositionInArena();

            Instantiate(shiningBubble, randomPosition, Quaternion.identity, bubblesParent);
        }
    }

    private IEnumerator BombRandomSpawn()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));

            Vector2 randomPosition = GetRandomPositionInArena();

            Instantiate(bomb, randomPosition, Quaternion.identity, bubblesParent);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;

            for (int i = 0; i <= 10; i++)
            {
                Vector2 randomPosition = GetRandomPositionInArena();

                Instantiate(bubble, randomPosition, Quaternion.identity, bubblesParent);
            }

            StartCoroutine(BubbleRandomSpawn());
            StartCoroutine(ShiningBubbleRandomSpawn());
            StartCoroutine(BombRandomSpawn());
        }
    }

    private Vector2 GetRandomPositionInArena()
    {
        if (arenaCollider == null) return Vector2.zero;

        Vector2 arenaCenter = arenaCollider.transform.position;
        Vector2 arenaSize = arenaCollider.size;

        float randomX = Random.Range(arenaCenter.x - arenaSize.x / 2, arenaCenter.x + arenaSize.x / 2);
        float randomY = Random.Range(arenaCenter.y - arenaSize.y / 2, arenaCenter.y + arenaSize.y / 2);

        return new Vector2(randomX, randomY);
    }
}
