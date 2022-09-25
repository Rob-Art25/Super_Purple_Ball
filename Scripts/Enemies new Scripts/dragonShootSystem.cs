using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonShootSystem : MonoBehaviour
{
    [Header("Variables")]
    public int raycastDistance;
    private float coolDownAttack = 1.5f, actualCoolDownAttack;
    [SerializeField]
    Transform dragon;

    [SerializeField]
    GameObject fireThrower;

    // Update is called once per frame

    private void Start()
    {
        actualCoolDownAttack = 0;
        fireThrower.SetActive(false);
    }

    private void Update()
    {
        actualCoolDownAttack -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(!CanSeePlayer())
        {
            if (fireThrower.activeSelf == true)
            {
                Invoke("stopFireThrowing", 1);
            }            
        }       
    }

    bool CanSeePlayer()
    {
        bool val = false;
        
        float castDist = raycastDistance;
        if (!GetComponentInParent<SpriteRenderer>().flipX)
        {
            castDist = -raycastDistance;
        }

        Vector2 endPos = transform.position + Vector3.right * castDist;
        RaycastHit2D hit2D = Physics2D.Linecast(transform.position, endPos, raycastDistance);

        if (hit2D.collider != null)
        {
            if (hit2D.collider.CompareTag("Player"))
            {
                if (actualCoolDownAttack < 0)
                {
                    Shoot();
                    val = true;
                    actualCoolDownAttack = coolDownAttack;
                }
                Debug.DrawRay(transform.position, Vector2.left, Color.yellow);
            }
            else
            {
                val = false;
                Debug.DrawRay(transform.position, Vector2.left, Color.blue);
            }
        }

        return val;
    }

    void Shoot()
    {
        if(dragon.localScale.x > 0)
        {
            fireThrower.transform.localScale = new Vector3(fireThrower.transform.localScale.x * -1, fireThrower.transform.localScale.y, fireThrower.transform.localScale.z);
            fireThrower.SetActive(true);
        }
        else
        {
          fireThrower.SetActive(true);
        }
    }

    void stopFireThrowing()
    {
        fireThrower.SetActive(false);
    }

}
