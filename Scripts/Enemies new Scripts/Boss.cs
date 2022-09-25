using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject door, gift;

    public JumpCollider parent;    

    private void Start()
    {        
        door.SetActive(false);
        if (gift != null)
            gift.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(parent.lifes <= 0)
        {            
            door.SetActive(true);
            if(gift != null)
                gift.SetActive(true);
        }
    }   
}
