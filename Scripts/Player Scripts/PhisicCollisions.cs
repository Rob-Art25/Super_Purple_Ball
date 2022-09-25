using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhisicCollisions : MonoBehaviour
{

    JoyStickMove mainScript;

    private void Start()
    {
        mainScript = GetComponent<JoyStickMove>();
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hielo"))
        {
            mainScript.onIce = true;
        }

        if (collision.gameObject.CompareTag("Enemigos"))
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hielo"))
        {
            mainScript.onIce = false;
        }
    }
}
