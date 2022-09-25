using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JoyStickMove : MonoBehaviour
{
    [Header("Variables Principales: Movimiento")]
    
    [SerializeField] public float MoveSpeed;     
    [SerializeField] float JumpSpeed;
    private float horizontalMove;    
    private Rigidbody2D rb2d;
    public SpriteRenderer Sprite;
    public float salud, maxSalud = 10, iceSpeed;
    public bool water;
    [SerializeField] Joystick joystick;

    [Header("Variables Extra: Extras")]
    public bool canDoubleJump = false, warp = false;
    public GameManager gameManager;
    public AudioSource jumpClip, hitClip;
    public bool onIce;
    public Animator animator;

    void Start()
    {               
                // SE INICIALIZAN LOS VALORES NECESARIOS
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        horizontalMove = 0;     
        
        if(PlayerPrefs.GetFloat("CheckPointX") != 0 && gameManager.puntoDeControl)
        {
            transform.position =  new Vector2(PlayerPrefs.GetFloat("CheckPointX"), PlayerPrefs.GetFloat("CheckPointY"));
        }                

        if(gameManager.goldenBall)
        {
            // Activar el nuevo skin
            Debug.Log("Se ha Activado el Nuevo Skin");           
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }

    }
    
    void Update()
    {
                    // DETECCIÓN DE MUERTE POR CAÍDA.
        if (transform.position.y < -7f && gameObject != null)
        {            
            Die();
        }

                    // CONTROL DE ANIMACIÓN: Movimiento y Flip.
        if (horizontalMove < 0)
        {          
                Sprite.flipX = false;                                      
        }
        else if (horizontalMove > 0)
        {            
                Sprite.flipX = true;                
        }
               
        if(salud <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        joyStickMove();
    }

                    // MÉTODO: PROCESAR MUERTE.
    public void Die()
    {        
        gameManager.ProcessDeath();
        Destroy(gameObject);
    }    

                    // MÉTODO: MOVIMIENTO.
    public void joyStickMove()
    {
        Vector2 iceMove = new Vector2(iceSpeed, rb2d.velocity.y);
        

        if (onIce)
        {
            transform.position = Vector2.MoveTowards(transform.position, iceMove, -iceSpeed * Time.deltaTime); // Línea para resbalar en el Hielo
        }      

        if(joystick != null)
        {
            horizontalMove = joystick.Horizontal * MoveSpeed;
            rb2d.velocity = new Vector2(horizontalMove, rb2d.velocity.y);
        }
        else
        {
            joystick = FindObjectOfType<Joystick>();
            horizontalMove = joystick.Horizontal * MoveSpeed;
            rb2d.velocity = new Vector2(horizontalMove, rb2d.velocity.y);            
        }
        
    }

                    // MÉTODO: SALTO
    public void jump()
    {        
       if(IsGrounded.isGrounded || water)
       {            
            rb2d.velocity = new Vector2(rb2d.velocity.x, JumpSpeed);
            jumpClip.Play();
            canDoubleJump = true;            
       }
       else
       {                                            
            if (canDoubleJump)
            {                    
                DoubleJump();
            }
       }        
    }

                    // MÉTODO: DOBLE SALTO
    void DoubleJump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, JumpSpeed);
        jumpClip.Play();
        canDoubleJump = false;        
    }
    
}



