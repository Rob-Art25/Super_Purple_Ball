using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class congratsPaneQuit : MonoBehaviour
{

    public GameObject congratsPane;    

    public void okButton()
    {        
        congratsPane.SetActive(false);
    }
}
