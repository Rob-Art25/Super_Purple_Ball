using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectsUp : MonoBehaviour
{

    public float speed = 1f;
    
    

    void Start()
    {
        Destroy(gameObject, 6);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed;
    }
}
