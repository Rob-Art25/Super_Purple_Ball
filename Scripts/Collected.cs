using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Collected : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]int LocalScore;
    public AudioSource clip;    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
        {
            clip.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            if(gameObject.transform.childCount > 1)
                gameObject.transform.GetChild(1).gameObject.SetActive(true);

            gameManager.puntaje += LocalScore;
            Destroy(gameObject, 0.5f);
        }        
    }   
}
