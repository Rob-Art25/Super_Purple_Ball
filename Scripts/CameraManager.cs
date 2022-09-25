using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public GameObject follow;
    [SerializeField] Vector2 minCamPos, maxCamPos;
    public float smoothTime;    
    private Vector2 Velocity;
    
    void FixedUpdate()
    {
        if (follow == null)
        {
            follow = FindObjectOfType<GameManager>().Player;            
        }
        else
        {
            float posX = Mathf.SmoothDamp(transform.position.x, follow.transform.position.x, ref Velocity.x, smoothTime);
            float posY = Mathf.SmoothDamp(transform.position.y, follow.transform.position.y, ref Velocity.y, smoothTime);            

            transform.position = new Vector3(Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), transform.position.z);
        }
    }
}
