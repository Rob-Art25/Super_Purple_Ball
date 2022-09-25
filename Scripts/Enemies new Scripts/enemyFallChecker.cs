using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFallChecker : MonoBehaviour
{
    public float speed;
    public int raycastDistance;
    Rigidbody2D rb2d;

    [SerializeField] 
    Transform castPoint;
    
    [SerializeField] 
    Transform player;

    bool isFacingRight;
    private bool isActive;
    private bool isSearching;

    private float coolDownAttack = 1.5f, actualCoolDownAttack;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        actualCoolDownAttack = 0;
    }

    // Update is called once per frame

    private void Update()
    {
        actualCoolDownAttack -= Time.deltaTime;
        
        if(CanSeePlayer(raycastDistance))
        {
            isActive = true;           
        }
        else
        {
            if(isActive)
            {
                if(!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 5);
                }
            }
            
        }

        if(isActive)
        {
            Invoke("ChasePlayer", 1f);
        }

        enemyDie();
    }    

    void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            if(actualCoolDownAttack < 0)
            {
                // El enemigo está del lado izquierdo del jugador
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
                transform.localScale = new Vector2(-1, 1); // para controlar el Flip (creo)            
                isFacingRight = true;
                actualCoolDownAttack = coolDownAttack;
            }
            
        }
        else
        {
            if(actualCoolDownAttack < 0)
            {
                // El enemigo está a la derecha del Jugador
                rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
                transform.localScale = new Vector2(1, 1); //para controlar el Flip(creo)            
                isFacingRight = false;
                actualCoolDownAttack = coolDownAttack;
            }
        }
    }

    void StopChasingPlayer()
    {
        isActive = false;
        isSearching = false;
        rb2d.velocity = new Vector2(0, 0);
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;        

        if (isFacingRight)
        {
            castDist = -distance;            
        }     

            Vector2 endPos = castPoint.position + Vector3.right * castDist;
            RaycastHit2D hit2D = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

        if (hit2D.collider != null)
        {
            if (hit2D.collider.CompareTag("Player"))
            {                
                player = hit2D.collider.transform;
                val = true;                
            }
            else
            {                
                val = false;
            }

            Debug.DrawLine(castPoint.position, hit2D.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, hit2D.point, Color.blue);            
        }

        return val;
    }

    void enemyDie()
    {
        if(transform.position.y < -7.5f)
        {
            Destroy(gameObject);
        }
    }
}
