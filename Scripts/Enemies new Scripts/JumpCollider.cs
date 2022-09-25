using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JumpCollider : MonoBehaviour
{
    
    public GameObject particle, score;
    public SpriteRenderer sprite;
    public float jumpforce = 2.5f;
    public int lifes = 2;
    public int localScore;    
    public AudioSource clip;
    public Animator enemyAnim;
    [SerializeField]
    GameObject damagePoint;
    
    //Script para daño por Salto

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.CompareTag("Player"))
        {            
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * jumpforce);
            particle.SetActive(true);            
            LooseLife();
            CheckLife();
        }

    }

    public void LooseLife()
    {
        lifes--;        
        
    }

    public void CheckLife()
    {         
        if(enemyAnim != null)
            enemyAnim.Play("Hit");        

        if (lifes == 0)
        {
            FindObjectOfType<GameManager>().puntaje += localScore;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            particle.SetActive(false);

            if(damagePoint != null)
                damagePoint.SetActive(false);

            score.SetActive(true);
            if (clip != null)
            {
                clip.Play();
                Invoke("EnemyDie", 2.5f);
            }
            else
            {
                Invoke("EnemyDie", 0.5f);
            }
                
        }
    }   

    public void EnemyDie()
    {        
        Destroy(gameObject);
    }   
}
