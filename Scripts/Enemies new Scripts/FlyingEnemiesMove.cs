using UnityEngine;

public class FlyingEnemiesMove : MonoBehaviour
{
    const string LEFT = "left", RIGHT = "right";

    [SerializeField]
    Transform castPos, roofCastPos, groundCastPos;

    [SerializeField]
    float baseCastDist;

    string facingDirection;

    Vector3 baseScale;

    Rigidbody2D rb2d;
    public float moveSpeed = 5, moveSpeedY;
    float vY;

    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        facingDirection = LEFT;
        rb2d = GetComponent<Rigidbody2D>();
        vY = moveSpeedY;
    }

    private void Update()
    {
        if (transform.position.y < -7.5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        float vX = moveSpeed;        

        if (facingDirection == RIGHT)
        {
            vX = -moveSpeed;
        }

        if(isHittingRoof())
        {
           vY = -moveSpeedY;           
        }else if(isHittingGround() && vY < 0)
        {
            vY = moveSpeedY;
        }

        // Mueve el Objeto/ enemigo
        rb2d.velocity = new Vector2(vX, vY);
                              
        if (isHittingWall())
        {
            if (facingDirection == LEFT)
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

        if (newDirection == RIGHT)
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
        if (facingDirection == RIGHT)
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

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    bool isHittingRoof()
    {
        bool val = false;

        float castDist = baseCastDist;        

        // determina el destino del objetivo basado en el castDist
        Vector3 roofTargetPos = roofCastPos.position;        
        roofTargetPos.y += castDist;        

        Debug.DrawLine(roofCastPos.position, roofTargetPos, Color.blue);        

        if (Physics2D.Linecast(roofCastPos.position, roofTargetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }        
        else
        {
            val = false;
        }

        return val;
    }

    bool isHittingGround()
    {
        bool val = false;

        float castDist = baseCastDist;

        // determina el destino del objetivo basado en el castDist
        Vector3 groundTargetPos = groundCastPos.position;
        groundTargetPos.y += -castDist;
        
        Debug.DrawLine(groundCastPos.position, groundTargetPos, Color.blue);

        if (Physics2D.Linecast(groundCastPos.position, groundTargetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }
}
