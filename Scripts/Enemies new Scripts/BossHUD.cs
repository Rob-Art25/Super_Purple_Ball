using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour
{

    [SerializeField]
    Image bossHealthBar;

    [SerializeField]
    JumpCollider bossHealth;

    int maxHealth, currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = bossHealth.lifes;
        currentHealth = 0;
        bossHealthBar.fillAmount = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = bossHealth.lifes;
        bossHealthBar.fillAmount = (float)currentHealth/maxHealth;
    }
}
