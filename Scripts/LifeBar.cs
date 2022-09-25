using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeBar : MonoBehaviour
{
    public float maxHP, currentHP;
    public Image hpBar, player, backGroundBar;
    public Text hpText;

    private void Start()
    {
        maxHP = FindObjectOfType<JoyStickMove>().salud;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(FindObjectOfType<JoyStickMove>() != null)
        {
            currentHP = FindObjectOfType<JoyStickMove>().salud;
            hpBar.fillAmount = currentHP / maxHP;
            hpText.text = "HP: " + FindObjectOfType<JoyStickMove>().salud.ToString();
        }
        
        LvlCheck();
    }


    private void LvlCheck()
    {
        if (SceneManager.GetActiveScene().buildIndex == 16 || SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().name == "Game Over Finished")
        {
            hpText.enabled = false;
            hpBar.enabled = false;
            player.enabled = false;
            backGroundBar.enabled = false;
        }
        else
        {
            hpText.enabled = true;
            hpBar.enabled = true;
            player.enabled = true;
            backGroundBar.enabled = true;
        }
    }
}
