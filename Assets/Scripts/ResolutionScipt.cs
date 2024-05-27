using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionScipt : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolution;
    private List<Resolution> filteredResolutions;

    private float currentRefreshrate;
    private int currentResolutionIndex = 0;

    void Start()
    {
        resolution = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        currentRefreshrate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolution.Length; i++)
        {
            if (resolution[i].refreshRate == currentRefreshrate)
            {
                filteredResolutions.Add(resolution[i]);
            }
        }

        List<string> Options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string ResolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " +
                                      filteredResolutions[i].refreshRate + "hz";
            Options.Add(ResolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

       resolutionDropdown.AddOptions(Options);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
       Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}