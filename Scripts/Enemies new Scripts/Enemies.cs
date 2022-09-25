using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int damage;
    
    
    private void OnCollisionEnter2D(Collision2D collision) // Quita salud al jugador al detectar una colisi�n f�sica con �l
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<JoyStickMove>().salud -= damage;
            collision.gameObject.GetComponent<JoyStickMove>().animator.Play("Hit");
            collision.gameObject.GetComponent<JoyStickMove>().hitClip.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Quita salud al jugador al detectar una colisi�n atravesable con �l
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<JoyStickMove>().salud -= damage;
            collision.gameObject.GetComponent<JoyStickMove>().animator.Play("Hit");
            collision.gameObject.GetComponent<JoyStickMove>().hitClip.Play();
        }
    }

}
