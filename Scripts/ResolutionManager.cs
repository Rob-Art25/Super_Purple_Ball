using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{

    Resolution[] resolutions;
    public Dropdown resolutionsDropdown;
    
    // Start is called before the first frame update
    void Start()
    {
        ResolutionChecker();
    }

    public void ResolutionChecker()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> opciones = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string opcion = resolutions[i].width + " x " + resolutions[i].height;
            opciones.Add(opcion);

            if(Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        resolutionsDropdown.AddOptions(opciones);
        resolutionsDropdown.value = currentResolution;
        resolutionsDropdown.RefreshShownValue();

        //
        PlayerPrefs.GetInt("ResolutionIndex", 0);
        //
    }


    public void ResolutionChange(int resolutionIndex)
    {
        //
        PlayerPrefs.SetInt("ResolutionIndex", resolutionsDropdown.value);
        //        
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
