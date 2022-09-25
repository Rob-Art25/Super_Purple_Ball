using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class GameHUD : MonoBehaviour
{
    [Header("Objetos: ")]
    [SerializeField] Text scoreText, livesText, levelText;
    public Text FinishText, BonusText;
    public GameObject PausePanel, optionsPane;
    public AudioSource buttonClip, dieClip, levelClearedClip;    
    public Button JumpButton, pauseButton;
    public Slider slider;
    public Image muteIcon;
    public GameObject pauseFirstButton, optionsFirstButton, optionsCloseButton;
    
    private float sliderValue;
    JoyStickMove JoyStick;
    GameManager gameManager;

    //----Movimiento con el TouchPad----------------
    [Header("Variables: ")]    
    public Joystick joyStick;    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        UpdateLives(gameManager.vidas);
        UpdateScore(gameManager.puntaje);
        UpdateLevelTxt(gameManager.nivel);        
        JoyStick = FindObjectOfType<JoyStickMove>();

        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        AudioListener.volume = slider.value;
        checkIfMute();
        FinishText.gameObject.SetActive(false);
    }

    private void Update()
    {
        changeSlider(slider.value);
        
        if(SceneManager.GetActiveScene().name != "Menú")
        {
            UpdateScore(gameManager.puntaje);
            UpdateLives(gameManager.vidas);
            UpdateLevelTxt(gameManager.nivel);            
        }                

        if(IsGrounded.isGrounded || JoyStick.canDoubleJump)
        {
            JumpButton.interactable = true;
        }
        else
        {
            JumpButton.interactable = false;
        }

    }

            // ACTUALIZAR TEXTO DE VIDAS     
    public void UpdateLives(int lives)
    {
        livesText.text = "x " + lives.ToString();
    }
            // ACTUALIZAR TEXTO DE SCORE
    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
            // ACTUALIZAR TEXTO DE NIVEL
     public void UpdateLevelTxt(int level)
     {
        if(gameManager.nivel > 0 && gameManager.nivel <= 3)
        {
            levelText.text = "Level: 1 -  " + level.ToString();
        }
        
        if(gameManager.nivel > 3 && gameManager.nivel <= 6)
        {
            levelText.text = "Level: 2 -  " + (level - 3).ToString();
        }
        
        if(gameManager.nivel > 6 && gameManager.nivel <= 9)
        {
            levelText.text = "Level: 3 -  " + (level - 6).ToString();
        }
        
        if(gameManager.nivel > 9 && gameManager.nivel < 12)
        {
            levelText.text = "Level: 4 -  " + (level - 9).ToString();            
        }

        if (gameManager.nivel == 12)
        {
            levelText.text = "Level: 4 -  3: " + "FINAL LEVEL";
        }
    }   

      // BOTÓN PAUSA
    public void PauseButton()
    {
        buttonClip.Play();

        if (gameManager.timeScale != 0)
        {
            gameManager.timeScale = 0;
            PausePanel.SetActive(true);
            
            // PRIMERO SE LIMPIA LA SELECCIÓN
            EventSystem.current.SetSelectedGameObject(null);
            // YA DESPUÉS SE ESTABLECE LA PRIMER OPCIÓN AL ABRIR EL MENÚ
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        else
        {
            // PRIMERO SE LIMPIA LA SELECCIÓN
            EventSystem.current.SetSelectedGameObject(null);
            PausePanel.SetActive(false);
            gameManager.timeScale = 1;
        }
    }

            // BOTÓN SALIR DEL JUEGO
    public void ExitButton()
    {
        buttonClip.Play();
        PausePanel.SetActive(false);
        gameManager.timeScale = 1;        
        FindObjectOfType<GameManager>().bonusClear = false;

        SceneManager.LoadScene(0);
    }
            // SALTAR
    public void Jump()
    {
        if (JoyStick != null)
            JoyStick.jump();
        else
        {            
            JoyStick = FindObjectOfType<JoyStickMove>();
            JoyStick.jump();
        }
    }                        

    public void optionPaneActivate()
    {
        // PRIMERO SE LIMPIA LA SELECCIÓN
        EventSystem.current.SetSelectedGameObject(null);
        // YA DESPUÉS SE ESTABLECE LA PRIMER OPCIÓN DEL MENÚ
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        optionsPane.SetActive(true);
    }

    public void optionPaneOkButton()
    {
        optionsPane.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsCloseButton);
    }

    public void changeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        AudioListener.volume = slider.value;
        checkIfMute();
    }

    void checkIfMute()
    {
        if(slider.value == 0)
        {
            muteIcon.enabled = true;
        }
        else
        {
            muteIcon.enabled = false;
        }
    }        

}
