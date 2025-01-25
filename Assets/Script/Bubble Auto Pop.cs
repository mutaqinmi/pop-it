using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAutoPop : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(PopBubble());    
    }

    private IEnumerator PopBubble()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        Destroy(gameObject);
    }
}
