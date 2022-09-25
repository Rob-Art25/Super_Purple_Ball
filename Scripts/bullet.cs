using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed = 8f;
    public float lifeTime = 2f;        
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {       
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
