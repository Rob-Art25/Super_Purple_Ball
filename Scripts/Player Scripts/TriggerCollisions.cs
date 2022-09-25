using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisions : MonoBehaviour
{

    JoyStickMove mainScript;
        
    void Start()
    {
        mainScript = GetComponent<JoyStickMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // Detecta las colisiones con objetos atravesables
    {
        if (collision.CompareTag("puertaDeNivel"))
        {
            FindObjectOfType<GameHUD>().FinishText.gameObject.SetActive(true);
            collision.GetComponent<BoxCollider2D>().enabled = false;
            mainScript.gameManager.CambioDeNivel();
        }

        if (collision.CompareTag("CheckPoint"))
        {
            PlayerPrefs.SetFloat("CheckPointX", transform.position.x);
            PlayerPrefs.SetFloat("CheckPointY", transform.position.y);
            mainScript.gameManager.puntoDeControl = true;
        }

        if (collision.CompareTag("Enemigos"))
        {
            mainScript.hitClip.Play();

            mainScript.animator.Play("Hit");

            if (mainScript.salud <= 0)
            {
                mainScript.Die();
            }
            else
            {
                mainScript.salud--;
            }
        }
        if (collision.CompareTag("agua"))
        {
            mainScript.water = true;
        }

        if (collision.CompareTag("Life")) // Incrementa las vidas al conseguir una extra.
        {
            mainScript.gameManager.lifesManager();
        }

        if (collision.CompareTag("Warp"))
        {
            mainScript.warp = true;
            FindObjectOfType<GameHUD>().BonusText.gameObject.SetActive(true);
            mainScript.gameManager.CambioDeNivel();
        }

        if (collision.CompareTag("WarpOut"))
        {
            mainScript.gameManager.CambioDeNivel();
        }

        if (collision.CompareTag("BonusFlag"))
        {
            mainScript.gameManager.bonusClear = true;
        }

        if (collision.CompareTag("LifeUnlock"))
        {
            mainScript.gameManager.lifesLimit += 5;
            FindObjectOfType<congratsPaneQuit>().congratsPane.SetActive(true);
            Debug.Log("se ha incrementado el limite de vidas" + mainScript.gameManager.lifesLimit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("agua"))
        {
            mainScript.water = false;
        }

    } // Detecta cuando se sale de una colisión con objetos atravesables (agua)
}
