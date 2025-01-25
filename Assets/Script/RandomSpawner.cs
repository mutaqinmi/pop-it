using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform bubblesParent;
    public GameObject bubble;
    public BoxCollider arenaCollider;
    private bool isSpawning;

    private IEnumerator BubbleRandomSpawn()
    {
        while (isSpawning)
        {
            Vector2 randomPosition = GetRandomPositionInArena();

            Instantiate(bubble, randomPosition, Quaternion.identity, bubblesParent);

            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
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
