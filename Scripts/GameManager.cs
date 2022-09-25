using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [Header("Variables: ")]
    public int vidas = 3, nivel, world;
    public int puntaje, bestScore;
    public int lifesLimit;

    [Header("Utilidades/Otros: ")]
    public bool puntoDeControl, gameOver = false, gameOverFinished = false, newSkin, goldenBall, bonusClear;
    private bool bonus1, bonus2, bonus3, bonus4;    
    [SerializeField] public GameObject PlayerPrefab, Player,warp1, warp2, warp3, warp4;
    public AudioSource dieClip, lvlTransition;    
    
    [HideInInspector]
    public float timeScale;

    private void Awake() // Singleton
    {
        int gameManagerCount = FindObjectsOfType<GameManager>().Length;
        if (gameManagerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        puntaje = 0;        
        puntoDeControl = false;
        newSkin = false;
        goldenBall = false;        
        lifesLimit = 5;        
    }

    // Update is called once per frame
    void Update()
    {
        bonusChecker();             
        nivel = SceneManager.GetActiveScene().buildIndex;
        world = worldChecker(nivel);                

        if (nivel == 16)
        {            
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            if (nivel == 15 || nivel == 0)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            else 
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }            

        }
        Time.timeScale = timeScale;

        // Control del puntaje
        processBestScore(puntaje);

        // Panel del tutorial inicial.
        if(FindObjectOfType<TextPanel>() != null && FindObjectOfType<TextPanel>().tuto == false)
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }


        lifeLimitChecker();

    }    

    int worldChecker(int nivel)
    {
        if(nivel > 0 && nivel <= 3)
        {
            world = 1;            
        }
        else if(nivel > 3 && nivel <= 6)
        {
            world = 2;            
        }
        else if(nivel > 6 && nivel <= 9)
        {
            world = 3;            
        }
        else if(nivel > 9 && nivel <= 12)
        {
            world = 4;            
        }

        return world;
    }   

    void lifeLimitChecker()
    {
        if (world == 2 && lifesLimit > 10)
        {
            lifesLimit = 10;
        }

        if (world == 3 && lifesLimit > 15)
        {
            lifesLimit = 15;
        }

        if (world == 4 && lifesLimit > 20)
        {
            lifesLimit = 20;
        }
    }

    public void CambioDeNivel()
    {                
        lvlTransition.Play();
        FindObjectOfType<CameraManager>().gameObject.transform.GetChild(0).gameObject.SetActive(true);        

        if (FindObjectOfType<JoyStickMove>().warp)
        {            
            FindObjectOfType<JoyStickMove>().warp = false;
            FindObjectOfType<BannerAd>().LoadBanner();
            Invoke("loadBonusStages", 1f);
        }
        else if(FindObjectOfType<JoyStickMove>().warp == false && SceneManager.GetActiveScene().buildIndex > 14)
        {
            if (bonus1)
                warp1.SetActive(false);
            else if (bonus2)
                warp2.SetActive(false);
            else if (bonus3)
                warp3.SetActive(false);
            else if (bonus4)
                warp4.SetActive(false);
            Invoke("backToMainLevel", 1f);
        }
        else
        {            
            Invoke("levelLoader", 1f);
        }        
    }

    // CONTROL DE MUERTE DEL PLAYER.
    public void ProcessDeath()
    {       
        FindObjectOfType<CameraManager>().gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Invoke("respawnSys", 0.5f);    
    }   

    public void transitionStop()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void levelLoader()
    {        
        FindObjectOfType<GameHUD>().FinishText.gameObject.SetActive(false);
        bonusClear = false;
        puntoDeControl = false;        
        PlayerPrefs.SetFloat("CheckPointX", 0);
        PlayerPrefs.SetFloat("CheckPointY", 0);
        FindObjectOfType<InterstitialAd>().ShowAd();

        if (nivel < 14)
        {            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);            
        }
        else
        {
            gameOverFinished = true;
            nivel = 16;
            SceneManager.LoadScene("Game Over Finished");
        }
    }

    public void respawnSys()
    {
        if (vidas > 0)
        {
            dieClip.Play();
            vidas--;            

            if (PlayerPrefab != null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if (vidas == 0)
        {
            nivel = 15;
            gameOver = true;
            SceneManager.LoadScene("Game Over");
        }
    }

    public void lifesManager()
    {
        if(vidas < lifesLimit)
        {            
            vidas++;            
        }
        else
        {
            puntaje += 1000;
        }

    }

    int processBestScore(int score)
    {
        if(score > bestScore)
        {
            bestScore = score;
        }
        
        return bestScore;
    }

    public void loadBonusStages()
    {
        FindObjectOfType<GameHUD>().BonusText.gameObject.SetActive(false);
        
        if(nivel == 1)
        {                        
            bonus1 = true;            
            SceneManager.LoadScene("Bonus 1");            
        }
        
        if(nivel == 5)
        {
            bonus2 = true;            
            SceneManager.LoadScene("Bonus 2");            
        }
        
        if((nivel == 8))
        {
            bonus3 = true;            
            SceneManager.LoadScene("Bonus 3");
        }
        
        if(nivel == 11)
        {
            bonus4 = true;            
            SceneManager.LoadScene("Bonus 4");
        }        
    }

    public void backToMainLevel()
    {
        FindObjectOfType<BannerAd>().HideBannerAd();

        if (bonus1)
        {
            bonus1 = false;
            SceneManager.LoadScene(1); 
        }
        if (bonus2)
        {
            bonus2 = false;
            SceneManager.LoadScene(5); 
        }
        if (bonus3)
        {
            bonus3 = false;
            SceneManager.LoadScene(8); 
        }
        if (bonus4)
        {
            bonus4 = false;
            SceneManager.LoadScene(11); 
        }
        // Poner lógica para el resto de Niveles con Nivel Bonus...
    }   

    void bonusChecker()
    {
        if (nivel == 1 && !bonusClear)
            warp1.SetActive(true);
        else
            warp1.SetActive(false);

        if (nivel == 5 && !bonusClear)
            warp2.SetActive(true);
        else
            warp2.SetActive(false);

        if (nivel == 8 && !bonusClear)
            warp3.SetActive(true);
        else
            warp3.SetActive(false);

        if (nivel == 11 && !bonusClear)
            warp4.SetActive(true);
        else
            warp4.SetActive(false);      
    }
}
