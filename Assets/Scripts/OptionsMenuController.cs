// Student name: Phan Tan Dat
// Student ID: 18127078
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsMenuController : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    int currentResolutionIndex = 0;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume){
        audioMixer.SetFloat("volume", volume);
    }
    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
}
