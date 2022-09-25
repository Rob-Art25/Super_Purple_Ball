using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    [Header("Variables: ")]
    public float speed = 0.5f;
    private float waitedTime;
    public float waitTime = 2;
    private int i = 0;
    public Transform[] movePoints;    
    private Vector2 currentPos;    

    // Start is called before the first frame update
    void Start()
    {
        waitedTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoints[i].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[i].transform.position) < 0.1f)
        {

            if (waitedTime <= 0)
            {

                if (movePoints[i] != movePoints[movePoints.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }

                waitedTime = waitTime;
            }
            else
            {
                waitedTime -= Time.deltaTime;
            }
        }        
    }    
}
