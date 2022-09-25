using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newEnemyMove : MonoBehaviour
{
    const string LEFT = "left", RIGHT = "right";

    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDist;

    string facingDirection;

    Vector3 baseScale;

    Rigidbody2D rb2d;
    public float moveSpeed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = LEFT;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position.y < -7.5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        float vX = moveSpeed;

        if(facingDirection == RIGHT)
        {
            vX = -moveSpeed;
        }        
        
        // Mueve el Objeto/ enemigo
        rb2d.velocity = new Vector2(vX, rb2d.velocity.y);

        if(isHittingWall() || isNearEdge())
        {
            if(facingDirection == LEFT)
            {
                changeFacingDirection(RIGHT);
            }
            else
            {
                changeFacingDirection(LEFT);
            }
        }
    }

    void changeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if(newDirection == RIGHT)
        {
            newScale.x = -baseScale.x;
        }
        else
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;
    }

    bool isHittingWall()
    {
        bool val = false;

        float castDist = baseCastDist;

        // Define la distancia de izq o der.
        if(facingDirection == RIGHT)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;
        }

        // determina el destino del objetivo basado en el castDist
        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if(Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    bool isNearEdge()
    {
        bool val = true;

        float castDist = baseCastDist;              

        // determina el destino del objetivo basado en el castDist
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
}
