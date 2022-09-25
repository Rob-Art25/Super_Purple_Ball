using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectPlayer : MonoBehaviour
{
    
    public enum Player {PurpleBall, GoldenBall}
    public Player playerSelected;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public RuntimeAnimatorController[] playerController;
    public Sprite[] playerRenderer;      


    void Start()
    {     

        switch (playerSelected)
        {
            case Player.PurpleBall:                
                spriteRenderer.sprite = playerRenderer[0];
                animator.runtimeAnimatorController = playerController[0];               
                break;
            case Player.GoldenBall:
                spriteRenderer.sprite = playerRenderer[1];
                animator.runtimeAnimatorController = playerController[1];
                break;
            default:
                break;
        }
    }    
}
