using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeWalls : MonoBehaviour
{

    [SerializeField]
    GameObject walls;

    void Start()
    {
        walls.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!walls.activeSelf)
            walls.SetActive(true);
        }
    }
}
