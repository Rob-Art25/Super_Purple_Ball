using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    public GameManager gm;
    private int top1, top2, top3, top4;
    public Text Top1, Top2, Top3, Top4;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        PlayerPrefs.GetInt("Top1", 0);
        top2 = 0;
        top3 = 0;
        top4 = 0;
        leaderBoardManager();
    }


    void leaderBoardManager()
    {
        if(PlayerPrefs.GetInt("Top1") == 0)
        {
            if(top1 < gm.bestScore)
            {
                top1 = gm.bestScore;
                PlayerPrefs.SetInt("Top1", top1);                
            }
            
        }
        else
        {
            top1 = PlayerPrefs.GetInt("Top1");

            if(top1 < gm.bestScore)
            {
                top2 = top1;
                top1 = gm.bestScore;
                PlayerPrefs.SetInt("Top1", top1);
                PlayerPrefs.SetInt("Top2", top2);
            }
            else
            {                
                top2 = PlayerPrefs.GetInt("Top2"); 
                
                if(top2 < gm.bestScore)
                {
                    top3 = top2;
                    top2 = gm.bestScore;
                    PlayerPrefs.SetInt("Top2", top2);
                    PlayerPrefs.SetInt("Top3", top3);
                }
                else
                {
                    top3 = PlayerPrefs.GetInt("Top3");

                    if(top3 < gm.bestScore)
                    {
                        top4 = top3;
                        top3 = gm.bestScore;
                        PlayerPrefs.SetInt("Top3", top3);
                        PlayerPrefs.SetInt("Top4", top4);
                    }
                }
            }

            
        }
        
        Top1.text = "Top 1: " + top1.ToString();
        Top2.text = "Top 2: " + top2.ToString();
        Top3.text = "Top 3: " + top3.ToString();
        Top4.text = "Top 4: " + top4.ToString();        
    }

}
