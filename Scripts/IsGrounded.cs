using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public static bool isGrounded;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground") || collision.CompareTag("Hielo"))
        {
            isGrounded = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Ground") || collision.CompareTag("Hielo"))
        {
            isGrounded = false;
        }

    }

}
