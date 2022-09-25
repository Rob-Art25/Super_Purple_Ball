using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropMinnions : MonoBehaviour
{

    [SerializeField]
    GameObject minnion;
    
    
    private float actualCoolDownAttack, coolDownAttack = 3f;
    public float raycastDistance;
    
    [SerializeField]
    private Transform enemyTransform;

    private void Start()
    {
        actualCoolDownAttack = 0;
    }

    private void Update()
    {
        actualCoolDownAttack -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        CanSeePlayer();
        
    }

    void CanSeePlayer()
    {
        float castDis = raycastDistance;
        
        
        
        if (enemyTransform.localScale.x < 0)
        {
            if (castDis > 0)
            {
                castDis = -raycastDistance;
            }
        }
        else
        {
            if (castDis < 0)
            {
                castDis = -raycastDistance;
            }
        }


        Vector2 endPos = transform.position + Vector3.right * castDis;
        RaycastHit2D hit2D = Physics2D.Linecast(transform.position, endPos, 1 << LayerMask.NameToLayer("Player"));

        if (hit2D.collider != null)
        {
            if (hit2D.collider.CompareTag("Player"))
            {
                if (actualCoolDownAttack < 0)
                {
                    spawnMinnion();
                    actualCoolDownAttack = coolDownAttack;
                }
                Debug.DrawLine(transform.position, endPos, Color.yellow);
            }
            else
                Debug.DrawLine(transform.position, endPos, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, endPos, Color.blue);
        }
    }

    void spawnMinnion()
    {
        Vector3 newMinnionPos = new Vector3(transform.position.x, Random.Range(-4f, 2f));
        GameObject bossMinnion = Instantiate(minnion, newMinnionPos, Quaternion.identity);        
    }

}
