using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings_Menu : MonoBehaviour
{
  

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

  
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
   
    public void setFulscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    

 

}
