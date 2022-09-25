using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour
{
    public bool tuto;
    private void Start()
    {
        FindObjectOfType<GameManager>().timeScale = 0;
        tuto = true;
    }    

    public void okButton()
    {
        FindObjectOfType<GameManager>().timeScale = 1;        
        gameObject.SetActive(false);
        tuto = false;
    }
}
