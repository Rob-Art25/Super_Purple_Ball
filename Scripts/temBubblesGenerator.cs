using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temBubblesGenerator : MonoBehaviour
{

    [SerializeField]
    GameObject[] bubbleItem;


    public float timeSpawn = 1, repeatSpawnRate = 3;
    
    [SerializeField]
    Transform xRangeLeft;
    [SerializeField]
    Transform xRangeRight;
    [SerializeField]
    Transform yRangeDown;
    [SerializeField]
    Transform yRangeUp;

    void Start()
    {
        InvokeRepeating("generateBubble", timeSpawn, repeatSpawnRate);
    }


    public void generateBubble()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        spawnPosition = new Vector3(Random.Range(xRangeLeft.position.x, xRangeRight.position.x), Random.Range(yRangeDown.position.y, yRangeUp.position.y), 0);
        GameObject bubble = Instantiate(bubbleItem[Random.Range(0, bubbleItem.Length)], spawnPosition, gameObject.transform.rotation);
    }
}
