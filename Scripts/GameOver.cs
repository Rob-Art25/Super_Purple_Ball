using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text score, BestScore, SkinUnlocked;

    private void Update()
    {
        if(FindObjectOfType<GameManager>().nivel == 14)
        {
            score.text = "Score: " + FindObjectOfType<GameManager>().puntaje.ToString();
            BestScore.text = "Best Score: " + FindObjectOfType<GameManager>().bestScore.ToString();
            if(FindObjectOfType<GameManager>().puntaje >= 100000)
            {                
                SkinUnlocked.text = "Felicidades! has desbloqueado un nuevo skin: Golden Ball";
                FindObjectOfType<GameManager>().newSkin = true;
            }
        }
        else
        {
            score.text = "Score: " + FindObjectOfType<GameManager>().puntaje.ToString();
            BestScore.text = "Best Score: " + FindObjectOfType<GameManager>().bestScore.ToString();
            SkinUnlocked.text = "Puedes Hacerlo mejor! Vamos, intentalo otra vez! si consigues una buena cantidad de puntos conseguiras algo increible";
        }
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }


    public void Exit()
    {
        Application.Quit();
    }

}
