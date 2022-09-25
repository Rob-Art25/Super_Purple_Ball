using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHUD : MonoBehaviour
{
    public TMP_Dropdown dropDown;

    private int index = 0;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public RuntimeAnimatorController[] playerController;
    public Sprite[] playerRenderer;

    List<string> players = new List<string>();

    private void Start()
    {
        dropDown.ClearOptions();        
        if (FindObjectOfType<GameManager>()!= null)
        {
            players.Add("Purple Ball");
            players.Add("Golden Ball");
            dropDown.AddOptions(players);
            dropDown.value = PlayerPrefs.GetInt("Skin", 0);
            dropDown.RefreshShownValue();                      
        }
    }

    private void Update()
    {
        if (FindObjectOfType<GameManager>() != null)
        {
            dropDown.gameObject.SetActive(true);

            if (FindObjectOfType<GameManager>().newSkin)
            {
                dropDown.interactable = true;                
            }
            else
            {
                dropDown.interactable = false;
            }
        }
        else
        {
            dropDown.gameObject.SetActive(false);
        }
    }


    // Agregar efectos animaciones y todo eso para el inicio del juego
    public void StarButton()
    {
        
        if(FindObjectOfType<GameManager>() != null)
        {
            FindObjectOfType<GameManager>().vidas = 3;
            FindObjectOfType<GameManager>().nivel = 1;
            FindObjectOfType<GameManager>().puntaje = 0;
            FindObjectOfType<GameManager>().gameOver = false;
            FindObjectOfType<GameManager>().gameOverFinished = false;            
            

            if (PlayerPrefs.GetFloat("CheckPointX") != 0)
            {
                PlayerPrefs.SetFloat("CheckPointX", 0);
                PlayerPrefs.SetFloat("CheckPointY", 0);
            }
            SceneManager.LoadScene(1);
        }else
        {
            SceneManager.LoadScene(1);
        }                
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void skinChange()
    {
        index = dropDown.value;        
        characterSelect(dropDown.value);
    }

    void characterSelect(int index)
    {                        
        switch (index + 1)
        {
            case 1:  //Player.PurpleBall:
                spriteRenderer.sprite = playerRenderer[0];
                animator.runtimeAnimatorController = playerController[0];
                PlayerPrefs.SetInt("Skin", dropDown.value);
                FindObjectOfType<GameManager>().goldenBall = false;
                break;
            case 2: // Player.GoldenBall:
                spriteRenderer.sprite = playerRenderer[1];
                animator.runtimeAnimatorController = playerController[1];
                PlayerPrefs.SetInt("Skin", dropDown.value);
                FindObjectOfType<GameManager>().goldenBall = true;
                break;
            default:
                break;
        }
    }
}
