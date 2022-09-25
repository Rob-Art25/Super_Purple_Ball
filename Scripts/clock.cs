using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour
{
    public Text clockText;
    public int seconds;
    public int timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = seconds;
        Invoke("updateClockText", 1f);
    }         

   void updateClockText()
    {
        timer--;        
        clockText.text = "TIME: " + timer.ToString();
        Invoke("updateClockText", 1f);

        if(timer <= 0)
        {
            stopClock();
            FindObjectOfType<GameManager>().ProcessDeath();            
        }
    }

    void stopClock()
    {
        CancelInvoke();
    }
}
