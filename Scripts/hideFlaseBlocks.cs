using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideFlaseBlocks : MonoBehaviour
{

    [SerializeField]
    float raycastDistance;

    [SerializeField]
    Animator animator;

    [SerializeField]
    AudioSource clip;

    private void FixedUpdate()
    {
        hideBlock();
    }

    void hideBlock()
    {
        float castDist = raycastDistance;

        RaycastHit2D blockRay = Physics2D.Raycast(transform.position, Vector2.up, castDist);

        if(blockRay.collider != null)
        {
            if(blockRay.collider.CompareTag("Player"))
            {
                clip.Play();
                animator.enabled = true;
            }
            
        }       
    }
}
