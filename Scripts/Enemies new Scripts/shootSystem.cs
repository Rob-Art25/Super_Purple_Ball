using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootSystem : MonoBehaviour
{    
    [Header("Variables")]
    public float raycastDistance;
    private float coolDownAttack = 1.5f, actualCoolDownAttack;

    [SerializeField]
    Transform enemyTransform;

    [SerializeField]
    GameObject proyectilePrefab;    

    // Update is called once per frame

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
            if(castDis > 0)
            {
                castDis = -raycastDistance;
            }
        }
        else
        {
            if(castDis < 0)
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
                    Invoke("Shoot", 0.5f);
                    actualCoolDownAttack = coolDownAttack;
                }
                Debug.DrawLine(transform.position, endPos, Color.black);
            }
            else
                Debug.DrawLine(transform.position, endPos, Color.grey);
        }
        else
        {
            Debug.DrawLine(transform.position, endPos, Color.green);
        }
    }


    void Shoot()
    {
        GameObject Bullet;        
        
        if(enemyTransform.localScale.x > 0)
        {
            if(proyectilePrefab.GetComponent<bullet>().speed > 0)
            {                
                proyectilePrefab.GetComponent<bullet>().speed *= -1;
                Bullet = Instantiate(proyectilePrefab, transform.position, Quaternion.identity);
            }
            else
                Bullet = Instantiate(proyectilePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            if(proyectilePrefab.GetComponent<bullet>().speed < 0)
            {                
                proyectilePrefab.GetComponent<bullet>().speed *= -1;
                Bullet = Instantiate(proyectilePrefab, transform.position, Quaternion.identity);
            }
            else
                Bullet = Instantiate(proyectilePrefab, transform.position, Quaternion.identity);
        }
    } // Listo!
}
