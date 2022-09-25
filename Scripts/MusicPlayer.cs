using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    [Header("Canciones para los Mundos: ")]
    public AudioSource Mundo1, Mundo2, Mundo3, Mundo4, Mundo5;
    


    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<GameManager>().nivel == 0 || FindObjectOfType<GameManager>().nivel > 15)
        {
            Mundo1.Stop();
            Mundo2.Stop();
            Mundo3.Stop();
            Mundo4.Stop();
            Mundo5.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        WorldChecker();
    }    

    void WorldChecker()
    {
            if(FindObjectOfType<GameManager>().nivel >= 1 && FindObjectOfType<GameManager>().nivel < 3)
            {                
                LevelChecker(1); // Mundo 1
            }
            else if(FindObjectOfType<GameManager>().nivel >= 4 && FindObjectOfType<GameManager>().nivel < 6)
            {            
                LevelChecker(2); // Mundo 2
            }
            else if(FindObjectOfType<GameManager>().nivel >= 7 && FindObjectOfType<GameManager>().nivel < 9)
            {            
                LevelChecker(3); // Mundo 3
            }
            else if(FindObjectOfType<GameManager>().nivel >= 10 && FindObjectOfType<GameManager>().nivel < 12)
            {            
                LevelChecker(4); // Mundo 4
            }
            else if(FindObjectOfType<GameManager>().nivel >= 13 && FindObjectOfType<GameManager>().nivel < 15)
            {            
                LevelChecker(5); // Mundo 5
            }
            else if(FindObjectOfType<GameManager>().gameOver || FindObjectOfType<GameManager>().gameOverFinished)
            {
                Mundo1.Stop();
                Mundo2.Stop();
                Mundo3.Stop();
                Mundo4.Stop();
                Mundo5.Stop();
            }
    }

    void LevelChecker(int level)
    {
        switch(level)
        {
            
            case 1:
                if(!Mundo1.isPlaying)
                {
                    Mundo1.Play();
                    Mundo2.Stop();
                    Mundo3.Stop();
                    Mundo4.Stop();
                    Mundo5.Stop();
                   break;
                }
                else
                {
                    break;
                }
            
            case 2:
                if (!Mundo2.isPlaying)
                {
                    Mundo2.Play();
                    Mundo1.Stop();
                    Mundo3.Stop();
                    Mundo4.Stop();
                    Mundo5.Stop();
                    break;
                }
                else
                {
                    break;
                }
            case 3:
                if (!Mundo3.isPlaying)
                {
                    Mundo3.Play();
                    Mundo2.Stop();
                    Mundo1.Stop();
                    Mundo4.Stop();
                    Mundo5.Stop();
                    break;
                }
                else
                {
                    break;
                }

            case 4:
                if (!Mundo4.isPlaying)
                {
                    Mundo4.Play();
                    Mundo2.Stop();
                    Mundo3.Stop();
                    Mundo1.Stop();
                    Mundo5.Stop();
                    break;
                }
                else
                {
                    break;
                }

            case 5:
                if (!Mundo5.isPlaying)
                {
                    Mundo5.Play();
                    Mundo2.Stop();
                    Mundo3.Stop();
                    Mundo4.Stop();
                    Mundo1.Stop();
                    break;
                }
                else
                {
                    break;
                }
        }
    }
}
