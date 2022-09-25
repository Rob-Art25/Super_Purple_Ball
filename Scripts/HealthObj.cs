using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObj : MonoBehaviour
{
    [Header("Varaibles: ")]
    public int HP;


    private void OnTriggerEnter2D(Collider2D collision)  // Detecta la colisión (atravesable) con el player para darle los puntos de salud.
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<JoyStickMove>().salud < collision.GetComponent<JoyStickMove>().maxSalud)
                collision.GetComponent<JoyStickMove>().salud += HP;            
        }
    }
}
